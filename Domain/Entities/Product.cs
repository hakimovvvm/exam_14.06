using System.Runtime.CompilerServices;

namespace Domain.DTOs;

public class Product
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public int QuantityInStock { get; set; }
    public int CategoryId { get; set; }
    public int SupplierId { get; set; }

    public Category Category { get; set; }
    public Supplier Supplier { get; set; }
    public List<Sale> Sales { get; set; }  
    public List<StockAdjustment> StockAdjustments { get; set; }
}
