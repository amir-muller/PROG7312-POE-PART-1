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

// POST /api/data form pushes data here
api.MapPost("/", async (CreateDataRequest request, IDataService dataService, CancellationToken ct) =>
{
    if (string.IsNullOrWhiteSpace(request.Payload))
    {
        return Results.BadRequest("Payload cannot be empty.");
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


//run the application
app.Run();
