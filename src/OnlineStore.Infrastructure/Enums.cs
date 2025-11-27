using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Infrastructure;

public enum enTransactionTypesPaytab
{
  Sale,
  Auth,
  Capture,
  Void,
  Register,
  Refund,
}

public enum enTransactionClass
{
  ecom,
  recurring,
  moto
}
