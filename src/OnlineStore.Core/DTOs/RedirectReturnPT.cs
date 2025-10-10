namespace OnlineStore.Web.DTOs;

using System.Text.Json.Serialization; // Make sure to include this namespace
using OnlineStore.Core.Enums;

public class PaytabsRedirectReturnResponse
{
  [JsonPropertyName("tran_ref")]
  public required string TranRef { get; set; }

  [JsonPropertyName("cart_id")]
  public required int CartId { get; set; }

  [JsonPropertyName("cart_description")]
  public required string CartDescription { get; set; }

  [JsonPropertyName("cart_currency")]
  public required enCurrencies CartCurrency { get; set; }

  [JsonPropertyName("cart_amount")]
  public required decimal CartAmount { get; set; }

  [JsonPropertyName("callback")]
  public required string Callback { get; set; }

  [JsonPropertyName("return")]
  public required string Return { get; set; }

  [JsonPropertyName("redirect_url")]
  public required string RedirectUrl { get; set; }
}
