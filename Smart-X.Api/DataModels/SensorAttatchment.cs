namespace Smart_X.Api.DataModels
{
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
}
