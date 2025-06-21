namespace OnlineStore.Core.Entities;
public class Inventory
{
  public int ID { get; set; }
  public int ProductID { get; set; }
  public int Quantity { get; set; }
  public DateTime LastRestockedAt { get; set; }

  public Inventory(int id, int productId, int quantity, DateTime lastRestockedAt)
  {
    ID = id;
    ProductID = productId;
    Quantity = quantity;
    LastRestockedAt = lastRestockedAt;
  }

  public Inventory() { }
}
