using Domain.DTOs.ProductDTOs;

namespace Domain.DTOs.SupplierDTOs;

public class SupplierWithProductsDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string ContactInfo { get; set; } = null!;
    public List<ProductDTO> Products { get; set; } 
}
