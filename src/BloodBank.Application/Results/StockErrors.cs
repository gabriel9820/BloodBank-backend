using System.Net;

namespace BloodBank.Application.Results;

public static class StockErrors
{
    public static readonly Error InsufficientStock = new(HttpStatusCode.BadRequest, "Estoque insuficiente.");
    public static readonly Error StockNotFound = new(HttpStatusCode.NotFound, "Estoque não encontrado.");
}
