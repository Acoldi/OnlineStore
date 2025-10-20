using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Web.DTOs;

namespace OnlineStore.Core.InterfacesAndServices.Products;
public interface IProductService
{
  public Task<int> CreateNewProduct(AddProductParameters addProductParameters, CancellationToken? cancellationToken);
}
