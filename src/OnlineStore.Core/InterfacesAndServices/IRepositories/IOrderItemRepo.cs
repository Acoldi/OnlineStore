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
  public Task<OrderItem> CreateAndReturnEntityAsync(OrderItem param, CancellationToken? ct = null);

  public Task<bool> CreateMultipleAsync(List<OrderItem> OrderItems, CancellationToken? ct);
  /// <summary>
  /// Creates an order item record with its price in the database
  /// </summary>
  /// <param name="param"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  /// <exception cref="NotImplementedException"></exception>
  public new Task<int> CreateAsync(OrderItem param, CancellationToken? cancellationToken);

}
