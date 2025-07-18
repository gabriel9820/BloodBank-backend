using System.Net;

namespace BloodBank.Application.Results;

public static class StockErrors
{
    public static readonly Error InsufficientStock = new(HttpStatusCode.BadRequest, "Estoque insuficiente.");
}
