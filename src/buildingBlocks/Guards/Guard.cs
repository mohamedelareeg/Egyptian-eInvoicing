using BuildingBlocks.Domain.Shared.Guard;

namespace BuildingBlocks.Domain.Shared.Guards;
public class Guard : IGuardClause
{
    public static IGuardClause Against { get; } = new Guard();

    private Guard() { }
}
