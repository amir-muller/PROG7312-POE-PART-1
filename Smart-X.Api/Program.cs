using Microsoft.EntityFrameworkCore;
using Smart_X.Api.DataModels;

var builder = WebApplication.CreateBuilder(args);

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
    db.Database.EnsureCreated();
}

//defining api endpoints
var api = app.MapGroup("/api/data");

//-----------------------------------------------------------------------------
//sensor details
//POST /api/sensors 
var sensorApi = app.MapGroup("/api/sensors");

sensorApi.MapPost("/", async (CreateSensorDetailsRequest request, IDataService dataService, CancellationToken ct) =>
{
    if (string.IsNullOrWhiteSpace(request.SensorMAC))
        return Results.BadRequest("MAC address is required.");

    var result = await dataService.SaveSensorDetailsAsync(request, ct);
    return Results.Created($"/api/sensors/{result.Id}", result);
});

//GET /api/sensors
sensorApi.MapGet("/", async (IDataService dataService, CancellationToken ct) =>
{
    var sensors = await dataService.GetAllSensorDetailsAsync(ct);
    return Results.Ok(sensors);
});

//-----------------------------------------------------------------------------

//sensor data
// POST /api/data form pushes data here
api.MapPost("/", async (CreateSensorDataRequest request, IDataService dataService, CancellationToken ct) =>
{
    if (string.IsNullOrWhiteSpace(request.SensorMAC))
    {
        return Results.BadRequest("Sensor MAC address cannot be empty.");
    }

    var result = await dataService.SaveDataAsync(request, ct);
    return Results.Created($"/api/data/{result.Id}", result);
});


//GET /api/data form retrieves data from this
api.MapGet("/", async (IDataService dataService, CancellationToken ct) =>
{
    var data = await dataService.GetAllDataAsync(ct);
    return Results.Ok(data);
});

//-----------------------------------------------------------------------------
var attachmentApi = app.MapGroup("/api/attachments");

//upload files / attachemtns
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

//get file meta data
attachmentApi.MapGet("/sensor/{sensorMAC}", async (string sensorMAC, IDataService dataService, CancellationToken ct) =>
{
    var attachments = await dataService.GetAttachmentsByMacAsync(sensorMAC, ct);
    return Results.Ok(attachments);
});

//view by id
attachmentApi.MapGet("/download/{id:guid}", async (Guid id, IDataService dataService, CancellationToken ct) =>
{
    var fileResult = await dataService.GetFileByIdAsync(id, ct);
    if (fileResult == null) return Results.NotFound();

    return Results.File(fileResult.Value.Data, fileResult.Value.ContentType, fileResult.Value.FileName);
});


//-----------------------------------------------------------------------------



//run the application
app.Run();
