using OnlineStore.Core.DTOs.Items;
using OnlineStore.Core.Entities;

namespace OnlineStore.Core.Mappers.ItemMappers;
public class CartItemMapper : Mapper<OrderItem, CartItemDto>
{
  public static CartItemDto toDto(OrderItem Entity)
  {
    CartItemDto cartItemDto = new CartItemDto()
    {
      ShoppingCartId = Entity.ShoppingCartId!.Value,
      ProductId = Entity.ProductId,
      Quantity = Entity.Quantity,
    };

    foreach (var item in Entity.CustomizationChoices)
    {
      cartItemDto.ChoicesID.Add(item.Id);
    }

    return cartItemDto;
  }

  public static OrderItem toEntity(CartItemDto DTO)
  {
    OrderItem orderItem = new OrderItem()
    {
      ShoppingCartId = DTO.ShoppingCartId,
      ProductId = DTO.ProductId,
      Quantity = DTO.Quantity,
    };

    foreach (int item in DTO.ChoicesID)
    {
      orderItem.CustomizationChoices.Add(new CustomizationChoice()
      {
        Id = item,
        
      });
    }

    return orderItem;
  }
}
