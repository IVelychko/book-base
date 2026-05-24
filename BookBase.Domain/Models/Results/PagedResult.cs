namespace BookBase.Domain.Models.Results;

public class PagedResult<T>
{
    public required IEnumerable<T> Items { get; set; }

    public required int PageNumber { get; set; }

    public required int PageSize { get; set; }

    public required int TotalItemsCount { get; set; }
}