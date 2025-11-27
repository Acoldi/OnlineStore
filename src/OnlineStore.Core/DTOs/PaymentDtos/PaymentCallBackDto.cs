using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Core.Enums;

namespace OnlineStore.Core.DTOs.PaymentDtos;
public class PaymentCallBackDto
{
  public string TransactionID { get; set; } = string.Empty;
  public int OrderID { get; set; }
  public decimal CartAmount { get; set; }
  public string CartCurrency { get; set; } = string.Empty;
  public CustomerDetailsDto CustomerDetails { get; set; } = new CustomerDetailsDto();
  public enPaymentStatus PaymemtStatus { get; set; }

}
