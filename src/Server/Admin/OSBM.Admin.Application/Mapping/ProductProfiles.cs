using AutoMapper;

using OSBM.Admin.Application.DTOs.Products;
using OSBM.Admin.Application.Features.Products.Commands;
using OSBM.Admin.Domain.Aggregates.Products;

namespace OSBM.Admin.Application.Mapping;

internal class ProductProfiles : Profile
{
    public ProductProfiles()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<CreateProductCommand, Product>();
    }
}