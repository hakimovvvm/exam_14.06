using Domain.ApiResponse;
using Domain.DTOs.SaleDTOs;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SaleController(ISaleService service)
{
    [HttpGet]
    public async Task<Response<List<SaleDTO>>> GetAllAsync()
    {
        return await service.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<Response<SaleDTO>> GetByIdAsync(int id)
    {
        return await service.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task<Response<string>> CreateAsync(CreateSaleDTO create)
    {
        return await service.CreateAsync(create);
    }

    [HttpDelete("{id}")]
    public async Task<Response<string>> DeleteAsync(int id)
    {
        return await service.DeleteAsync(id);
    }

    [HttpGet("total-revenue")]
    public async Task<Response<decimal>> GetTotalRevenueAsync()
    {
        return await service.GetTotalRevenueAsync();
    }

    [HttpGet("total-quantity-sold")]
    public async Task<Response<int>> GetTotalQuantitySoldAsync()
    {
        return await service.GetTotalQuantitySoldAsync();
    }
}
