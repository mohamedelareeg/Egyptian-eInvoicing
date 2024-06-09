using System.Linq.Expressions;

namespace BuildingBlocks.Specifications;
public interface ISpecification<T>
{
    List<WhereExpression<T>> WhereExpressions { get; }
    List<Expression<Func<T, object>>> Includes { get; }
    List<string> IncludeStrings { get; }
    List<OrderExpression<T>> OrderByExpressions { get; }

    int Take { get; }
    int Skip { get; }
    bool isPagingEnabled { get; }
}
