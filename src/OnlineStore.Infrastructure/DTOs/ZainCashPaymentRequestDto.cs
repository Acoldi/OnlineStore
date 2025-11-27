using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Infrastructure.DTOs;
public class ZainCashPaymentRequestDto
{
  public int amount { get; set; }
  
  public string serviceType  { get; set; } = string.Empty;
  
  public string msisdn { get; set; } = string.Empty;
  
  public int orderId { get; set; }
  
  public string redirectUrl { get; set; } = string.Empty;

}
