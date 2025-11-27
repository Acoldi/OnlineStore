using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Core.Entities;
using OnlineStore.Core.InterfacesAndServices.IRepositories;

namespace OnlineStore.Core.InterfacesAndServices.UserServices;
public interface IUserService
{
  public Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

  public Task<Guid> CreateAsync(User NewUser, CancellationToken cancellationToken = default);
}
