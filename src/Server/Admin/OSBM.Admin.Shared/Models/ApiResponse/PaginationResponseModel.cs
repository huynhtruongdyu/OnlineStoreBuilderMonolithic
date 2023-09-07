using Microsoft.EntityFrameworkCore;

namespace OSBM.Admin.Shared.Models.ApiResponse;

public class PaginationResponseModel<T>
{
    public int TotalItems { get; }
    public int PageSize { get; }
    public int PageCount { get; }
    public int PageIndex { get; }

    public bool CanGoNext => PageIndex * PageSize < TotalItems;
    public bool CanGoPrevious => PageIndex > 1;

    public List<T> Items { get; }

    private PaginationResponseModel(List<T> items, int pageIndex, int pageSize, int totalItems)
    {
        Items = items;
        PageSize = pageSize;
        TotalItems = totalItems;
        PageIndex = pageIndex;
        PageCount = items?.Count() ?? 0;
    }

    public static async Task<PaginationResponseModel<T>> CreateAsync(IQueryable<T> query, int pageSize, int pageIndex, CancellationToken cancellationToken = default)
    {
        var totalItems = await query.CountAsync(cancellationToken);
        var items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
        return new(items, pageIndex, pageSize, totalItems);
    }
}