using OnlineStore.Core.Interfaces.DataAccess;
using OnlineStore.Core.Interfaces.User;

namespace OnlineStore.Core.InterfacesAndServices.User;
public class UserSservice : IUserService
{
  IDataAccess _dataAccess;
  public UserSservice(IDataAccess dataAccess)
  {
    _dataAccess = dataAccess;
  }

  public async Task<int> Create(AddNewUserParameters AddUserParameters, CancellationToken ct)
  {
    return await _dataAccess.CreateAsync("SP_AddNewUser", System.Data.CommandType.StoredProcedure, ct, AddUserParameters);
  }
}
