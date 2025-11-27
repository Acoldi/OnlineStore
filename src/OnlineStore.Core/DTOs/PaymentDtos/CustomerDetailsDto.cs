using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.DTOs.PaymentDtos;
public class CustomerDetailsDto
{
  public string name { get; set; } = string.Empty;
  public string email { get; set; } = string.Empty;
  public string phone { get; set; } = string.Empty;
  public string street1 { get; set; } = string.Empty;
  public string city { get; set; } = string.Empty;
  public string state { get; set; } = string.Empty; // 2 Letters
  public string country { get; set; } = string.Empty; // 2 Letters
  public string zip { get; set; } = string.Empty;

}
