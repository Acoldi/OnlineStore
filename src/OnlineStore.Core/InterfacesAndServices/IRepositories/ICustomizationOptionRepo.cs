using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Core.Entities;
using OnlineStore.Core.Interfaces.DataAccess;

namespace OnlineStore.Core.InterfacesAndServices.IRepositories;
public interface ICustomizationOptionRepo : IDataAccess<CustomizationOption, int>
{
  public Task<List<CustomizationOption>?> GetProductCustomizationOptions(int productID);

}
