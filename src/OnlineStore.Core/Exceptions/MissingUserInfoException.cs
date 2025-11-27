using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace OnlineStore.Core.Exceptions;
public class MissingUserInfoException : Exception
{
  public MissingUserInfoException() {  }

  public MissingUserInfoException(string message) : base(message) { }

  public MissingUserInfoException(string? message, Exception? innerException) : base(message, innerException) 
  {  
  }
}
