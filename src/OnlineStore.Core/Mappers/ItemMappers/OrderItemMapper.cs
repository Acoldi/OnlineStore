using OnlineStore.Core.DTOs.Items;
using OnlineStore.Core.Entities;

namespace OnlineStore.Core.Mappers.ItemMappers;
public class OrderItemMapper : Mapper<OrderItem, OrderItemDto>
{
  public static OrderItemDto toDto(OrderItem Entity)
  {
    var orderItemDto = new OrderItemDto()
    {
      OrderItemID = Entity.OrderId!.Value,
      ProductId = Entity.ProductId,
      Quantity = Entity.Quantity,
    };
    
    foreach (var item in Entity.CustomizationChoices)
    {
      orderItemDto.ChoicesID.Add(item.Id);
    }

    return orderItemDto;
  }

  public static OrderItem toEntity(OrderItemDto DTO)
  {
    var orderItem = new OrderItem()
    {
      OrderId = DTO.OrderItemID,
      ProductId = DTO.ProductId,
      Quantity = DTO.Quantity,
    };

    foreach (var item in DTO.ChoicesID)
    {
      orderItem.CustomizationChoices.Add(new CustomizationChoice()
      {
        Id = item
      });
    }

    return orderItem;
  }
}
