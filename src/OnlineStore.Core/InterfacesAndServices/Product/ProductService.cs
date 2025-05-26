using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Core.Entities;
using OnlineStore.Core.Interfaces.DataAccess;
using OnlineStore.Core.Interfaces.Products;
using System.Data;
using OnlineStore.Core.Interfaces.DTOs.Parameters;

namespace OnlineStore.Core.InterfacesAndServices.Product;
public class ProductService : IProductService
{
  IDataAccess _dataAccess;
  public ProductService(IDataAccess dataAccess)
  {
    _dataAccess = dataAccess;
  }

  public async Task<int> Create(AddProductParameters NewProduct, CancellationToken? cancellationToken = null)
  {
    var product = new Product(null, NewProduct.ProductName, NewProduct.Description,
     NewProduct.Price, NewProduct.CategoryID, NewProduct.Sku, DateTime.Now, true);

    return await _dataAccess.CreateAsync("SP_AddProduct", CommandType.StoredProcedure, cancellationToken, product);
  }

  public async Task<bool> Delete(object? ID, CancellationToken? cancellationToken = null)
  {
    return await _dataAccess.DeleteAsync("SP_DeleteProduct", CommandType.StoredProcedure, cancellationToken, ID);
  }

  public async Task<List<Product>?> GetAll(CancellationToken? cancellationToken = null)
  {
    return await _dataAccess.LoadDataAsync<Product>("SP_GetAllProducts", CommandType.StoredProcedure, cancellationToken);
  }

  public async Task<List<Product>?> GetByCategory(string category, CancellationToken? cancellationToken = null)
  {
    return await _dataAccess.LoadDataAsync<Product>("SP_GetProductByCategory", CommandType.StoredProcedure, cancellationToken, category);
  }

  public async Task<List<Product>?> GetByCategoryID(string category, CancellationToken? cancellationToken = null)
  {
    return await _dataAccess.LoadDataAsync<Product>("SP_GetProductByCategoryID", CommandType.StoredProcedure, cancellationToken, category);
  }

  public async Task<Product?> GetById(int id, CancellationToken? cancellationToken = null)
  {
    return await _dataAccess.LoadSingleAsync<Product>("SP_GetProductByID", CommandType.StoredProcedure, cancellationToken, id);
  }
  public async Task Update(UpdateProductParameters UpdatedProductParams, CancellationToken? cancellationToken = null)
  {
    await _dataAccess.UpdateDataAsync("SP_UpdateProduct", CommandType.StoredProcedure, cancellationToken, UpdatedProductParams);
  }

  public async Task<List<Product>?> GetCustomizableProducts(CancellationToken? cancellationToken)
  {
    return await _dataAccess.LoadDataAsync<Product>("SP_GetCustomizableProducts", CommandType.StoredProcedure, cancellationToken);
  }

}
