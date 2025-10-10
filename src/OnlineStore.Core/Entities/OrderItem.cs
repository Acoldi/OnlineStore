using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Core.InterfacesAndServices.IRepositories;

namespace OnlineStore.Core.Entities;
public class OrderItem
{
  public int Id { get; set; }
  private int? _ProductID { get; set; }
  public int? ProductID
  {
    // When using dapper, I should retrieve both objects from the repository
    // When Using EF, it maps the product property automatically
    set
    {
      if (value == null && ShoppingCartID == null)
      {
        throw new ArgumentException("Either ProductID or ShoppingCartID should be null");
      }
      _ProductID = value;
    }
    get
    {
      return _ProductID;
    }
  }
  public Product? product { get; set; }
  public int? OrderID { get; set; }
  public int Quantity { get; set; }
  public int? ShoppingCartID { get; set; }
  public decimal? Price { get; set; }

  /// <summary>
  /// Creates a new instance of OrderItem linked to an order
  /// </summary>
  /// <param name="id"></param>
  /// <param name="productID"></param>
  /// <param name="orderID"></param>
  /// <param name="quantity"></param>
  /// <param name="shoppingCarID"></param>
  /// <param name="price"></param>
  public OrderItem(int id, int productID, int orderID, int quantity, int? shoppingCarID, decimal price)
  {
    Id = id;
    ProductID = productID;
    OrderID = orderID;
    Quantity = quantity;
    ShoppingCartID = shoppingCarID;
    Price = price;
  }

  /// <summary>
  /// Creates a new instance of OrderItem linked to a shopping cart
  /// </summary>
  /// <param name="id"></param>
  /// <param name="productID"></param>
  /// <param name="orderID"></param>
  /// <param name="quantity"></param>
  /// <param name="shoppingCarID"></param>
  /// <param name="price"></param>
  public OrderItem(int id, int productID, int? orderID, int quantity, int shoppingCarID)
  {
    Id = id;
    ProductID = productID;
    OrderID = orderID;
    Quantity = quantity;
    ShoppingCartID = shoppingCarID;
  }

  public OrderItem()
  {

  }
}
