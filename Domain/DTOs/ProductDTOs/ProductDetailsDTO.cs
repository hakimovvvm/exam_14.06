using Domain.DTOs.SaleDTOs;
using Domain.DTOs.StockAdjustmentDTOs;

namespace Domain.DTOs.ProductDTOs;

public class ProductDetailsDTO : ProductDTO
{
    public List<SaleDTO> Sales { get; set; }
    public List<StockAdjustmentDTO> Adjustments { get; set; }
}
