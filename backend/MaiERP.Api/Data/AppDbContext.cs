using MaiERP.Api.Models.Items;
using Microsoft.EntityFrameworkCore;

namespace MaiERP.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Item> Items => Set<Item>();
    public DbSet<ItemCategory> ItemCategories => Set<ItemCategory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ItemCategory>()
            .HasIndex(c => c.Code)
            .IsUnique();

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasIndex(i => i.Code).IsUnique();

            entity.Property(i => i.SupplyType)
                .HasConversion<string>()
                .HasMaxLength(20);

            entity.Property(i => i.MinimumStock).HasPrecision(18, 3);
            entity.Property(i => i.SafetyStock).HasPrecision(18, 3);

            entity.HasOne(i => i.ItemCategory)
                .WithMany()
                .HasForeignKey(i => i.ItemCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}