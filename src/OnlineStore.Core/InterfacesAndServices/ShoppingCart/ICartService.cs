using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Core.Entities;
using OnlineStore.Core.InterfacesAndServices.DTOs;
using OnlineStore.Core.InterfacesAndServices.DTOs.Parameters;

namespace OnlineStore.Core.InterfacesAndServices.ShoppingCart;
public interface ICartService
{
  public Task PlaceOrder(CreateOrderParam createOrderParam, CancellationToken ct);

  public Task<List<CartItem>> LoadCartItems(int customerID, CancellationToken ct);


}
