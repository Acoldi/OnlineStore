using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Core.DTOs;
using OnlineStore.Core.Entities;

namespace OnlineStore.Core.Mappers;
public interface Mapper<TEntity, TDto> where TEntity : class where TDto : class
{
  public abstract static TEntity toEntity(TDto dto);
  public abstract static TDto toDto(TEntity entity);

}
