using System.Security.Cryptography.X509Certificates;

namespace Domain.Filter;

public class ProductFilter : ValidFilter
{
    public string? Name { get; set; }
    public int? MaxPrice { get; set; }
    public int? MinPrice { get; set; }
    public int? CategoryId { get; set; }
    public int? SupplierId { get; set; }

    public ProductFilter(string Name, int MaxPrice, int MinPrice)
    {
        this.Name = Name;
        this.MaxPrice = MaxPrice;
        this.MinPrice = MinPrice;
    }
}
