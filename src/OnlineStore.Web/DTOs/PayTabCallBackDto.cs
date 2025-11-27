
using System.Text.Json.Serialization;

namespace OnlineStore.Web.DTOs;

public class PayTabCallBackDto
{
  public string TranRef { get; set; } = string.Empty;

  public string CartId { get; set; } = string.Empty;

  public string CartDescription { get; set; } = string.Empty;

  public CartCurrency CartCurrency { get; set; }

  public decimal CartAmount { get; set; }

  public CartCurrency TranCurrency { get; set; }

  public string TranTotal { get; set; } = string.Empty;

  [JsonPropertyName("customer_details")]
  public CustomerDetailsCallBackDto CustomerDetails { get; set; } = new();

  [JsonPropertyName("shipping_details")]
  public ShippingDetailsCallBackDto ShippingDetails { get; set; } = new();

  [JsonPropertyName("payment_result")]
  public PaytabPaymentResultDto PaymentResult { get; set; } = new();

  [JsonPropertyName("payment_info")]
  public PaytabPaymentInfoCallBackDto PaymentInfo { get; set; } = new();

  public string Token { get; set; } = string.Empty;
}

public class CustomerDetailsCallBackDto
{
  public string Name { get; set; } = string.Empty;

  public string Email { get; set; } = string.Empty;

  public string Phone { get; set; } = string.Empty;

  public string Street1 { get; set; } = string.Empty;

  public string City { get; set; } = string.Empty;

  public string State { get; set; } = string.Empty;

  public string Country { get; set; } = string.Empty;

  public string Ip { get; set; } = string.Empty;
}

public class ShippingDetailsCallBackDto
{
  public string Name { get; set; } = string.Empty;

  public string Email { get; set; } = string.Empty;

  public string Phone { get; set; } = string.Empty;

  public string Street1 { get; set; } = string.Empty;

  public string City { get; set; } = string.Empty;

  public string State { get; set; } = string.Empty;

  public string Country { get; set; } = string.Empty;
}

public class PaytabPaymentResultDto
{
  public ResponseStatus ResponseStatus { get; set; }

  public string ResponseCode { get; set; } = string.Empty;

  public string ResponseMessage { get; set; } = string.Empty;

  public string CvvResult { get; set; } = string.Empty;

  public string AvsResult { get; set; } = string.Empty;

  public DateTime TransactionTime { get; set; }

  public override string ToString()
  {
    return $"Transaction Time: {TransactionTime}\n Response Status: {ResponseStatus}\n Response Code: {ResponseCode}\n Response Message: {ResponseMessage}";
  }
}

public class PaytabPaymentInfoCallBackDto
{
  public string CardType { get; set; } = string.Empty;

  public string CardScheme { get; set; } = string.Empty;

  public string PaymentDescription { get; set; } = string.Empty;

  public int ExpiryMonth { get; set; }

  public int ExpiryYear { get; set; }
}
