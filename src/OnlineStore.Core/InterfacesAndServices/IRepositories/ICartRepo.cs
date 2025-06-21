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

  public Task<bool> RemoveCartItemsAsync(int ShoppingCartID);

  public Task<int?> CreateAsync(Guid UserID, CancellationToken? cancellationToken = null);

}
