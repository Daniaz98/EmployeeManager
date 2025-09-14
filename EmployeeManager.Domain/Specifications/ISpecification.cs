using System.Linq.Expressions;

namespace EmployeeManager.Domain;

public interface ISpecification<T>
{
    bool IsSatisfiedBy(T entity);
    Expression<Func<T, bool>> ToExpression();
}