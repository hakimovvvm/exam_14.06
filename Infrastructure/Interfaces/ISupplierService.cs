using Domain.ApiResponse;
using Domain.DTOs.SupplierDTOs;
using Domain.Filter;

namespace Infrastructure.Interfaces;

public interface ISupplierService
{
    Task<PagedResponse<List<SupplierDTO>>> GetAllAsync(SupplierFilter filter);
    Task<Response<SupplierDTO>> GetByIdAsync(int id);
    Task<Response<string>> CreateAsync(CreateSupplierDTO create);
    Task<Response<string>> UpdateAsync(SupplierDTO update);
    Task<Response<string>> DeleteAsync(int id);
    Task<Response<List<SupplierWithProductsDTO>>> GetSuppliersWithProductsAsync();
}
