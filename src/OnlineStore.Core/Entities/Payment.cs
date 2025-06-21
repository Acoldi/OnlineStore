using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Core.Enums;

namespace OnlineStore.Core.Entities;
public class Payment
{
    public int ID { get; set; }
    public required int OrderID { get; set; }
    public required string TransactionID { get; set; }
    public required decimal Amount { get; set; }
    public required enPaymentStatus Status { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required enPaymentMethod Method { get; set; }
    public required DateTime UpdatedAt { get; set; }

    [SetsRequiredMembers]
    public Payment(int orderID, string transactionID, decimal amount, enPaymentStatus status, DateTime createdAt, enPaymentMethod method, DateTime updatedAt)
    {
      OrderID = orderID;
      TransactionID = transactionID;
      Amount = amount;
      Status = status;
      CreatedAt = createdAt;
      Method = method;
      UpdatedAt = updatedAt;
    }

    public Payment() { }
}
