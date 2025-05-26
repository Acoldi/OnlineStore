using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Extensions.Msal;
using OnlineStore.Core.InterfacesAndServices.DTOs.Parameters;

namespace OnlineStore.Core.InterfacesAndServices.OrderItem;
public interface IOrderItemService
{
  public Task<int> create(CreateOrderItemParam createOrderItemParam, CancellationToken ct);
}
