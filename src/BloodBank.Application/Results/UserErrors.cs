using System.Net;

namespace BloodBank.Application.Results;

public class UserErrors
{
    public static readonly Error InvalidData = new(HttpStatusCode.BadRequest, "Não foi possível criar/atualizar o usuário. Dados informados inválidos.");
    public static readonly Error InvalidCredentials = new(HttpStatusCode.Unauthorized, "Email e/ou senha incorreto(s).");
}
