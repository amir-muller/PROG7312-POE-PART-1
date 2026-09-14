using System.Net.Http.Json;

namespace SystemFrontEnd.Services;

public class ApiClient
{
    private static readonly HttpClient _httpClient = new HttpClient
    {
        BaseAddress = new Uri("http://localhost:5276/") 
    };

    //sensor details methods
    public async Task<SensorDetailsResponse?> PostSensorDetailsAsync(string mac, string location, string name, string category)
    {
        var request = new CreateSensorDetailsRequest(mac, location, name, category);
        var response = await _httpClient.PostAsJsonAsync("api/sensors", request);

        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<SensorDetailsResponse>();

        var error = await response.Content.ReadAsStringAsync();
        throw new Exception($"API Error ({response.StatusCode}): {error}");
    }

    public async Task<List<SensorDetailsResponse>?> GetAllSensorDetailsAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<SensorDetailsResponse>>("api/sensors");
    }



    //sensor data methods
    public async Task<List<SensorDataResponse>?> GetAllDataAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<List<SensorDataResponse>>("api/data");
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to fetch data: {ex.Message}", ex);
        }
    }

    public async Task<SensorDataResponse?> PostDataAsync(string sensorMAC, double sensorValue)
    {
        var request = new CreateSensorDataRequest(sensorMAC, sensorValue);
        var response = await _httpClient.PostAsJsonAsync("api/data", request);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<SensorDataResponse>();
        }

        var error = await response.Content.ReadAsStringAsync();
        throw new Exception($"API Error ({response.StatusCode}): {error}");
    }

    //sensor attachment methods
    public async Task<SensorAttachmentResponse?> UploadFileAsync(string sensorMAC, string fileType, string filePath)
    {
        using var content = new MultipartFormDataContent();
        using var fileStream = File.OpenRead(filePath);
        using var streamContent = new StreamContent(fileStream);

        content.Add(new StringContent(sensorMAC), "sensorMAC");
        content.Add(new StringContent(fileType), "fileType");
        content.Add(streamContent, "file", Path.GetFileName(filePath));

        var response = await _httpClient.PostAsync("api/attachments/upload", content);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<SensorAttachmentResponse>();
        }

        var error = await response.Content.ReadAsStringAsync();
        throw new Exception($"Upload failed: {error}");
    }

    public async Task<List<SensorAttachmentResponse>?> GetAttachmentsByMacAsync(string sensorMAC)
    {
        return await _httpClient.GetFromJsonAsync<List<SensorAttachmentResponse>>($"api/attachments/sensor/{sensorMAC}");
    }
}
