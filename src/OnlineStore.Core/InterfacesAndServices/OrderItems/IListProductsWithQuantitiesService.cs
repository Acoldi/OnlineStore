using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Core.DTOs;

namespace OnlineStore.Core.InterfacesAndServices.OrderItems;
public interface IListProductsWithQuantitiesService
{
  public List<ProductNameQuantity> listProductsWithQuantitiesAsync(int OrderID);
}
