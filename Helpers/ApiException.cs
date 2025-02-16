using System.Net;

namespace NftApi;

public class ApiException(HttpStatusCode statuCode, string message, string? details)
{
    public string StatusCode { get; set; } = statuCode.ToString();
    public string Message { get; set; } = message;
    public string? Details { get; set; } = details;
}
