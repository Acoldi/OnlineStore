using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Core.Entities;
using OnlineStore.Core.Enums;

namespace OnlineStore.Core.InterfacesAndServices.Payment;
public interface IPaymentService
{
  /// <summary>
  /// Generate a payment URL users can be redirected to and complete their payment
  /// </summary>
  /// <param name="order"></param>
  /// <returns></returns>
  public Task<string> GenerateZainCashURL(Order order);

  public Task<Dictionary<string, string>> GetZaincashCallBackResults(string token);

  public Task<enPaymentStatus> CheckZainCashTransactionStatus(string transactionID);


}
