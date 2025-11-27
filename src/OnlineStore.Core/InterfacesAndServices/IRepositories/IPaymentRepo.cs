using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using OnlineStore.Core.Entities;
using OnlineStore.Core.Interfaces.DataAccess;

namespace OnlineStore.Core.InterfacesAndServices.IRepositories;
public interface IPaymentRepo : IDataAccess<Payment, int>
{
  public Task<Entities.Payment?> GetByTransactionID(string transactionID, CancellationToken? ct = null);
}
