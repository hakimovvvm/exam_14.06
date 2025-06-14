using System.Net;
using AutoMapper;
using Domain.ApiResponse;
using Domain.DTOs;
using Domain.DTOs.ProductDTOs;
using Domain.Filter;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class ProductService(DataContext context, IMapper mapper) : IProductService
{
    public async Task<Response<ProductDTO>> GetByIdAsync(int id)
    {
        var product = await context.Products
            .Include(p => p.Category)
            .Include(p => p.Supplier)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
            return new Response<ProductDTO>(HttpStatusCode.NotFound, "product not found");

        var productDto = mapper.Map<ProductDTO>(product);
        return new Response<ProductDTO>(productDto);
    }

    public async Task<PagedResponse<List<ProductDTO>>> GetAllAsync(ProductFilter filter)
    {
        var query = context.Products
            .Include(p => p.Category)
            .Include(p => p.Supplier)
            .AsQueryable();

        if (!string.IsNullOrEmpty(filter.Name))
            query = query.Where(p => p.Name.ToLower().Contains(filter.Name.ToLower()));

        if (filter.CategoryId.HasValue)
            query = query.Where(p => p.CategoryId == filter.CategoryId);

        if (filter.SupplierId.HasValue)
            query = query.Where(p => p.SupplierId == filter.SupplierId);

        int totalResults = await query.CountAsync();

        var products = await query
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();

        var data = mapper.Map<List<ProductDTO>>(products);
        var totalPages = (int)Math.Ceiling(totalResults / (double)filter.PageSize);

        return new PagedResponse<List<ProductDTO>>(data, totalPages, filter.PageNumber, filter.PageSize);
    }

    public async Task<Response<string>> CreateAsync(CreateProductDTO create)
    {
        var product = mapper.Map<Product>(create);

        context.Products.Add(product);
        await context.SaveChangesAsync();

        return new Response<string>(null!, "successfully");
    }

    public async Task<Response<string>> UpdateAsync(ProductDTO update)
    {
        var product = await context.Products.FindAsync(update.Id);

        if (product == null)
            return new Response<string>(HttpStatusCode.NotFound, "product not found");

        mapper.Map(update, product);
        await context.SaveChangesAsync();

        return new Response<string>(null!, "successfully");
    }

    public async Task<Response<string>> DeleteAsync(int id)
    {
        var product = await context.Products.FindAsync(id);

        if (product == null)
            return new Response<string>(HttpStatusCode.NotFound, "product not found");

        context.Products.Remove(product);
        await context.SaveChangesAsync();

        return new Response<string>(null!, "successfully");
    }

    public async Task<Response<List<ProductDTO>>> GetLowStockProductsAsync()
    {
        var products = await context.Products
            .Where(p => p.QuantityInStock < 5)
            .ToListAsync();

        var data = mapper.Map<List<ProductDTO>>(products);

        return new Response<List<ProductDTO>>(data);
    }

    public async Task<Response<ProductStatisticsDTO>> GetProductStatisticsAsync()
    {
        var totalProducts = await context.Products.CountAsync();
        var averagePrice = await context.Products.AverageAsync(p => p.Price);
        var totalSold = await context.Sales.SumAsync(s => s.QuantitySold);

        var stats = new ProductStatisticsDTO
        {
            TotalProducts = totalProducts,
            AveragePrice = averagePrice,
            TotalSold = totalSold
        };

        return new Response<ProductStatisticsDTO>(stats);
    }

    public async Task<Response<ProductDetailsDTO>> GetProductDetailsAsync(int id)
    {
        var product = await context.Products
            .Include(p => p.Category)
            .Include(p => p.Supplier)
            .Include(p => p.Sales)
            .Include(p => p.StockAdjustments)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
            return new Response<ProductDetailsDTO>(HttpStatusCode.NotFound, "product not found");

        var productDetails = mapper.Map<ProductDetailsDTO>(product);
        return new Response<ProductDetailsDTO>(productDetails);
    }
}
