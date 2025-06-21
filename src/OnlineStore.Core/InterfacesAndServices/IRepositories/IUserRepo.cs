using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Core.Entities;
using OnlineStore.Core.Interfaces.DataAccess;

namespace OnlineStore.Core.InterfacesAndServices.IRepositories;
public interface IUserRepo : IDataAccess<User, Guid>
{
  public Task<User?> GetByEmail(string email, CancellationToken? ct);
}
