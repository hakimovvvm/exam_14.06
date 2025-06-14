using AutoMapper;
using Domain.ApiResponse;
using Domain.DTOs;
using Domain.DTOs.SupplierDTOs;
using Domain.Filter;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Infrastructure.Services;

public class SupplierService(DataContext context, IMapper mapper) : ISupplierService
{
    public async Task<Response<string>> CreateAsync(CreateSupplierDTO create)
    {
        var supplier = mapper.Map<Supplier>(create);
        context.Suppliers.Add(supplier);
        await context.SaveChangesAsync();

        return new Response<string>(null!, "successfully.");
    }

    public async Task<Response<string>> DeleteAsync(int id)
    {
        var supplier = await context.Suppliers.FindAsync(id);

        if (supplier == null)
            return new Response<string>(HttpStatusCode.NotFound, "supplier not found.");

        context.Suppliers.Remove(supplier);
        await context.SaveChangesAsync();

        return new Response<string>(null!, "successfully.");
    }

    public async Task<PagedResponse<List<SupplierDTO>>> GetAllAsync(SupplierFilter filter)
    {
        var query = context.Suppliers.AsQueryable();

        if (!string.IsNullOrEmpty(filter.Name))
            query = query.Where(s => s.Name.ToLower().Contains(filter.Name.ToLower()));

        var total = await query.CountAsync();

        var data = await query
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();

        var result = mapper.Map<List<SupplierDTO>>(data);
        var totalPages = (int)Math.Ceiling(total / (double)filter.PageSize);

        return new PagedResponse<List<SupplierDTO>>(result, totalPages, filter.PageNumber, filter.PageSize);
    }

    public async Task<Response<SupplierDTO>> GetByIdAsync(int id)
    {
        var supplier = await context.Suppliers.FindAsync(id);

        if (supplier == null)
            return new Response<SupplierDTO>(HttpStatusCode.NotFound, "supplier not found.");

        var dto = mapper.Map<SupplierDTO>(supplier);
        return new Response<SupplierDTO>(dto);
    }

    public async Task<Response<string>> UpdateAsync(SupplierDTO update)
    {
        var supplier = await context.Suppliers.FindAsync(update.Id);

        if (supplier == null)
            return new Response<string>(HttpStatusCode.NotFound, "supplier not found.");

        mapper.Map(update, supplier);
        await context.SaveChangesAsync();

        return new Response<string>(null!, "successfully.");
    }

    public async Task<Response<List<SupplierWithProductsDTO>>> GetSuppliersWithProductsAsync()
    {
        var suppliers = await context.Suppliers
            .Include(s => s.Products)
            .ToListAsync();

        var result = mapper.Map<List<SupplierWithProductsDTO>>(suppliers);
        return new Response<List<SupplierWithProductsDTO>>(result);
    }
}
