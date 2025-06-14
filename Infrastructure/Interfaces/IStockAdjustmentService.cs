using Domain.ApiResponse;
using Domain.DTOs.StockAdjustmentDTOs;
using Domain.Filter;

namespace Infrastructure.Interfaces;

public interface IStockAdjustmentService
{
    Task<Response<StockAdjustmentDTO>> GetByIdAsync(int id);
    Task<PagedResponse<List<StockAdjustmentDTO>>> GetAllAsync(StockAdjustmentFilter filter);
    Task<Response<string>> CreateAsync(CreateStockAdjustmentDTO create);
    Task<Response<string>> UpdateAsync(StockAdjustmentDTO update);
    Task<Response<string>> DeleteAsync(int id);

    Task<Response<List<StockAdjustmentDTO>>> GetAdjustmentsByProductIdAsync(int productId);
    Task<Response<StockAdjustmentStatisticsDTO>> GetAdjustmentStatisticsAsync();
    Task<Response<List<StockAdjustmentDTO>>> GetRecentAdjustmentsAsync(int count);
}
