using Domain.ApiResponse;
using Domain.DTOs.SupplierDTOs;
using Domain.Filter;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SupplierController(ISupplierService service)
{
    [HttpGet]
    public async Task<PagedResponse<List<SupplierDTO>>> GetAllAsync([FromQuery] SupplierFilter filter)
    {
        return await service.GetAllAsync(filter);
    }

    [HttpGet("{id}")]
    public async Task<Response<SupplierDTO>> GetByIdAsync(int id)
    {
        return await service.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task<Response<string>> CreateAsync(CreateSupplierDTO create)
    {
        return await service.CreateAsync(create);
    }

    [HttpPut]
    public async Task<Response<string>> UpdateAsync(SupplierDTO update)
    {
        return await service.UpdateAsync(update);
    }

    [HttpDelete("{id}")]
    public async Task<Response<string>> DeleteAsync(int id)
    {
        return await service.DeleteAsync(id);
    }

    [HttpGet("with-products")]
    public async Task<Response<List<SupplierWithProductsDTO>>> GetSuppliersWithProductsAsync()
    {
        return await service.GetSuppliersWithProductsAsync();
    }
}
