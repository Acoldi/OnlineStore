using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Core.DTOs;
using OnlineStore.Core.Entities;
using OnlineStore.Core.Interfaces.DataAccess;

namespace OnlineStore.Core.InterfacesAndServices.IRepositories;
public interface IReviewRepo : IDataAccess<Review, int>
{
  public Task<List<Review>> GetAllAcceptedReviews();
  public Task<List<Review>> GetAllAcceptedReviews(int productID);
}
