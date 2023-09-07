using MediatR;

using OSBM.Admin.Application.Contracts.Repositories;
using OSBM.Admin.Application.DTOs.Products;
using OSBM.Admin.Shared.Models.ApiRequest;
using OSBM.Admin.Shared.Models.ApiResponse;

namespace OSBM.Admin.Application.Features.Products.Queries;

public class GetProductsWithPaginationQuery : PaginationApiRequestModel, IRequest<PaginationResponseModel<ProductDto>>
{
}

public class GetProductsWithPaginationQueryHandler : IRequestHandler<GetProductsWithPaginationQuery, PaginationResponseModel<ProductDto>>
{
    private readonly IProductRepository _productRepository;

    public GetProductsWithPaginationQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<PaginationResponseModel<ProductDto>> Handle(GetProductsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var productsQuery = _productRepository
            .FindByCondition()
            .Select(x => new ProductDto(x.Id, x.Name, x.Brief, x.Description, x.ThumbnailUrl));
        var productsResponse = await PaginationResponseModel<ProductDto>
            .CreateAsync(productsQuery, request.PageSize, request.PageIndex, cancellationToken);

        return productsResponse;
    }
}