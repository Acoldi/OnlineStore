using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Core.Interfaces.DataAccess;

namespace OnlineStore.Core.Interfaces.User;
public interface IUserService
{
  public Task<int> Create(AddNewUserParameters AddUserParameters, CancellationToken ct);

}
