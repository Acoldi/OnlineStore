using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Core.Entities;
using OnlineStore.Core.InterfacesAndServices.IRepositories;
using OnlineStore.Web.DTOs;

namespace OnlineStore.Core.InterfacesAndServices.Products;
public class ProductService : IProductService
{
  private IProductRepo _IProductRepo;
  public ProductService(IProductRepo iProductRepo)
  {
    _IProductRepo = iProductRepo;
  }

  public async Task<int> CreateNewProduct(AddProductParameters addProductParameters, CancellationToken? cancellationToken)
  {
    Product product = new Product()
    {
      Name = addProductParameters.ProductName,
      Sku = addProductParameters.ProductName,
      CategoryId = addProductParameters.CategoryID,
      Price = addProductParameters.Price,
      Slug = UtilityService.GenerateSlug(addProductParameters.ProductName),
      IsActive = true,
    };

    product.Id = await _IProductRepo.CreateAsync(product, cancellationToken);
    
    return product.Id;
  }
}
