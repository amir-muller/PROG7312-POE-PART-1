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

    // file attachments
    Task<SensorAttachmentResponse> UploadAttachmentAsync(string sensorMAC, string fileType, IFormFile file, CancellationToken ct = default);
    Task<List<SensorAttachmentResponse>> GetAttachmentsByMacAsync(string sensorMAC, CancellationToken ct = default);
    Task<(byte[] Data, string ContentType, string FileName)?> GetFileByIdAsync(Guid id, CancellationToken ct = default);
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


    //file attachemnts
    public async Task<SensorAttachmentResponse> UploadAttachmentAsync(string sensorMAC, string fileType, IFormFile file, CancellationToken ct = default)
    {
        using var ms = new MemoryStream();
        await file.CopyToAsync(ms, ct);

        var attachment = new SensorAttachment
        {
            Id = Guid.NewGuid(),
            SensorMAC = sensorMAC,
            FileName = file.FileName,
            ContentType = file.ContentType,
            FileType = fileType,
            Data = ms.ToArray(),
            UploadedAt = DateTime.UtcNow
        };

        _db.SensorAttachments.Add(attachment);
        await _db.SaveChangesAsync(ct);

        return new SensorAttachmentResponse(
            attachment.Id,
            attachment.SensorMAC,
            attachment.FileName,
            attachment.ContentType,
            attachment.FileType,
            attachment.UploadedAt
        );
    }

    public async Task<List<SensorAttachmentResponse>> GetAttachmentsByMacAsync(string sensorMAC, CancellationToken ct = default)
    {
        return await _db.SensorAttachments
            .AsNoTracking()
            .Where(a => a.SensorMAC == sensorMAC)
            .Select(a => new SensorAttachmentResponse(a.Id, a.SensorMAC, a.FileName, a.ContentType, a.FileType, a.UploadedAt))
            .ToListAsync(ct);
    }

    public async Task<(byte[] Data, string ContentType, string FileName)?> GetFileByIdAsync(Guid id, CancellationToken ct = default)
    {
        var file = await _db.SensorAttachments.FindAsync(new object[] { id }, ct);
        if (file == null) return null;

        return (file.Data, file.ContentType, file.FileName);
    }
}
