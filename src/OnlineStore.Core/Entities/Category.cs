using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Entities;
public class Category
{
  public int ID { get; set; }
  public required string Name { get; set; }
	public int? ParentCategoryID { get; set; }
  public required string Slug { get; set; }

  public Category()
  {  }

  public Category(string name, int? parentCategoryID, string slug)
  {
    Name = name;
    ParentCategoryID = parentCategoryID;
    Slug = slug;
  }
}
