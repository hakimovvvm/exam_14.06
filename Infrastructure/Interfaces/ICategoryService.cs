using System.Dynamic;
using Domain.ApiResponse;
using Domain.DTOs;
using Domain.DTOs.CategoryDTO;
using Domain.Filter;

namespace Infrastructure.Interfaces;

public interface ICategoryService
{
    Task<Response<CategoryDTO>> GetByIdAsync(int id);
    Task<PagedResponse<List<CategoryDTO>>> GetAllAsync(CategoryFilter filter);
    Task<Response<string>> CreateAsync(CreateCategoryDTO create);
    Task<Response<string>> UpdateAsync(CategoryDTO update);
    Task<Response<string>> DeleteAsync(int id);
    Task<Response<List<CategoryWithProductsDTO>>> GetCategoriesWithProductsAsync();
}
