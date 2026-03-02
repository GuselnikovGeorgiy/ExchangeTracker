namespace Core.Abstractions.Models;

public class GetPriceQueryOperationModel
{
    public required string PairName { get; init; }
    public required string ExchangeName { get; init; }
}
