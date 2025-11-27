using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Core.DTOs;
using OnlineStore.Core.Entities;

namespace OnlineStore.Core.Mappers;
public class CustomizationChoiceMapper : Mapper<CustomizationChoice, CustomizationChoiceDto>
{
  public static CustomizationChoiceDto toDto(CustomizationChoice Entity)
  {
    return new CustomizationChoiceDto()
    {
      OptionId = Entity.OptionId,
      Value = Entity.Value,
    };
  }

  public static CustomizationChoice toEntity(CustomizationChoiceDto DTO)
  {
    return new CustomizationChoice()
    {
      Value = DTO.Value,
      OptionId = DTO.OptionId,
    };
  }
}
