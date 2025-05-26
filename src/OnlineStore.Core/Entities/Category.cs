using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Entities;
public class Category
{
  public int ID { get; set; }
  public string Name { get; set; }
	public int? ParentCategoryID { get; set; }
  public string? Slug { get; set; }

  public Category()
  {
    Name = string.Empty;
    ParentCategoryID = null;
    Slug = null;
  }

  public Category(string name, int? parentCategoryID, string? slug)
  {
    Name = name;
    ParentCategoryID = parentCategoryID;
    Slug = slug;
  }
}
