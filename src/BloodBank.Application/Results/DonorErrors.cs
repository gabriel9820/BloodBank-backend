using System.Net;

namespace BloodBank.Application.Results;

public static class DonorErrors
{
    public static readonly Error CellPhoneNumberAlreadyInUse = new(HttpStatusCode.BadRequest, "O número de celular informado já está em uso.");
    public static readonly Error EmailAlreadyInUse = new(HttpStatusCode.BadRequest, "O e-mail informado já está em uso.");
    public static readonly Error DonorNotFound = new(HttpStatusCode.NotFound, "Doador não encontrado.");
}
