namespace Smart_X.Api.DataModels;

public record CreateDataRequest(string Source, string Payload);

public record DataResponse(Guid Id, string Source, string Payload, DateTime RequestedAt);
