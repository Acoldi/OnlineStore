using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Core.Entities;
using OnlineStore.Core.Interfaces.DataAccess;

namespace OnlineStore.Core.InterfacesAndServices.IRepositories;
public interface ICartRepo : IDataAccess<ShoppingCart, int>
{
  public Task<List<OrderItem>?> GetCartItemsAsync(Guid UserID);

  /// <summary>
  /// Deltes all items in the user cart and all thier related customizations (after delete trigger is applied on the DB 
  /// level that removes an item customizations)
  /// </summary>
  /// <param name="ShoppingCartID"></param>
  /// <returns></returns>
  public Task<bool> RemoveCartItemsAsync(int ShoppingCartID);

  public Task<int?> CreateAsync(Guid UserID, CancellationToken? cancellationToken = null);

}
