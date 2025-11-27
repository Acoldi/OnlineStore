using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Core.DTOs;
using OnlineStore.Core.Entities;
using OnlineStore.Core.InterfacesAndServices.IRepositories;
using OnlineStore.Core.Mappers;

namespace OnlineStore.Core.InterfacesAndServices.CustomizationServices;
public class CustomizationsService : ICustomizationsService
{
  private readonly ICustomizationOptionRepo _customizationOptionRepo;
  private readonly ICustomizationChoiceRepo _customizationChoiceRepo;

  public CustomizationsService(ICustomizationOptionRepo customizationOptionRepo, ICustomizationChoiceRepo customizationChoiceRepo)
  {
    _customizationOptionRepo = customizationOptionRepo;
    _customizationChoiceRepo = customizationChoiceRepo;
  }

  public async Task<int> CreateCustomizationChoiceAsync(CustomizationChoiceDto customizationChoice , CancellationToken ct = default)
  {
    return await _customizationChoiceRepo.CreateAsync(CustomizationChoiceMapper.toEntity(customizationChoice), ct);
  }

  public async Task<List<CustomizationOptionDto>?> ListCustomizationOptionsForProduct(int ProductID)
  {
    List<CustomizationOption>? customizationOptions = await _customizationOptionRepo.GetProductCustomizationOptions(ProductID);

    if (customizationOptions == null) return null;

    List<CustomizationOptionDto> customizationOptionDtos = new List<CustomizationOptionDto>(); 

    // LINQ's Select instead of foreach
    foreach (var item in customizationOptions)
    {
      customizationOptionDtos.Add(CustomizationOptionMapper.toDto(item));
    }

    return customizationOptionDtos;
  }
}
