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
  /// Removes all items in the user cart and all thier related customizations (Instead-of-delete trigger is applied on the DB 
  /// level that removes items customizations before removing the items)
  /// </summary>
  /// <param name="ShoppingCartID"></param>
  /// <returns></returns>
  public Task<bool> RemoveCartItemsAsync(int ShoppingCartID);
  /// <summary>
  /// Removes the specified items from the shopping cart.
  /// </summary>
  /// <param name="ItemIDs"></param>
  /// <param name="ShoppingCartID"></param>
  /// <returns></returns>
  public Task<bool> RemoveCartItemsAsync(List<int> ItemIDs, int ShoppingCartID);

  public Task<int> CreateAsync(Guid UserID, CancellationToken? cancellationToken = null);

}
