using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Entities;
public class Customer
{
  public int ID { set; get; }
  public DateTime TurnedInAT { get; set; }
  public int UserID { get; set; }

  public Customer(Guid userID)
  {
    this.UserID = UserID;
    TurnedInAT = DateTime.Now;
  }
}
