namespace Domain.DTOs;

public class StockAdjustment
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int AdjustmentAmount { get; set; }
    public string? Reason { get; set; }
    public DateTime AdjustmentDate { get; set; }

    public Product Product { get; set; }
}
