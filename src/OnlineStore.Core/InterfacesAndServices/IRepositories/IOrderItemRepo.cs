using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Extensions.Msal;
using OnlineStore.Core.Entities;
using OnlineStore.Core.Interfaces.DataAccess;

namespace OnlineStore.Core.InterfacesAndServices.IRepositories;
public interface IOrderItemRepo : IDataAccess<OrderItem, int>
{
  public Task<bool> CreateMultipleAsync(List<OrderItem> OrderItems, CancellationToken? ct);

}
