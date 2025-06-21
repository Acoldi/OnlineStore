using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Entities;
public class ShoppingCart
{
  public Guid UserID { get; set; }
  public DateTime CreatedAt { get; set; } = DateTime.Now;

  public ShoppingCart(Guid UserID)
  {
    this.UserID = UserID;
  }

  public ShoppingCart() { }
}
