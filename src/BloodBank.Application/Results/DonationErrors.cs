using System.Net;

namespace BloodBank.Application.Results;

public static class DonationErrors
{
    public static readonly Error DonationNotFound = new(HttpStatusCode.NotFound, "Doação não encontrada.");
}
