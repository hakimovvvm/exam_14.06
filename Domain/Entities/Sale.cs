namespace Domain.DTOs;

public class Sale
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int QuantitySold { get; set; }
    public DateTime SaleDate { get; set; }

    public Product Product { get; set; }
}
