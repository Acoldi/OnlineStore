using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Core.DTOs;
using OnlineStore.Core.Entities;
using OnlineStore.Core.InterfacesAndServices.IRepositories;
using OnlineStore.Core.Mappers;
using OnlineStore.Web.DTOs;

namespace OnlineStore.Core.InterfacesAndServices.Products;
public class ProductService : IProductService
{
  private IProductRepo _IProductRepo;
  public ProductService(IProductRepo iProductRepo)
  {
    _IProductRepo = iProductRepo;
  }

  public async Task<int> CreateNewProduct(ProductDto productDto, CancellationToken? cancellationToken)
  {
    Product product = ProductMapper.toEntity(productDto);

    product.Id = await _IProductRepo.CreateAsync(product, cancellationToken);
    
    return product.Id;
  }

  public async Task<int> CreateNewProductAsync(ProductDto product, CancellationToken cancellationToken = default)
  {
    return await _IProductRepo.CreateAsync(ProductMapper.toEntity(product), cancellationToken);
  }

  public async Task<bool> DeleteAsync(int ProductID)
  {
    return await _IProductRepo.DeleteAsync(ProductID);
  }

  public async Task<List<ProductDto>?> GetAsync()
  {
    List<Product>? products = await _IProductRepo.GetAsync();

    if (products == null) return null;

    List<ProductDto>? productDtos = products.Select(p => ProductMapper.toDto(p)).ToList();

    return productDtos;
  }

  public async Task<ProductDto?> GetByNameAsync(string Name)
  {
    Product? product = await _IProductRepo.GetByNameAsync(Name);
    if (product == null) return null;
    return ProductMapper.toDto(product);
  }

  public async Task<List<ProductDto>?> GetCustomizableProducts(CancellationToken cancellationToken = default)
  {
    List<Product>? productDtos = await _IProductRepo.GetCustomizableProducts(cancellationToken);

    if (productDtos == null) return null;

    return [.. productDtos.Select(p => ProductMapper.toDto(p))];
  }
}
