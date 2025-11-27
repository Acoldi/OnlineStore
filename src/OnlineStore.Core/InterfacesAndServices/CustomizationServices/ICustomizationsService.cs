using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Core.DTOs;
using OnlineStore.Core.Entities;

namespace OnlineStore.Core.InterfacesAndServices.CustomizationServices;
public interface ICustomizationsService
{
  public Task<List<CustomizationOptionDto>?> ListCustomizationOptionsForProduct(int ProductID);

  public Task<int> CreateCustomizationChoiceAsync (CustomizationChoiceDto customizationChoice ,CancellationToken ct = default);

}
