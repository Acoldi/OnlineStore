using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Core.DTOs;
using OnlineStore.Core.Entities;
using OnlineStore.Web.DTOs;

namespace OnlineStore.Core.InterfacesAndServices.Products;
public interface IProductService
{
  public Task<int> CreateNewProduct(ProductDto addProductParameters, CancellationToken? cancellationToken);

  public Task<List<ProductDto>?> GetAsync();
  public Task<ProductDto?> GetByNameAsync(string Name);
  public Task<int> CreateNewProductAsync(ProductDto product, CancellationToken cancellationToken = default);
  public Task<bool> DeleteAsync(int ID);
  public Task<List<ProductDto>?> GetCustomizableProducts(CancellationToken cancellationToken = default);

}
