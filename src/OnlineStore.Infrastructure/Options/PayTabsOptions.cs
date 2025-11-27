namespace OnlineStore.Infrastructure.Options;

public class PayTabsOptions
{
  public const string PayTabs = "PayTabs";
  public string api_key { get; set; } = string.Empty;
  public string server_key { get; set; } = string.Empty;
  public string profile_id { get; set; } = string.Empty;
  public string tran_type { get; set; } = string.Empty;
  public string tran_class { get; set; } = string.Empty;
  public string paypage_lang { get; set; } = string.Empty;
  public string cart_currency { get; set; } = string.Empty;
  public string callback { get; set; } = string.Empty;
  public string BaseAddress { get; set; } = string.Empty;
}
