using AutoMapper;

using MediatR;

using OSBM.Admin.Application.Contracts.Repositories;
using OSBM.Admin.Application.DTOs.Products;

namespace OSBM.Admin.Application.Features.Products.Queries;
public record GetProductByIdQuery(long Id) : IRequest<ProductDto?>;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto?>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public GetProductByIdQueryHandler(IMapper mapper, IProductRepository productRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var productFound = _productRepository.Find(request.Id);
        if (productFound == null)
        {
            await Task.CompletedTask;
            return null;
        }
        var response = _mapper.Map<ProductDto>(productFound);
        return await Task.FromResult(response);
    }
}