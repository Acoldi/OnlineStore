using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using OnlineStore.Core.Entities;

namespace OnlineStore.Core.DTOs.Items;
public class CartItemDto
{
  [JsonPropertyName("product_id")]
  public int ProductId { get; set; }

  [JsonPropertyName("quantity")]
  public int Quantity { get; set; }

  [JsonPropertyName("shopping_cart_id")]
  public required int ShoppingCartId { get; set; }
  [JsonPropertyName("choices_id")]

  public List<int> ChoicesID = new List<int>();
}
