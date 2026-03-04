namespace Core.Abstractions.Models;

public sealed record GetPairPriceQueryOperationModel
{
    public required string PairName { get; init; }
    public required string ExchangeName { get; init; }
}
