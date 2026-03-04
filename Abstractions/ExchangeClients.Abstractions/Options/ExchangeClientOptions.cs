namespace Exchanges.Abstractions.Options;

public sealed record ExchangeClientOptions
{
    public required string BinanceUrl { get; init; }
    public required string BybitUrl { get; init; }
}