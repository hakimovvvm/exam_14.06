using Domain.DTOs.ProductDTOs;

namespace Domain.DTOs.CategoryDTO;

public class CategoryWithProductsDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public List<ProductDTO> Products { get; set; } = new();
}
