using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Core.Interfaces;

namespace OnlineStore.Core.InterfacesAndServices;
static public class UtilityService
{
  static public string GenerateSlug(string Token)
  {
    return Token.Replace("_", "-").Replace(" ", "-").ToLower().Trim();
  }
}
