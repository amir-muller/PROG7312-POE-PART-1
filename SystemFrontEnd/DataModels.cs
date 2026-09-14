namespace SystemFrontEnd;

//sensor data endpoint
public record CreateSensorDataRequest(string SensorMAC, double SensorValue);
public record SensorDataResponse(Guid Id, string SensorMAC, double SensorValue, DateTime TimeStamp);

//sensor details endpoint
public record CreateSensorDetailsRequest(string SensorMAC, string SensorLocation, string SensorName, string SensorCategory);
public record SensorDetailsResponse(Guid Id, string SensorMAC, string SensorLocation, string SensorName, string SensorCategory);