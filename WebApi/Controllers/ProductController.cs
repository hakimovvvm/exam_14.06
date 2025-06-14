using Domain.ApiResponse;
using Domain.DTOs.ProductDTOs;
using Domain.Filter;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController(IProductService service)
{
    [HttpGet("{id}")]
    public async Task<Response<ProductDTO>> GetByIdAsync(int id)
    {
        return await service.GetByIdAsync(id);
    }

    [HttpGet]
    public async Task<PagedResponse<List<ProductDTO>>> GetAllAsync([FromQuery] ProductFilter filter)
    {
        return await service.GetAllAsync(filter);
    }

    [HttpPost]
    public async Task<Response<string>> CreateAsync(CreateProductDTO create)
    {
        return await service.CreateAsync(create);
    }

    [HttpPut]
    public async Task<Response<string>> UpdateAsync(ProductDTO update)
    {
        return await service.UpdateAsync(update);
    }

    [HttpDelete("{id}")]
    public async Task<Response<string>> DeleteAsync(int id)
    {
        return await service.DeleteAsync(id);
    }

    [HttpGet("low-stock")]
    public async Task<Response<List<ProductDTO>>> GetLowStockProductsAsync()
    {
        return await service.GetLowStockProductsAsync();
    }

    [HttpGet("statistics")]
    public async Task<Response<ProductStatisticsDTO>> GetProductStatisticsAsync()
    {
        return await service.GetProductStatisticsAsync();
    }

    [HttpGet("details/{id}")]
    public async Task<Response<ProductDetailsDTO>> GetProductDetailsAsync(int id)
    {
        return await service.GetProductDetailsAsync(id);
    }
}
