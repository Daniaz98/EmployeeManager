namespace EmployeeManager.Domain.ValueObjects;

public class SearchResult<T> : ValueObject
{
    public IReadOnlyList<T> Items { get; }
    public int TotalCount { get; }
    public int Page { get; }
    public int PageSize { get; }
    public int TotalPages { get; }
    public bool HasNextPage { get; }
    public bool HasPreviousPage { get; }

    public SearchResult(IEnumerable<T> items, int totalCount, int page, int pageSize)
    {
        Items = items?.ToList().AsReadOnly() ?? new List<T>().AsReadOnly();
        TotalCount = Math.Max(0, totalCount);
        Page = Math.Max(1, page);
        PageSize = Math.Max(1, pageSize);
        TotalPages = PageSize > 0 ? (int)Math.Ceiling((double)TotalCount / PageSize) : 0;
        HasNextPage = Page < TotalPages;
        HasPreviousPage = Page > 1;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Items;
        yield return TotalCount;
        yield return Page;
        yield return PageSize;
    }
}