using Microsoft.EntityFrameworkCore;
using OnlineStore.Core.Entities;

namespace OnlineStore.Infrastructure.Data.Models;

public partial class EStoreSystemContext : DbContext
{
  public EStoreSystemContext()
  {
  }
  public EStoreSystemContext(DbContextOptions<EStoreSystemContext> options)
      : base(options)
  {
  }

  public virtual DbSet<Address> Addresses { get; set; }

  public virtual DbSet<CartItem> CartItems { get; set; }

  public virtual DbSet<Category> Categories { get; set; }

  public virtual DbSet<City> Cities { get; set; }

  public virtual DbSet<Country> Countries { get; set; }

  public virtual DbSet<Customer> Customers { get; set; }

  public virtual DbSet<CustomizationChoice> CustomizationChoices { get; set; }

  public virtual DbSet<CustomizationOption> CustomizationOptions { get; set; }

  public virtual DbSet<CustomizationOptionType> CustomizationOptionTypes { get; set; }

  public virtual DbSet<Image> Images { get; set; }

  public virtual DbSet<Inventory> Inventories { get; set; }

  public virtual DbSet<Order> Orders { get; set; }

  public virtual DbSet<OrderItem> OrderItems { get; set; }

  public virtual DbSet<Payment> Payments { get; set; }

  public virtual DbSet<Product> Products { get; set; }

  public virtual DbSet<Review> Reviews { get; set; }

