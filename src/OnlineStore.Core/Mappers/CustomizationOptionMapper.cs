using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Core.DTOs;
using OnlineStore.Core.Entities;

namespace OnlineStore.Core.Mappers;
public class CustomizationOptionMapper : Mapper<CustomizationOption, CustomizationOptionDto>
{
  public static CustomizationOptionDto toDto(CustomizationOption Entity)
  {
    return new CustomizationOptionDto()
    {
      AdditionalCost = Entity.AdditionalCost,
      Label = Entity.Label,
      ProductId = Entity.ProductId,
      TypeId = Entity.TypeId,
    };
  }

  public static CustomizationOption toEntity(CustomizationOptionDto DTO)
  {
    return new CustomizationOption()
    {
      TypeId = DTO.TypeId,
      AdditionalCost = DTO.AdditionalCost,
      Label = DTO.Label,
      ProductId = DTO.ProductId,
    };
  }
}
