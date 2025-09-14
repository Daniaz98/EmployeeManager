namespace EmployeeManager.Domain.ValueObjects;

public class SearchCriteria : ValueObject
{
    public string SearchTerm { get; }
    public int Page { get; }
    public int PageSize { get; }

    public SearchCriteria(
        string searchTerm = null,
        int page = 1,
        int pageSize = 10
        )
    {
        SearchTerm = searchTerm.Trim();
        Page = Math.Max(1, page);
        PageSize = Math.Min(100, Math.Max(1, pageSize));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return SearchTerm;
        yield return Page;
        yield return PageSize;
    }
}

public abstract class ValueObject
{
    protected abstract IEnumerable<object> GetEqualityComponents();

    public override bool Equals(object obj)
    {
        if (obj == null || obj.GetType() != GetType())
            return false;

        var other = (ValueObject)obj;
        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(x => x?.GetHashCode() ?? 0)
            .Aggregate((x, y) => x ^ y);
    }
}