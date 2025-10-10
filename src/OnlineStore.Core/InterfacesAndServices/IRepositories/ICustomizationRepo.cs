using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Core.Entities;
using OnlineStore.Core.Interfaces.DataAccess;

namespace OnlineStore.Core.InterfacesAndServices.IRepositories;
public interface ICustomizationRepo : IDataAccess<Customization, int>
{
  /// <summary>
  /// Deletes all customizations related to the given itemID
  /// </summary>
  /// <param name="ItemID"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  public Task<bool> DeleteByItemIDAsync(int ItemID, CancellationToken? cancellationToken = null)

}
