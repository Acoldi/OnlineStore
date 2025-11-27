using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.DTOs;
public class CategoryDto
{
  public required string Name { get; set; }
  public int? ParentCategoryId { get; set; }
  public required string Slug { get; set; }

}
