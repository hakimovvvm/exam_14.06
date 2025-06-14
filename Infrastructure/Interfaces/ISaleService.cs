using Domain.ApiResponse;
using Domain.DTOs.SaleDTOs;

namespace Infrastructure.Interfaces;

public interface ISaleService
{
    public Task<Response<List<SaleDTO>>> GetAllAsync();
    public Task<Response<SaleDTO>> GetByIdAsync(int id);
    public Task<Response<string>> CreateAsync(CreateSaleDTO create);
    public Task<Response<string>> DeleteAsync(int id);
    public Task<Response<decimal>> GetTotalRevenueAsync();
    public Task<Response<int>> GetTotalQuantitySoldAsync();
}
