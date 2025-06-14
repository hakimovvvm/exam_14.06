using System.Net;
using AutoMapper;
using Domain.ApiResponse;
using Domain.DTOs;
using Domain.DTOs.CategoryDTO;
using Domain.Filter;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class CategoryService(DataContext context, IMapper mapper) : ICategoryService
{
    public async Task<Response<CategoryDTO>> GetByIdAsync(int id)
    {
        var category = await context.Categories.FindAsync(id);

        if (category == null)
            return new Response<CategoryDTO>(HttpStatusCode.NotFound, "category not found");

        var dto = mapper.Map<CategoryDTO>(category);
        return new Response<CategoryDTO>(dto);
    }

    public async Task<PagedResponse<List<CategoryDTO>>> GetAllAsync(CategoryFilter filter)
    {
        var query = context.Categories.AsQueryable();

        if (!string.IsNullOrEmpty(filter.Name))
            query = query.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));

        int totalResults = await query.CountAsync();

        var categories = await query
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();

        var data = mapper.Map<List<CategoryDTO>>(categories);
        var totalPages = (int)Math.Ceiling(totalResults / (double)filter.PageSize);

        return new PagedResponse<List<CategoryDTO>>(data, totalPages, filter.PageNumber, filter.PageSize);
    }

    public async Task<Response<string>> CreateAsync(CreateCategoryDTO create)
    {
        var category = mapper.Map<Category>(create);

        context.Categories.Add(category);
        await context.SaveChangesAsync();

        return new Response<string>(null!, "successfully");
    }

    public async Task<Response<string>> UpdateAsync(CategoryDTO update)
    {
        var category = await context.Categories.FindAsync(update.Id);

        if (category == null)
            return new Response<string>(HttpStatusCode.NotFound, "category not found");

        mapper.Map(update, category);
        await context.SaveChangesAsync();

        return new Response<string>(null!, "successfully");
    }

    public async Task<Response<string>> DeleteAsync(int id)
    {
        var category = await context.Categories.FindAsync(id);

        if (category == null)
            return new Response<string>(HttpStatusCode.NotFound, "category not found");

        context.Categories.Remove(category);
        await context.SaveChangesAsync();

        return new Response<string>(null!, "successfully");
    }

    public async Task<Response<List<CategoryWithProductsDTO>>> GetCategoriesWithProductsAsync()
    {
        var categories = await context.Categories
            .Include(c => c.Products)
            .ToListAsync();

        var data = mapper.Map<List<CategoryWithProductsDTO>>(categories);
        return new Response<List<CategoryWithProductsDTO>>(data);
    }
}
