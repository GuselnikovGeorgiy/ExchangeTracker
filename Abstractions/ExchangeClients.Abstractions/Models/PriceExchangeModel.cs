namespace Exchanges.Abstractions.Models;

public sealed record PriceExchangeModel
{
    public required decimal Price { get; init; }
}