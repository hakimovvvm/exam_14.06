using AutoMapper;
using Domain.DTOs;
using Domain.DTOs.CategoryDTO;
using Domain.DTOs.ProductDTOs;
using Domain.DTOs.SaleDTOs;
using Domain.DTOs.StockAdjustmentDTOs;
using Domain.DTOs.SupplierDTOs;

namespace Infrastructure.AutoMapper;

public class InfrastructureProfile : Profile
{
    public InfrastructureProfile()
    {
        CreateMap<Product, ProductDTO>().ReverseMap();

        CreateMap<CreateCategoryDTO, Product>().ReverseMap();
        CreateMap<ProductDTO, Product>().ReverseMap();
        CreateMap<ProductDetailsDTO, Product>().ReverseMap();
        CreateMap<ProductStatisticsDTO, Product>().ReverseMap();

        CreateMap<Category, CategoryDTO>().ReverseMap();
        CreateMap<CreateCategoryDTO, Category>().ReverseMap();
        CreateMap<Category, CategoryWithProductsDTO>().ReverseMap();

        CreateMap<Supplier, SupplierDTO>().ReverseMap();
        CreateMap<CreateSupplierDTO, Supplier>().ReverseMap();
        CreateMap<Supplier, SupplierWithProductsDTO>().ReverseMap();

        CreateMap<Sale, SaleDTO>().ReverseMap();
        CreateMap<CreateSaleDTO, Sale>().ReverseMap();

        CreateMap<StockAdjustment, StockAdjustmentDTO>().ReverseMap();
        CreateMap<CreateStockAdjustmentDTO, StockAdjustment>().ReverseMap();
    }
}
