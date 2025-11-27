namespace OnlineStore.Infrastructure.Options;

public class ZainCashOptions
{
  public const string ZainCash = "ZainCash";
  
  public string serviceType { get; set; } = string.Empty;

  public string redirectUrl { get; set; } = string.Empty;

  public string msisdn { get; set; } = string.Empty;

  public string secret { get; set; } = string.Empty;

  public string merchantId { get; set; } = string.Empty;

  public string currency { get; set; } = string.Empty;

}
