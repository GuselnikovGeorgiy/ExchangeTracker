namespace Core.Abstractions.Models;

public sealed record PairPriceQueryOperationModel
{
    public required decimal Price { get; init; }
}