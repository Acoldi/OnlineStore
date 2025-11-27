using OnlineStore.Core.Entities;

namespace OnlineStore.Core.InterfacesAndServices.IRepositories;
public interface ICustomerRepo
{
  #region Ai Generated
  Task<int> CreateAsync(Customer param, CancellationToken? cancellationToken = null);
  Task<bool> DeleteAsync(int id, CancellationToken? cancellationToken = null);
  Task<List<Customer>?> GetAsync(CancellationToken? cancellationToken = null);
  #endregion Ai

  bool UpdateAsync(Customer param, CancellationToken? cancellationToken = null);
  Customer? GetByIDAsync(int id, CancellationToken? cancellationToken = null);
}
