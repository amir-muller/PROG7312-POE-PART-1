using Microsoft.EntityFrameworkCore; 

namespace Smart_X.Api.DataModels;

public class SensorDetails
{
    public Guid Id { get; set; }
    public string SensorMAC { get; set; } = string.Empty;
    public string SensorLocation { get; set; } = string.Empty;
    public string SensorName { get; set; } = string.Empty;
    public string SensorCategory { get; set; } = string.Empty;
}

public class SensorDataRecord
{
    public Guid Id { get; set; }
    public string SensorMAC { get; set; } = string.Empty;
    public double SensorValue { get; set; } 
    public DateTime TimeStamp { get; set; }
}

public class SensorAttachment
{
    public Guid Id { get; set; }
    public string SensorMAC { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public string FileType { get; set; } = string.Empty;
    public byte[] Data { get; set; } = Array.Empty<byte>();
    public DateTime UploadedAt { get; set; }

}

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<SensorDetails> SensorDetails => Set<SensorDetails>();
    public DbSet<SensorDataRecord> SensorDataRecords => Set<SensorDataRecord>();
    public DbSet<SensorAttachment> SensorAttachments => Set<SensorAttachment>();
}