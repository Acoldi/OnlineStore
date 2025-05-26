using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Core.InterfacesAndServices.DTOs.Parameters;

namespace OnlineStore.Core.InterfacesAndServices.Order;
public interface IOrderService
{
  public Task<int> Creaate(CreateOrderParam createOrderParam, CancellationToken ct);

}
