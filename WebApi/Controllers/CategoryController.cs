using Domain.ApiResponse;
using Domain.DTOs.CategoryDTO;
using Domain.Filter;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController(ICategoryService service)
{
    [HttpGet("{id}")]
    public async Task<Response<CategoryDTO>> GetByIdAsync(int id)
    {
        return await service.GetByIdAsync(id);
    }
    [HttpGet]
    public async Task<PagedResponse<List<CategoryDTO>>> GetAllAsync(CategoryFilter filter)
    {
        return await service.GetAllAsync(filter);
    }
    [HttpPost]
    public async Task<Response<string>> CreateAsync(CreateCategoryDTO create)
    {
        return await service.CreateAsync(create);
    }
    [HttpPut]
    public async Task<Response<string>> UpdateAsync(CategoryDTO update)
    {
        return await service.UpdateAsync(update);
    }
    [HttpDelete("{id}")]
    public async Task<Response<string>> DeleteAsync(int id)
    {
        return await service.DeleteAsync(id);
    }
    [HttpGet("with-products")]
    public async Task<Response<List<CategoryWithProductsDTO>>> GetCategoriesWithProductsAsync()
    {
        return await service.GetCategoriesWithProductsAsync();
    }

}
