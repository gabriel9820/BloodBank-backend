using System.Net;

namespace BloodBank.Application.Results;

public static class HospitalErrors
{
    public static readonly Error HospitalNotFound = new(HttpStatusCode.NotFound, "Hospital não encontrado.");
}
