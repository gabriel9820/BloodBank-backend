using System.Net;

namespace BloodBank.Application.Results;

public static class BloodTransferErrors
{
    public static readonly Error BloodTransferNotFound = new(HttpStatusCode.NotFound, "Transferência não encontrada.");
}
