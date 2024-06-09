using System.Linq.Expressions;

namespace BuildingBlocks.Specifications;

public class WhereExpression<T>
{
    public Expression<Func<T, bool>> Criteria { get; set; }
}
