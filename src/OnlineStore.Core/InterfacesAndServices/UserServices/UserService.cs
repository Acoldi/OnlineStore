using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Core.Entities;
using OnlineStore.Core.InterfacesAndServices.IRepositories;

namespace OnlineStore.Core.InterfacesAndServices.UserServices;
public class UserService : IUserService
{
  private readonly IUserRepo _userRepo;
  public UserService(IUserRepo userRepo)
  {
    _userRepo = userRepo;
  }

  public async Task<Guid> CreateAsync(User NewUser, CancellationToken cancellationToken = default)
  {
    return await _userRepo.CreateAsync(NewUser, cancellationToken);
  }

  public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
  {
    return await _userRepo.GetByEmail(email, cancellationToken);
  }
}
