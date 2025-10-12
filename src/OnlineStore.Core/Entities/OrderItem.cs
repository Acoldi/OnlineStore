namespace OnlineStore.Core.Entities;
public class OrderItem
{
  public int Id { get; set; }
  public int ProductID;

  public Product? product { get; set; }
  private int? _OrderID { get; set; }

  public int? OrderID
  {
    // When using dapper, I should retrieve both objects from the repository
    // When Using EF, it maps the product property automatically
    set
    {
      if (value == null && ShoppingCartID == null)
      {
        throw new ArgumentException("Either ProductID or ShoppingCartID should be null");
      }
      _OrderID = value;
    }
    get
    {
      return _OrderID;
    }
  }
  public int Quantity { get; set; }

  private int? _ShoppingCartID { get; set; }
  public int? ShoppingCartID
  {
    // When using dapper, I should retrieve both objects from the repository
    // When Using EF, it maps the product property automatically
    set
    {
      if (value == null && OrderID == null)
      {
        throw new ArgumentException("Either ProductID or ShoppingCartID should be null");
      }
      _ShoppingCartID = value;
    }
    get
    {
      return _ShoppingCartID;
    }
  }
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
