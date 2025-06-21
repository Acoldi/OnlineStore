using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace OnlineStore.Core.Entities;
public class Review
{
  public int ID { get; set; }
  public required int ProductID { get; set; }
  public required Guid UserID { get; set; }
  public int? Rating { get; set; }
  public string? Comment { get; set; }
  public required DateTime CreatedAt { get; set; }
  public required bool IsApproved { get; set; }

  [SetsRequiredMembers]
  public Review(int productID, Guid userID, DateTime createdAt, bool isApproved)
  {
    ProductID = productID;
    UserID = userID;
    CreatedAt = createdAt;
    IsApproved = isApproved;
  }

  public Review() { }

}
