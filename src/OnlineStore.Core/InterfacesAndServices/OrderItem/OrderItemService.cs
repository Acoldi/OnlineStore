using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;
using OnlineStore.Core.Interfaces.DataAccess;
using OnlineStore.Core.InterfacesAndServices.DTOs.Parameters;

namespace OnlineStore.Core.InterfacesAndServices.OrderItem;
public class OrderItemService : IOrderItemService
{
  IDataAccess _dataAccess;
  public OrderItemService(IDataAccess dataAccess)
  {
    _dataAccess = dataAccess;
  }
  public async Task<int> create(CreateOrderItemParam createOrderItemParam, CancellationToken ct)
  {
    return await _dataAccess.CreateAsync<int>("SP_AddOrderItem", System.Data.CommandType.StoredProcedure, ct, createOrderItemParam);
  }
}
