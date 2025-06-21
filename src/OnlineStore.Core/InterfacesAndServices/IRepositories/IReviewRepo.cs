using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Core.Interfaces.DataAccess;

namespace OnlineStore.Core.InterfacesAndServices.IRepositories;
public interface IReviewRepo : IDataAccess<Entities.Review, int>
{

}
