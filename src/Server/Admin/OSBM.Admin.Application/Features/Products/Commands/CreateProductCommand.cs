using AutoMapper;

using MediatR;

using OSBM.Admin.Application.Contracts.Repositories;
using OSBM.Admin.Application.DTOs.Products;
using OSBM.Admin.Domain.Entities;

namespace OSBM.Admin.Application.Features.Products.Commands;

public record CreateProductCommand(string Name, string? Brief, string? Description, string? ThumbnailUrl) : IRequest<ProductDto?>;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto?>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public CreateProductCommandHandler(IMapper mapper, IProductRepository productRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public async Task<ProductDto?> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Product>(request);
        _productRepository.Add(product);
        _productRepository.SaveChanges();
        var response = _mapper.Map<ProductDto>(product);
        return await Task.FromResult(response);
    }
}