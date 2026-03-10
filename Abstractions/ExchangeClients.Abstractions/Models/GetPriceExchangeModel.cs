namespace Exchanges.Abstractions.Models;

public sealed record GetPriceExchangeModel
{
    public required string PairName { get; init; }
}