using Microsoft.EntityFrameworkCore;
using OnlineStore.Core.Entities;

namespace OnlineStore.Infrastructure.Data.Models;
public partial class EStoreSystemContext : DbContext
{
  partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
  {
//    modelBuilder.Entity<Product>(entity => entity.HasMany(p => p.CustomizationOptions)
//.WithMany(p => p.Products)
//.UsingEntity<Dictionary<string, object>>(
//    "ProductCustomizationAvailability",
//    l => l.HasOne<CustomizationOption>().WithMany().HasForeignKey("CustomizationOptionID"),
//    r => r.HasOne<Product>().WithMany().HasForeignKey("ProductID"),
//    j =>
//    {
//      j.HasKey("CustomizationOptionID", "ProductID");
//      j.ToTable("ProductCustomizationAvailability");
//    }
//   ));

    modelBuilder.Entity<Order>(entity =>
    {
      // This fixes naming error in the original estoresystemcontext class
      entity.HasOne(o => o.Customer).WithMany(c => c.Orders)
              .HasForeignKey(o => o.CustomerId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_Orders_Customers");
    });
  }
}
