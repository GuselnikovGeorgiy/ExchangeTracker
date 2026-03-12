using System.Text.Json.Serialization;

namespace Exchanges.Abstractions.Models;

public sealed class PriceExchangeResponseModel
{
    [JsonPropertyName("price")]
    public required decimal Price { get; init; }
}