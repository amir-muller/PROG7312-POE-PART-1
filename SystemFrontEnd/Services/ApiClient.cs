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
}
