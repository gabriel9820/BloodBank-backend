using System.Net;

namespace BloodBank.Application.Results;

public class Error(HttpStatusCode code, string message)
{
    public int Code { get; init; } = (int)code;
    public string Message { get; init; } = message;

    public static readonly Error None = new(default, string.Empty);
}
