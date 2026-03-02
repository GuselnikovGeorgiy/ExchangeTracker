namespace Core.Abstractions.Models;

public sealed record GetPriceQueryOperationModel
{
    public required string PairName { get; init; }
    public required string ExchangeName { get; init; }
}
