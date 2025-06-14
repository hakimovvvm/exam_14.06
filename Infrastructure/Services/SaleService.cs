using System.Net;
using AutoMapper;
using Domain.ApiResponse;
using Domain.DTOs;
using Domain.DTOs.SaleDTOs;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class SaleService(DataContext context, IMapper mapper) : ISaleService
{
    public async Task<Response<List<SaleDTO>>> GetAllAsync()
    {
        var sales = await context.Sales
            .Include(s => s.Product)
            .ToListAsync();

        var data = mapper.Map<List<SaleDTO>>(sales);
        return new Response<List<SaleDTO>>(data);
    }

    public async Task<Response<SaleDTO>> GetByIdAsync(int id)
    {
        var sale = await context.Sales
            .Include(s => s.Product)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (sale == null)
            return new Response<SaleDTO>(HttpStatusCode.NotFound, "sale not found");

        var dto = mapper.Map<SaleDTO>(sale);
        return new Response<SaleDTO>(dto);
    }

    public async Task<Response<string>> CreateAsync(CreateSaleDTO create)
    {
        var product = await context.Products.FindAsync(create.ProductId);
        if (product == null)
            return new Response<string>(HttpStatusCode.NotFound, "product not found");

        if (product.QuantityInStock < create.QuantitySold)
            return new Response<string>(HttpStatusCode.BadRequest, "not enough stock");

        var sale = mapper.Map<Sale>(create);
        product.QuantityInStock -= create.QuantitySold;

        context.Sales.Add(sale);
        await context.SaveChangesAsync();

        return new Response<string>(null!, "successfully");
    }

    public async Task<Response<string>> DeleteAsync(int id)
    {
        var sale = await context.Sales.FindAsync(id);
        if (sale == null)
            return new Response<string>(HttpStatusCode.NotFound, "sale not found");

        context.Sales.Remove(sale);
        await context.SaveChangesAsync();

        return new Response<string>(null!, "successfully");
    }

    public async Task<Response<decimal>> GetTotalRevenueAsync()
    {
        var revenue = await context.Sales
            .Include(s => s.Product)
            .SumAsync(s => s.Product.Price * s.QuantitySold);

        return new Response<decimal>(revenue);
    }

    public async Task<Response<int>> GetTotalQuantitySoldAsync()
    {
        var totalSold = await context.Sales.SumAsync(s => s.QuantitySold);
        return new Response<int>(totalSold);
    }
}
