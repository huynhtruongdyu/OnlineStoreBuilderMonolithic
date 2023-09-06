using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

using OSBM.Admin.Application.Contracts.Repositories;
using OSBM.Admin.Application.DTOs.Products;

namespace OSBM.Admin.Application.Features.Products.Queries;

public record GetAllProductQuery : IRequest<IReadOnlyCollection<ProductDto>>;

public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, IReadOnlyCollection<ProductDto>>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public GetAllProductQueryHandler(IMapper mapper, IProductRepository productRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public async Task<IReadOnlyCollection<ProductDto>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository
                .FindAll()
                .Select(x => new ProductDto(x.Id, x.Name, x.Brief, x.Description, x.ThumbnailUrl))
                .ToListAsync(cancellationToken);
        return products;
    }
}