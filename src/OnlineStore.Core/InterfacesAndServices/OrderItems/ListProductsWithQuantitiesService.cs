using OnlineStore.Core.DTOs;
using OnlineStore.Core.Entities;
using OnlineStore.Core.InterfacesAndServices.IRepositories;

namespace OnlineStore.Core.InterfacesAndServices.OrderItems;
public class ListProductsWithQuantitiesService : IListProductsWithQuantitiesService
{
  private readonly IOrderItemRepo _orderItemRepo;
  public ListProductsWithQuantitiesService(IOrderItemRepo orderItemRepo)
  {
    _orderItemRepo = orderItemRepo;
  }

  public List<ProductNameQuantity> listProductsWithQuantitiesAsync(int OrderID)
  {
    List<OrderItem> Orders = _orderItemRepo.GetAsync(OrderID);

    List<ProductNameQuantity> result = new List<ProductNameQuantity>();
    foreach (var o in Orders)
    {
      result.Add(new ProductNameQuantity()
      {
        Name = o.Product.Name,
        Quantity = o.Quantity,
      });
    }

    return result;
  }
}
