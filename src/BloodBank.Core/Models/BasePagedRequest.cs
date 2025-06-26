namespace BloodBank.Core.Models;

public class BasePagedRequest
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
