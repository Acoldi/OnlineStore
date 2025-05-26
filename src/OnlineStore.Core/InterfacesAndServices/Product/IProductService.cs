using OnlineStore.Core.Entities;
using OnlineStore.Core.Interfaces.DTOs.Parameters;

namespace OnlineStore.Core.Interfaces.Products;
public interface IProductService
{
  public Task<List<Product>?> GetAll(CancellationToken? cancellationToken = null);
  public Task<Product?> GetById(int id, CancellationToken? cancellationToken = null);
  public Task<List<Product>?> GetByCategory(string category, CancellationToken? cancellationToken = null);
  public Task<List<Product>?> GetByCategoryID(string category, CancellationToken? cancellationToken = null);
  public Task<int> Create(AddProductParameters newProduct, CancellationToken? cancellationToken = null);
  public Task Update(UpdateProductParameters UpdatedProduct, CancellationToken? cancellationToken = null);
  public Task<bool> Delete(object? ID, CancellationToken? cancellationToken = null);
  public Task<List<Product>?> GetCustomizableProducts(CancellationToken? cancellationToken);
}
