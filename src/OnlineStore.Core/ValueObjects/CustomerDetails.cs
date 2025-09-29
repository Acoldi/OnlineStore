using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.ValueObjects;
public class CustomerDetails
{
  public required string name { get; set; }
  public required string email { get; set; }
  public required string phone { get; set; }
  public required string street1 { get; set; }
  public required string city { get; set; }
  public required string state { get; set; } // 2 Letters
  public required string country { get; set; } // 2 Letters
  public required string zip { get; set; }

}
