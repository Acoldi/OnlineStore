using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.DTOs;
public class ReviewDto
{
  public int ProductId { get; set; }

  public Guid UserId { get; set; }

  public int? Rating { get; set; }

  public string? Comment { get; set; }

  public DateTime CreatedAt { get; set; }

}
