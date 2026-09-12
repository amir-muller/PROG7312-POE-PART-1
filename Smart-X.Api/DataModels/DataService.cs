using Microsoft.EntityFrameworkCore;

namespace Smart_X.Api.DataModels;

public interface IDataService
{
    Task<DataResponse> SaveDataAsync (CreateDataRequest request, CancellationToken ct = default);
    Task<List<DataResponse>> GetAllDataAsync(CancellationToken ct = default);
}

public class DataService : IDataService
{
    private readonly ApplicationDbContext _db;
    public DataService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<DataResponse> SaveDataAsync(CreateDataRequest request, CancellationToken ct = default)
    {
        var record = new DataRecord
        {
            Id = Guid.NewGuid(),
            Source = request.Source,
            Payload = request.Payload,
            RequestedAt = DateTime.UtcNow
        };

        _db.DataRecords.Add(record);
        await _db.SaveChangesAsync(ct);

        return new DataResponse(record.Id, record.Source, record.Payload, record.RequestedAt);
    }

    public async Task<List<DataResponse>> GetAllDataAsync(CancellationToken ct = default)
    {
        return await _db.DataRecords
            .AsNoTracking()
            .Select(r => new DataResponse(r.Id, r.Source, r.Payload, r.RequestedAt))
            .ToListAsync(ct);
    }


}
