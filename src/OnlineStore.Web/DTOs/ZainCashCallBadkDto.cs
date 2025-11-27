using System.Text.Json.Serialization;
using System.Transactions;
using Microsoft.Identity.Client;

namespace OnlineStore.Web.DTOs;

public class ZainCashCallBadkDto
{
  [JsonPropertyName("status")]
  public ZainCashPaymentStatus status;

  public int orderId { get; set; }

  [JsonPropertyName("id")]
  public string transactionID { get; set; } = string.Empty;

  [JsonPropertyName("msg")]
  public string message { get; set; } = string.Empty;
}
