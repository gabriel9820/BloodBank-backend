namespace BloodBank.Core.Models;

public class PagedResult<T>(
    IEnumerable<T> data,
    int pageNumber,
    int pageSize,
    int totalRecords,
    int totalPages) where T : class
{
    public IEnumerable<T> Data { get; init; } = data;
    public int PageNumber { get; init; } = pageNumber;
    public int PageSize { get; init; } = pageSize;
    public int TotalRecords { get; init; } = totalRecords;
    public int TotalPages { get; init; } = totalPages;
}