  public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }

  public virtual DbSet<State> States { get; set; }

  public virtual DbSet<User> Users { get; set; }

  public virtual DbSet<Video> Videos { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Address>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK__Addresse__3214EC27FB110A03");

      entity.Property(e => e.Id).HasColumnName("ID");
      entity.Property(e => e.Street).HasMaxLength(50).HasColumnName("Street");
      entity.Property(e => e.Zip).HasColumnName("Zip");
      entity.Property(e => e.CityId).HasColumnName("CityID");
      entity.Property(e => e.CountryId).HasColumnName("CountryID");
      entity.Property(e => e.StateId).HasColumnName("StateID");
      entity.Property(e => e.UserId).HasColumnName("UserID");

      entity.HasOne(d => d.City).WithMany(p => p.Addresses)
              .HasForeignKey(d => d.CityId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_Cities");

      entity.HasOne(d => d.Country).WithMany(p => p.Addresses)
              .HasForeignKey(d => d.CountryId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_Addresses_Countries");

      entity.HasOne(d => d.State).WithMany(p => p.Addresses)
              .HasForeignKey(d => d.StateId)
              .HasConstraintName("FK_Addresses_States");

      entity.HasOne(d => d.User).WithMany(p => p.Addresses)
              .HasForeignKey(d => d.UserId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_Addresses_Users");
    });

    modelBuilder.Entity<CartItem>(entity =>
    {
      entity
              .HasNoKey()
              .ToView("CartItems");

      entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
      entity.Property(e => e.ProductId).HasColumnName("ProductID");
      entity.Property(e => e.UserId).HasColumnName("UserID");
    });

    modelBuilder.Entity<Category>(entity =>
    {
      entity.HasIndex(e => e.Slug, "UQ__Categori__BC7B5FB60CCF3B96").IsUnique();

      entity.Property(e => e.Id).HasColumnName("ID");
      entity.Property(e => e.Name).HasMaxLength(50);
      entity.Property(e => e.ParentCategoryId).HasColumnName("ParentCategoryID");
      entity.Property(e => e.Slug)
              .HasMaxLength(100)
              .IsUnicode(false);

      entity.HasOne(d => d.ParentCategory).WithMany(p => p.InverseParentCategory)
              .HasForeignKey(d => d.ParentCategoryId)
              .HasConstraintName("FK_Categories_Categories");
    });

    modelBuilder.Entity<City>(entity =>
    {
      entity.Property(e => e.Id).HasColumnName("ID");
      entity.Property(e => e.CountryId).HasColumnName("CountryID");
      entity.Property(e => e.Name).HasMaxLength(100);
      entity.Property(e => e.StateId).HasColumnName("StateID");

      entity.HasOne(d => d.Country).WithMany(p => p.Cities)
              .HasForeignKey(d => d.CountryId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_Cities_Countries");

      entity.HasOne(d => d.State).WithMany(p => p.Cities)
              .HasForeignKey(d => d.StateId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_Cities_States");
    });

    modelBuilder.Entity<Country>(entity =>
    {
      entity.Property(e => e.Id).HasColumnName("ID");
      entity.Property(e => e.DialCode).HasMaxLength(50);
      entity.Property(e => e.Isoalpha2)
              .HasMaxLength(2)
              .HasColumnName("ISOALpha2");
      entity.Property(e => e.IsocurrencyCode)
              .HasMaxLength(10)
              .HasColumnName("ISOCurrencyCode");
      entity.Property(e => e.Name).HasMaxLength(100);
      entity.Property(e => e.NameAr).HasMaxLength(50);
    });

    modelBuilder.Entity<Customer>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC27EC5F41AD");

      entity.Property(e => e.Id).HasColumnName("ID");
      entity.Property(e => e.IsActive).HasDefaultValue(true);
      entity.Property(e => e.TurnedInAt)
              .HasColumnType("datetime")
              .HasColumnName("TurnedInAT");
      entity.Property(e => e.UserId).HasColumnName("UserID");

      entity.HasOne(d => d.User).WithMany(p => p.Customers)
              .HasForeignKey(d => d.UserId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK__Customers__UserI__671F4F74");
    });

    modelBuilder.Entity<CustomizationChoice>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK__Customiz__3214EC27B49B61E0");

      entity.ToTable("CustomizationChoice", tb => tb.HasTrigger("After_insert_UpdateItemCost"));

      entity.Property(e => e.Id).HasColumnName("ID");
      entity.Property(e => e.OptionId).HasColumnName("OptionID");
      entity.Property(e => e.Value).HasMaxLength(100);

      entity.HasOne(d => d.Option).WithMany(p => p.CustomizationChoices)
              .HasForeignKey(d => d.OptionId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK__Customiza__Optio__2CF2ADDF");
    });

    modelBuilder.Entity<CustomizationOption>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK__Customiz__3214EC27F8294450");

      entity.HasIndex(e => e.Label, "UQ_CustomizationOptionLabel").IsUnique();

      entity.Property(e => e.Id).HasColumnName("ID");
      entity.Property(e => e.AdditionalCost).HasColumnType("decimal(10, 2)");
      entity.Property(e => e.Label).HasMaxLength(100);
      entity.Property(e => e.ProductId).HasColumnName("ProductID");
      entity.Property(e => e.TypeId).HasColumnName("TypeID");

      entity.HasOne(d => d.Type).WithMany(p => p.CustomizationOptions)
              .HasForeignKey(d => d.TypeId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_CustomizationOptions_CustomizationOptionTypes");

      entity.HasMany(d => d.Products).WithMany(p => p.CustomizationOptions)
              .UsingEntity<Dictionary<string, object>>(
                  "ProductCustomizationAvailability",
                  r => r.HasOne<Product>().WithMany()
                      .HasForeignKey("ProductId")
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_ProductCustomizationAvailability_products"),
                  l => l.HasOne<CustomizationOption>().WithMany()
                      .HasForeignKey("CustomizationOptionId")
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_ProductCustomizationAvailability_CustomizationOptions"),
                  j =>
                  {
                  j.HasKey("CustomizationOptionId", "ProductId");
                  j.ToTable("ProductCustomizationAvailability", tb => tb.HasComment("Associatvie table"));
                  j.IndexerProperty<int>("CustomizationOptionId").HasColumnName("CustomizationOptionID");
                  j.IndexerProperty<int>("ProductId").HasColumnName("ProductID");
                });
    });

    modelBuilder.Entity<CustomizationOptionType>(entity =>
    {
      entity.HasIndex(e => e.TypeName, "UQ_CustomizationOptionTypes").IsUnique();

      entity.Property(e => e.Id).HasColumnName("ID");
      entity.Property(e => e.TypeName).HasMaxLength(50);
    });

    modelBuilder.Entity<Image>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK__Images__3214EC2733C7FD79");

      entity.Property(e => e.Id).HasColumnName("ID");
      entity.Property(e => e.CreatedAt)
              .HasDefaultValueSql("(getdate())")
              .HasColumnType("datetime");
      entity.Property(e => e.ProductId).HasColumnName("ProductID");

      entity.HasOne(d => d.Product).WithMany(p => p.Images)
              .HasForeignKey(d => d.ProductId)
              .HasConstraintName("FK__Images__ProductI__756D6ECB");
    });

    modelBuilder.Entity<Inventory>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK__Inventor__3214EC2749C39A13");

      entity.ToTable("Inventory");

      entity.Property(e => e.Id).HasColumnName("ID");
      entity.Property(e => e.LastRestockedAt)
              .HasDefaultValueSql("(getdate())")
              .HasColumnType("datetime");
      entity.Property(e => e.ProductId).HasColumnName("ProductID");

      entity.HasOne(d => d.Product).WithMany(p => p.Inventories)
              .HasForeignKey(d => d.ProductId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_Inventory_products");
    });

    modelBuilder.Entity<Order>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK__Orders__3214EC2712E12D2F");

      entity.ToTable(tb =>
              {
              tb.HasComment("Pending=1,Shipping,Delivered,Cancelled");
              tb.HasTrigger("AfterTrigger_LinkCartItemsToNewlyAddedOrder");
              tb.HasTrigger("AfterUpdateOrder_AddRemoveClient");
            });

      entity.Property(e => e.Id).HasColumnName("ID");
      entity.Property(e => e.CreatedAt)
              .HasDefaultValueSql("(getdate())")
              .HasColumnType("datetime");
      entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
      entity.Property(e => e.OrderStatus).HasComment("  Pending = 1,\r\n  Shipping,\r\n  Delivered,\r\n  Cancelled");
      entity.Property(e => e.ShippingAddressId).HasColumnName("ShippingAddressID");
      entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 0)");

      entity.HasOne(d => d.ShippingAddress).WithMany(p => p.Orders)
              .HasForeignKey(d => d.ShippingAddressId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_Orders_Addresses");

      entity.HasOne(d => d.ShippingAddressNavigation).WithMany(p => p.Orders)
              .HasForeignKey(d => d.ShippingAddressId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_Orders_Customers");
    });

    modelBuilder.Entity<OrderItem>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK__OrderIte__3214EC279CA63698");

      entity.ToTable(tb =>
              {
              tb.HasTrigger("After_Insert_UpdateOrderTotalAmount");
              tb.HasTrigger("DeleteCustomizationsWithItem");
            });

      entity.Property(e => e.Id).HasColumnName("ID");
      entity.Property(e => e.OrderId).HasColumnName("OrderID");
      entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
      entity.Property(e => e.ProductId).HasColumnName("ProductID");
      entity.Property(e => e.ShoppingCartId).HasColumnName("ShoppingCartID");

      entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
              .HasForeignKey(d => d.OrderId)
              .HasConstraintName("FK_OrderItems_Orders1");

      entity.HasOne(d => d.Product).WithMany(p => p.OrderItems)
              .HasForeignKey(d => d.ProductId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_OrderItems_products");

      entity.HasOne(d => d.ShoppingCart).WithMany(p => p.OrderItems)
              .HasForeignKey(d => d.ShoppingCartId)
              .HasConstraintName("FK_OrderItems_ShoppingCart");

      entity.HasMany(d => d.CustomizationChoices).WithMany(p => p.OrderItems)
              .UsingEntity<Dictionary<string, object>>(
                  "Customization",
                  r => r.HasOne<CustomizationChoice>().WithMany()
                      .HasForeignKey("CustomizationChoiceId")
                      .HasConstraintName("FK_Customizations_CustomizationChoice"),
                  l => l.HasOne<OrderItem>().WithMany()
                      .HasForeignKey("OrderItemId")
                      .HasConstraintName("FK__Customiza__ItemI__236943A5"),
                  j =>
                  {
                  j.HasKey("OrderItemId", "CustomizationChoiceId");
                  j.ToTable("Customizations");
                  j.IndexerProperty<int>("OrderItemId").HasColumnName("OrderItemID");
                  j.IndexerProperty<int>("CustomizationChoiceId").HasColumnName("CustomizationChoiceID");
                });
    });

    modelBuilder.Entity<Payment>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK__Payments__3214EC276DC2CF34");

      entity.Property(e => e.Id).HasColumnName("ID");
      entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
      entity.Property(e => e.CreatedAt)
              .HasDefaultValueSql("(getdate())")
              .HasColumnType("datetime");
      entity.Property(e => e.OrderId).HasColumnName("OrderID");
      entity.Property(e => e.Status).HasComment("  Pending = 1,\r\n  Processing = 2,\r\n  Authorized = 3,\r\n  Completed = 4,\r\n  Failed = 5,\r\n  Cancelled = 6,\r\n  Voided = 7,\r\n  Expired = 8,");
      entity.Property(e => e.TransactionId)
              .HasMaxLength(255)
              .IsUnicode(true)
              .HasColumnName("TransactionID");
      entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
      entity.Property(e => e.Method).HasComment("  MasterVisa = 1,\r\n  ZainCash = 2,\r\n  Other = 3");

      entity.HasOne(d => d.Order).WithMany(p => p.Payments)
              .HasForeignKey(d => d.OrderId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_Payments_Orders");
    });

    modelBuilder.Entity<Product>(entity =>
    {
      entity.ToTable("products");

      entity.HasIndex(e => e.Name, "UQ_ProductName").IsUnique();

      entity.Property(e => e.Id).HasColumnName("ID");
      entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
      entity.Property(e => e.CreatedAt)
              .HasDefaultValueSql("(getdate())")
              .HasColumnType("datetime");
      entity.Property(e => e.Description).HasColumnType("text");
      entity.Property(e => e.Name).HasMaxLength(255);
      entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
      entity.Property(e => e.Sku).HasColumnName("SKU");
      entity.Property(e => e.Slug).HasColumnName("SLUG");

      entity.HasOne(d => d.Category).WithMany(p => p.Products)
              .HasForeignKey(d => d.CategoryId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_products_Categories");
    });

    modelBuilder.Entity<Review>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK__Reviews__74BC79AEDEBDA88E");

      entity.Property(e => e.Id).HasColumnName("ID");
      entity.Property(e => e.CreatedAt)
              .HasDefaultValueSql("(getdate())")
              .HasColumnType("datetime");
      entity.Property(e => e.ProductId).HasColumnName("ProductID");
      entity.Property(e => e.UserId).HasColumnName("UserID");

      entity.HasOne(d => d.Product).WithMany(p => p.Reviews)
              .HasForeignKey(d => d.ProductId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_Reviews_products");

      entity.HasOne(d => d.User).WithMany(p => p.Reviews)
              .HasForeignKey(d => d.UserId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_Reviews_Users");
    });

    modelBuilder.Entity<ShoppingCart>(entity =>
    {
      entity.ToTable("ShoppingCart");

      entity.HasIndex(e => e.UserId, "UQ_UserID").IsUnique();

      entity.Property(e => e.Id).HasColumnName("ID");
      entity.Property(e => e.CreatedAt).HasColumnType("datetime");
      entity.Property(e => e.UserId).HasColumnName("UserID");

      entity.HasOne(d => d.User).WithOne(p => p.ShoppingCart)
              .HasForeignKey<ShoppingCart>(d => d.UserId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_ShoppingCart_Users");
    });

    modelBuilder.Entity<State>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK__States__3214EC272BDBC709");

      entity.Property(e => e.Id)
              .ValueGeneratedNever()
              .HasColumnName("ID");
      entity.Property(e => e.CountryId).HasColumnName("CountryID");
      entity.Property(e => e.Name).HasMaxLength(100);
      entity.Property(e => e.StateCode).HasMaxLength(10);

      entity.HasOne(d => d.Country).WithMany(p => p.States)
              .HasForeignKey(d => d.CountryId)
              .HasConstraintName("FK__States__CountryI__1D9B5BB6");
    });

    modelBuilder.Entity<User>(entity =>
    {
      entity.HasIndex(e => e.EmailAddress, "UniqueEmaill").IsUnique();

      entity.Property(e => e.Id)
              .ValueGeneratedNever()
              .HasColumnName("ID");
      entity.Property(e => e.CreatedAt)
              .HasDefaultValueSql("(getdate())")
              .HasColumnType("datetime");
      entity.Property(e => e.DefaultAddressId).HasColumnName("DefaultAddressID");
      entity.Property(e => e.EmailAddress).HasMaxLength(50);
      entity.Property(e => e.FirstName).HasMaxLength(50);
      entity.Property(e => e.ImageUrl).HasMaxLength(50);
      entity.Property(e => e.IsActive).HasDefaultValue(true);
      entity.Property(e => e.LastLogin).HasColumnType("datetime");
      entity.Property(e => e.LastName).HasMaxLength(50);
      entity.Property(e => e.Password).HasMaxLength(256);
      entity.Property(e => e.PhoneNumber).HasMaxLength(50);
      entity.Property(e => e.UpdatedAt)
              .HasDefaultValueSql("(getdate())")
              .HasColumnType("datetime");

      entity.HasOne(d => d.DefaultAddress).WithMany(p => p.Users)
              .HasForeignKey(d => d.DefaultAddressId)
              .HasConstraintName("FK_Users_Addresses");
    });

    modelBuilder.Entity<Video>(entity =>
    {
      entity.HasOne(d => d.Product).WithMany(p => p.Videos)
              .HasForeignKey(d => d.ProductId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_Videos_Videos_Products");
    });

    OnModelCreatingPartial(modelBuilder);
  }

  partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
