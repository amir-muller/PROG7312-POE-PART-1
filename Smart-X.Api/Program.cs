using Microsoft.EntityFrameworkCore;
using Smart_X.Api.DataModels;
using Smart_X.Api.Services;
using SystemFrontEnd.Services;

var builder = WebApplication.CreateBuilder(args);

//sensor manager and network analysis engine
builder.Services.AddSingleton<RealTimeSensorManager>();
builder.Services.AddSingleton<NetworkAnalysisEngine>();

//background sim worker
builder.Services.AddHostedService<SensorSimulatorService>();

//database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=appdata.db"));

//reg data service
builder.Services.AddScoped<IDataService, DataService>();

var app = builder.Build();

//create database on startup if not there
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.EnsureDeletedAsync().Wait();
    db.Database.EnsureCreated();

    var dataService = scope.ServiceProvider.GetRequiredService<IDataService>();
    var engine = scope.ServiceProvider.GetRequiredService<NetworkAnalysisEngine>();

    var existingSensors = dataService.GetAllSensorDetailsAsync().Result;
    if (existingSensors != null && existingSensors.Any())
    {
        //rebuild graph
        engine.BuildDynamicTopology(existingSensors);
    }

}


//defining api endpoints
var api = app.MapGroup("/api/data");

//-----------------------------------------------------------------------------
//sensor details
var sensorApi = app.MapGroup("/api/sensors");

sensorApi.MapPost("/", async (CreateSensorDetailsRequest request, IDataService dataService, NetworkAnalysisEngine engine, CancellationToken ct) =>
{
    if (string.IsNullOrWhiteSpace(request.SensorMAC))
        return Results.BadRequest("MAC address is required.");

    var cleanRequest = request with
    {
        SensorMAC = request.SensorMAC.Trim(),
        SensorName = string.IsNullOrWhiteSpace(request.SensorName) ? "Sensor" : request.SensorName.Trim(),
        SensorLocation = string.IsNullOrWhiteSpace(request.SensorLocation) ? "Sector" : request.SensorLocation.Trim()
    };

    var result = await dataService.SaveSensorDetailsAsync(cleanRequest, ct);
    var allSensors = await dataService.GetAllSensorDetailsAsync(ct);

    engine.ConnectNewNodeWithNearestNeighbors(result, allSensors);

    return Results.Created($"/api/sensors/{result.Id}", result);
});

sensorApi.MapGet("/", async (IDataService dataService, CancellationToken ct) =>
{
    var sensors = await dataService.GetAllSensorDetailsAsync(ct);
    return Results.Ok(sensors);
});

//-----------------------------------------------------------------------------
//sensor data
api.MapPost("/", async (CreateSensorDataRequest request, IDataService dataService, CancellationToken ct) =>
{
    if (string.IsNullOrWhiteSpace(request.SensorMAC))
    {
        return Results.BadRequest("Sensor MAC address cannot be empty.");
    }

    var result = await dataService.SaveDataAsync(request, ct);
    return Results.Created($"/api/data/{result.Id}", result);
});

api.MapGet("/", async (IDataService dataService, CancellationToken ct) =>
{
    var data = await dataService.GetAllDataAsync(ct);
    return Results.Ok(data);
});

//BST Historical Range Query Endpoint
api.MapGet("/range", async (string sensorMac, string start, string end, IDataService dataService, CancellationToken ct) =>
{
    if (!DateTime.TryParse(start, out var parsedStart) || !DateTime.TryParse(end, out var parsedEnd))
    {
        return Results.BadRequest("Invalid date range format.");
    }

    var allData = await dataService.GetAllDataAsync(ct);

    var bst = new SensorHistoryBst();
    foreach (var resp in allData.Where(d => d.SensorMAC.Equals(sensorMac, StringComparison.OrdinalIgnoreCase)))
    {
        var record = new SensorDataRecord
        {
            Id = resp.Id,
            SensorMAC = resp.SensorMAC,
            SensorValue = resp.SensorValue,
            TimeStamp = resp.TimeStamp
        };

        bst.Insert(record);
    }

    var rangeResults = bst.RangeQuery(parsedStart, parsedEnd);
    return Results.Ok(rangeResults);
});


