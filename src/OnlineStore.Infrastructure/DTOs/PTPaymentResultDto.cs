using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Azure;

namespace OnlineStore.Infrastructure.DTOs;

public class PaymentResult
{
  [JsonPropertyName("response_status")]
  public required string ResponseStatus { get; set; }

  [JsonPropertyName("response_code")]
  public required string ResponseCode { get; set; }

  [JsonPropertyName("response_message")]
  public required string ResponseMessage { get; set; }

  [JsonPropertyName("acquirer_message")]
  public required string AcquirerMessage { get; set; }

  [JsonPropertyName("acquirer_rrn")]
  public required string AcquirerRRN { get; set; }

  [JsonPropertyName("transaction_time")]
  public DateTime TransactionTime { get; set; }
}
