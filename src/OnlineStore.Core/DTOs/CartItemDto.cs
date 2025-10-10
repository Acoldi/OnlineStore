using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Core.Entities;

namespace OnlineStore.Web.DTOs;
public class CartItemDto
{
  public required OrderItem orderItem;
  public required List<int> ChoicesID;
}
