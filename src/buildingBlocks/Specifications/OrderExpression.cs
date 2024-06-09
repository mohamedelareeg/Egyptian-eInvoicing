using System.Linq.Expressions;

namespace BuildingBlocks.Specifications;

public class OrderExpression<T>
{
    public Expression<Func<T, object>> KeySelector { get; set; }
    public OrderTypeEnum OrderType { get; set; }
}
