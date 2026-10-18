using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Smart_X.Api.DataModels;

namespace Smart_X.Api.Services;

public class SensorSimulatorService : BackgroundService
{
    readonly IServiceProvider _serviceProvider;

    public SensorSimulatorService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Delay(3000, stoppingToken);

        using var scope = _serviceProvider.CreateScope();
        var dataService = scope.ServiceProvider.GetRequiredService<IDataService>();
        var sensorManager = scope.ServiceProvider.GetRequiredService<RealTimeSensorManager>();

        //for it no sensor reg
        var existingSensors = await dataService.GetAllSensorDetailsAsync(stoppingToken);

        if (!existingSensors.Any())
        {
            var seedSensors = new[]
            {
                new CreateSensorDetailsRequest("AA:BB:CC:DD:EE:01", "Greenhouse Zone A", "Temp Sensor Alpha", "Temperature"),
                new CreateSensorDetailsRequest("AA:BB:CC:DD:EE:02", "Field Sector 2", "Moisture Monitor Beta", "Moisture")
            };

            foreach (var seed in seedSensors)
            {
                var created = await dataService.SaveSensorDetailsAsync(seed, stoppingToken);
                sensorManager.RegisterDevice(created);
            }
        }
        else
        {
            foreach (var sensor in existingSensors)
            {
                sensorManager.RegisterDevice(sensor);
            }
        }

        var random = new Random();

        
        while (!stoppingToken.IsCancellationRequested)
        {
            var currentSensors = await dataService.GetAllSensorDetailsAsync(stoppingToken);

            foreach (var sensor in currentSensors)
            {
                //gen readings for each catagory
                double simulatedValue = sensor.SensorCategory.Equals("Temperature", StringComparison.OrdinalIgnoreCase)
                    ? Math.Round(random.NextDouble() * 25 + 15, 2) 
                    : Math.Round(random.NextDouble() * 60 + 20, 2); 

                var dataRequest = new CreateSensorDataRequest(sensor.SensorMAC, simulatedValue);
                var dataResponse = await dataService.SaveDataAsync(dataRequest, stoppingToken);

                bool isCritical = simulatedValue > 75;
                sensorManager.EnqueueTelemetry(dataResponse, isCritical: isCritical, priority: isCritical ? 1 : 5);
            }

            await Task.Delay(3000, stoppingToken);
        }
    }
}