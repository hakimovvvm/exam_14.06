using Domain.ApiResponse;
using Domain.DTOs.StockAdjustmentDTOs;
using Domain.Filter;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StockAdjustmentController(IStockAdjustmentService service)
{
    [HttpGet("{id}")]
    public async Task<Response<StockAdjustmentDTO>> GetByIdAsync(int id)
    {
        return await service.GetByIdAsync(id);
    }

    [HttpGet]
    public async Task<PagedResponse<List<StockAdjustmentDTO>>> GetAllAsync([FromQuery] StockAdjustmentFilter filter)
    {
        return await service.GetAllAsync(filter);
    }

    [HttpPost]
    public async Task<Response<string>> CreateAsync(CreateStockAdjustmentDTO create)
    {
        return await service.CreateAsync(create);
    }

    [HttpPut]
    public async Task<Response<string>> UpdateAsync(StockAdjustmentDTO update)
    {
        return await service.UpdateAsync(update);
    }

    [HttpDelete("{id}")]
    public async Task<Response<string>> DeleteAsync(int id)
    {
        return await service.DeleteAsync(id);
    }

    [HttpGet("by-product/{productId}")]
    public async Task<Response<List<StockAdjustmentDTO>>> GetAdjustmentsByProductIdAsync(int productId)
    {
        return await service.GetAdjustmentsByProductIdAsync(productId);
    }

    [HttpGet("statistics")]
    public async Task<Response<StockAdjustmentStatisticsDTO>> GetAdjustmentStatisticsAsync()
    {
        return await service.GetAdjustmentStatisticsAsync();
    }

    [HttpGet("recent/{count}")]
    public async Task<Response<List<StockAdjustmentDTO>>> GetRecentAdjustmentsAsync(int count)
    {
        return await service.GetRecentAdjustmentsAsync(count);
    }
}
