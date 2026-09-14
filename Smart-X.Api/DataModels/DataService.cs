using Microsoft.EntityFrameworkCore;

namespace Smart_X.Api.DataModels;

public interface IDataService
{
    //sensor details
    Task<SensorDetailsResponse> SaveSensorDetailsAsync(CreateSensorDetailsRequest request, CancellationToken ct = default);
    Task<List<SensorDetailsResponse>> GetAllSensorDetailsAsync(CancellationToken ct = default);

    // Sensor Readings
    Task<SensorDataResponse> SaveDataAsync(CreateSensorDataRequest request, CancellationToken ct = default);
    Task<List<SensorDataResponse>> GetAllDataAsync(CancellationToken ct = default);
}

public class DataService : IDataService
{
    private readonly ApplicationDbContext _db;

    public DataService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<SensorDetailsResponse> SaveSensorDetailsAsync(CreateSensorDetailsRequest request, CancellationToken ct = default)
    {
        var sensor = new SensorDetails
        {
            Id = Guid.NewGuid(),
            SensorMAC = request.SensorMAC,
            SensorLocation = request.SensorLocation,
            SensorName = request.SensorName,
            SensorCategory = request.SensorCategory
        };

        _db.SensorDetails.Add(sensor);
        await _db.SaveChangesAsync(ct);

        return new SensorDetailsResponse(sensor.Id, sensor.SensorMAC, sensor.SensorLocation, sensor.SensorName, sensor.SensorCategory);
    }

    public async Task<List<SensorDetailsResponse>> GetAllSensorDetailsAsync(CancellationToken ct = default)
    {
        return await _db.SensorDetails
            .AsNoTracking()
            .Select(s => new SensorDetailsResponse(s.Id, s.SensorMAC, s.SensorLocation, s.SensorName, s.SensorCategory))
            .ToListAsync(ct);
    }


    public async Task<SensorDataResponse> SaveDataAsync(CreateSensorDataRequest request, CancellationToken ct = default)
    {
        var record = new SensorDataRecord
        {
            Id = Guid.NewGuid(),
            SensorMAC = request.SensorMAC,
            SensorValue = request.SensorValue,
            TimeStamp = DateTime.UtcNow
        };

        _db.SensorDataRecords.Add(record);
        await _db.SaveChangesAsync(ct);

        return new SensorDataResponse(record.Id, record.SensorMAC, record.SensorValue, record.TimeStamp);
    }

    public async Task<List<SensorDataResponse>> GetAllDataAsync(CancellationToken ct = default)
    {
        return await _db.SensorDataRecords
            .AsNoTracking()
            .Select(r => new SensorDataResponse(r.Id, r.SensorMAC, r.SensorValue, r.TimeStamp))
            .ToListAsync(ct);
    }
}
