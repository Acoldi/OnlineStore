using OnlineStore.Core.Entities;
using OnlineStore.Core.Interfaces.DataAccess;

namespace OnlineStore.Core.InterfacesAndServices.IRepositories;
public interface IProductRepo : IDataAccess<Product, int  >
{
  public Task<Product?> GetByNameAsync(string Name, CancellationToken? ct= null);
  public Task<int> GetIdBySKU(string ProductSKU, CancellationToken? ct = null);
  public Task<List<Product>?> GetByCategory(string category, CancellationToken? ct= null);
  public Task<List<Product>?> GetByCategoryID(int ID, CancellationToken? ct= null);
  public Task<List<Product>?> GetCustomizableProducts(CancellationToken? ct);
  public Task<string> GetProductSKU(string ProductName, CancellationToken? ct);
}
