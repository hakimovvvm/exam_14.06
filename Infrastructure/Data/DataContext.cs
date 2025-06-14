using Domain.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<StockAdjustment> StockAdjustments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<Product>()
            .Property(p => p.Name).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<Product>()
            .Property(p => p.Price).HasColumnType("decimal(18,2)");
        modelBuilder.Entity<Product>()
            .Property(p => p.QuantityInStock).IsRequired();

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);

        modelBuilder.Entity<Product>().
            HasOne(p => p.Supplier)
            .WithMany(s => s.Products)
            .HasForeignKey(p => p.SupplierId);

        modelBuilder.Entity<Category>()
            .HasKey(c => c.Id);
        modelBuilder.Entity<Category>()
            .Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(50);

        modelBuilder.Entity<Supplier>()
            .HasKey(s => s.Id);
        modelBuilder.Entity<Supplier>()
            .Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(100);
        modelBuilder.Entity<Supplier>()
            .Property(s => s.Phone)
            .HasMaxLength(20);

        modelBuilder.Entity<Sale>()
            .HasKey(s => s.Id);
        modelBuilder.Entity<Sale>()
            .Property(s => s.QuantitySold)
            .IsRequired();
        modelBuilder.Entity<Sale>()
            .HasOne(s => s.Product)
            .WithMany(p => p.Sales)
            .HasForeignKey(s => s.ProductId);

        modelBuilder.Entity<StockAdjustment>()
            .HasKey(a => a.Id);
        modelBuilder.Entity<StockAdjustment>()
            .Property(a => a.AdjustmentAmount)
            .IsRequired();
        modelBuilder.Entity<StockAdjustment>()
            .Property(a => a.Reason)
            .HasMaxLength(200);
        modelBuilder.Entity<StockAdjustment>()
            .HasOne(a => a.Product)
            .WithMany(p => p.StockAdjustments)
            .HasForeignKey(a => a.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
