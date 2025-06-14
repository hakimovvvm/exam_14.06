using Domain.ApiResponse;
using Domain.DTOs.ProductDTOs;
using Domain.Filter;

namespace Infrastructure.Interfaces;

public interface IProductService
{
    Task<Response<ProductDTO>> GetByIdAsync(int id);
    Task<PagedResponse<List<ProductDTO>>> GetAllAsync(ProductFilter filter);
    Task<Response<string>> CreateAsync(CreateProductDTO create);
    Task<Response<string>> UpdateAsync(ProductDTO update);
    Task<Response<string>> DeleteAsync(int id);

    Task<Response<List<ProductDTO>>> GetLowStockProductsAsync();
    Task<Response<ProductStatisticsDTO>> GetProductStatisticsAsync();
    Task<Response<ProductDetailsDTO>> GetProductDetailsAsync(int id);
}
