using System.Net;
using AutoMapper;
using Domain.ApiResponse;
using Domain.DTOs;
using Domain.DTOs.StockAdjustmentDTOs;
using Domain.Filter;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class StockAdjustmentService(DataContext context, IMapper mapper) : IStockAdjustmentService
{
    public async Task<Response<StockAdjustmentDTO>> GetByIdAsync(int id)
    {
        var adjustment = await context.StockAdjustments
            .Include(sa => sa.Product)
            .FirstOrDefaultAsync(sa => sa.Id == id);

        if (adjustment == null)
            return new Response<StockAdjustmentDTO>(HttpStatusCode.NotFound, "adjustment not found");

        var dto = mapper.Map<StockAdjustmentDTO>(adjustment);
        return new Response<StockAdjustmentDTO>(dto);
    }

    public async Task<PagedResponse<List<StockAdjustmentDTO>>> GetAllAsync(StockAdjustmentFilter filter)
    {
        var query = context.StockAdjustments
            .Include(sa => sa.Product)
            .AsQueryable();

        if (filter.ProductId.HasValue)
            query = query.Where(sa => sa.ProductId == filter.ProductId);

        if (filter.FromDate.HasValue)
            query = query.Where(sa => sa.AdjustmentDate >= filter.FromDate.Value);

        if (filter.ToDate.HasValue)
            query = query.Where(sa => sa.AdjustmentDate <= filter.ToDate.Value);

        int totalResults = await query.CountAsync();

        var adjustments = await query
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();

        var data = mapper.Map<List<StockAdjustmentDTO>>(adjustments);
        var totalPages = (int)Math.Ceiling(totalResults / (double)filter.PageSize);

        return new PagedResponse<List<StockAdjustmentDTO>>(data, totalPages, filter.PageNumber, filter.PageSize);
    }


    public async Task<Response<string>> CreateAsync(CreateStockAdjustmentDTO create)
    {
        var adjustment = mapper.Map<StockAdjustment>(create);
        context.StockAdjustments.Add(adjustment);
        await context.SaveChangesAsync();

        return new Response<string>(null!, "successfully");
    }

    public async Task<Response<string>> UpdateAsync(StockAdjustmentDTO update)
    {
        var adjustment = await context.StockAdjustments.FindAsync(update.Id);

        if (adjustment == null)
            return new Response<string>(HttpStatusCode.NotFound, "adjustment not found");

        mapper.Map(update, adjustment);
        await context.SaveChangesAsync();

        return new Response<string>(null!, "successfully");
    }

    public async Task<Response<string>> DeleteAsync(int id)
    {
        var adjustment = await context.StockAdjustments.FindAsync(id);

        if (adjustment == null)
            return new Response<string>(HttpStatusCode.NotFound, "adjustment not found");

        context.StockAdjustments.Remove(adjustment);
        await context.SaveChangesAsync();

        return new Response<string>(null!, "successfully");
    }

    public async Task<Response<List<StockAdjustmentDTO>>> GetAdjustmentsByProductIdAsync(int productId)
    {
        var adjustments = await context.StockAdjustments
            .Where(sa => sa.ProductId == productId)
            .Include(sa => sa.Product)
            .ToListAsync();

        var data = mapper.Map<List<StockAdjustmentDTO>>(adjustments);
        return new Response<List<StockAdjustmentDTO>>(data);
    }

    public async Task<Response<StockAdjustmentStatisticsDTO>> GetAdjustmentStatisticsAsync()
    {
        var totalAdjustments = await context.StockAdjustments.CountAsync();
        var totalQuantity = await context.StockAdjustments.SumAsync(sa => sa.AdjustmentAmount);

        var stats = new StockAdjustmentStatisticsDTO
        {
            TotalAdjustments = totalAdjustments,
            TotalQuantityAdjusted = totalQuantity
        };

        return new Response<StockAdjustmentStatisticsDTO>(stats);
    }

    public async Task<Response<List<StockAdjustmentDTO>>> GetRecentAdjustmentsAsync(int count)
    {
        var adjustments = await context.StockAdjustments
            .Include(sa => sa.Product)
            .OrderByDescending(sa => sa.AdjustmentDate)
            .Take(count)
            .ToListAsync();

        var data = mapper.Map<List<StockAdjustmentDTO>>(adjustments);
        return new Response<List<StockAdjustmentDTO>>(data);
    }
}
