using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Core.Interfaces.DataAccess;
using OnlineStore.Core.InterfacesAndServices.DTOs.Parameters;

namespace OnlineStore.Core.InterfacesAndServices.Order;
public class OrderService : IOrderService
{
  IDataAccess _dataAccess;
  public OrderService(IDataAccess dataAccess)
  {
    _dataAccess = dataAccess;
  }

  public async Task<int> Creaate(CreateOrderParam createOrderParam, CancellationToken ct)
  {
    return await _dataAccess.CreateAsync<int>("[SP_AddOrder]", System.Data.CommandType.StoredProcedure, ct, createOrderParam);
  }
}
