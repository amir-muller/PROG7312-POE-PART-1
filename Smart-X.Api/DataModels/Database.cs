using Microsoft.EntityFrameworkCore; 

namespace Smart_X.Api.DataModels;

public class DataRecord
{
    public Guid Id { get; set; }
    public string Source { get; set; } = string.Empty;
    public string Payload { get; set; } = string.Empty;
    public DateTime RequestedAt { get; set; }
}

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<DataRecord> DataRecords => Set<DataRecord>();

}


