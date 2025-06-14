namespace Domain.Filter;

public class StockAdjustmentFilter : ValidFilter
{
    public int? ProductId { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}
