using System.ComponentModel;
using System.Text.Json.Serialization;
using OnlineStore.Core.ValueObjects;

namespace OnlineStore.Web.DTOs;

public class PayTabCallBackReturnResponse
{
  [JsonPropertyName("tran_ref")]
  public required string TransferID { get; set; }

  [JsonPropertyName("cart_id")]
  public required string OrderID { get; set; }

  [JsonPropertyName("cart_description")]
  public required string OrderDescription { get; set; }

  [JsonPropertyName("cart_currency")]
  [DefaultValue("IQD")]
  public required string CartCurrency { get; set; }
  [JsonPropertyName("tran_currency")]
  public required string TransferCurrency { get; set; }

  [JsonPropertyName("cart_amount")]
  public required decimal Amount { get; set; }
  [JsonPropertyName("tran_total")]
  public required decimal TransferTotalAmount { get; set; }

  [JsonPropertyName("customer_details")]
  public required CustomerDetails CustomerDetails { get; set; }

  [JsonPropertyName("payment_result")]
  public required PaymentResult paymentResult { get; set; }

}