api.MapGet("/stream-state", (RealTimeSensorManager manager) =>
    Results.Ok(manager.GetDashboardState()));

api.MapPost("/process-next", (RealTimeSensorManager manager) =>
{
    var processed = manager.ProcessNext();
    return processed is null ? Results.NoContent() : Results.Text(processed);
});

api.MapPost("/override", (ManualOverrideRequest request, RealTimeSensorManager manager) =>
{
    if (string.IsNullOrWhiteSpace(request.Command) || request.TargetMACs is null || request.TargetMACs.Count == 0)
        return Results.BadRequest("A command and at least one target MAC are required.");

    string entry = $"{request.Command} -> {string.Join(", ", request.TargetMACs)}";
    manager.RecordManualCommand(entry);
    return Results.Text($"Command sent: {entry}");
});

api.MapPost("/override/undo", (RealTimeSensorManager manager) =>
{
    var undone = manager.UndoLastCommand();
    return Results.Text(undone is null ? "Nothing to undo." : $"Undid: {undone}");
});


api.MapPost("/", async (CreateSensorDataRequest request, IDataService dataService, RealTimeSensorManager manager, CancellationToken ct) =>
{
    if (string.IsNullOrWhiteSpace(request.SensorMAC))
        return Results.BadRequest("Sensor MAC address cannot be empty.");

    var result = await dataService.SaveDataAsync(request, ct);

    bool isCritical = result.SensorValue > 75;
    manager.EnqueueTelemetry(result, isCritical, isCritical ? 1 : 5);

    return Results.Created($"/api/data/{result.Id}", result);
});


//-----------------------------------------------------------------------------
//topology analysis endpoints
var topologyApi = app.MapGroup("/api/topology");

topologyApi.MapGet("/dfs/{startNode}", (string startNode, NetworkAnalysisEngine engine) =>
{
    var connectedDevices = engine.GetConnectedDevicesDfs(startNode);
    return Results.Ok(connectedDevices);
});

topologyApi.MapGet("/mst/path", (string startNode, string targetNode, NetworkAnalysisEngine engine) =>
{
    var mstParent = engine.ComputePrimsMst(startNode);
    var path = engine.GetHopPathToGateway(mstParent, targetNode);
    return Results.Ok(new { Path = path, TotalHops = path.Count - 1 });
});

topologyApi.MapGet("/edges", (NetworkAnalysisEngine engine) =>
{
    var edges = engine.GetAllEdges()
        .Select(e => new { e.Source, e.Target, e.Weight });
    return Results.Ok(edges);
});

//-----------------------------------------------------------------------------
var attachmentApi = app.MapGroup("/api/attachments");

attachmentApi.MapPost("/upload", async (HttpContext context, IDataService dataService, CancellationToken ct) =>
{
    var form = await context.Request.ReadFormAsync(ct);
    var file = form.Files.GetFile("file");
    var sensorMAC = form["sensorMAC"].ToString();
    var fileType = form["fileType"].ToString();

    if (file == null || file.Length == 0 || string.IsNullOrWhiteSpace(sensorMAC))
    {
        return Results.BadRequest("File and Sensor MAC Address are required.");
    }

    var result = await dataService.UploadAttachmentAsync(sensorMAC, fileType, file, ct);
    return Results.Created($"/api/attachments/{result.Id}", result);
}).DisableAntiforgery();

attachmentApi.MapGet("/sensor/{sensorMAC}", async (string sensorMAC, IDataService dataService, CancellationToken ct) =>
{
    var attachments = await dataService.GetAttachmentsByMacAsync(sensorMAC, ct);
    return Results.Ok(attachments);
});

attachmentApi.MapGet("/download/{id:guid}", async (Guid id, IDataService dataService, CancellationToken ct) =>
{
    var fileResult = await dataService.GetFileByIdAsync(id, ct);
    if (fileResult == null) return Results.NotFound();

    return Results.File(fileResult.Value.Data, fileResult.Value.ContentType, fileResult.Value.FileName);
});

//run the application
app.Run();