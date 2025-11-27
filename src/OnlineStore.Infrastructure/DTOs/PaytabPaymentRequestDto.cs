using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using OnlineStore.Core.DTOs.PaymentDtos;

namespace OnlineStore.Infrastructure.DTOs;
public class PaytabPaymentRequestDto
{
  [JsonPropertyName("profile_id")]
  public string ProfileId { get; set; } = string.Empty;

  [JsonPropertyName("tran_type")]
  public string TranType { get; set; } = string.Empty;

  [JsonPropertyName("tran_class")]
  public string TranClass { get; set; } = string.Empty;

  [JsonPropertyName("cart_id")]
  public string CartId { get; set; } = string.Empty;

  [JsonPropertyName("cart_currency")]
  public string CartCurrency { get; set; } = string.Empty;

  [JsonPropertyName("cart_amount")]
  public decimal CartAmount { get; set; }

  [JsonPropertyName("cart_description")]
  public string CartDescription { get; set; } = string.Empty;

  [JsonPropertyName("paypage_lang")]
  public string PaypageLang { get; set; } = string.Empty;

  [JsonPropertyName("customer_details")]
  public CustomerDetailsDto customerDetailsDto { get; set; } = new CustomerDetailsDto();

  [JsonPropertyName("callback")]
  public string CallBack { get; set; } = string.Empty;

  [JsonPropertyName("return")]
  public string Return { get; set; } = string.Empty;

}
