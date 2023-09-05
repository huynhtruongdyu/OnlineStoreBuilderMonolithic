using OSBM.Admin.Domain.Common;
using OSBM.Admin.Domain.Enums;

namespace OSBM.Admin.Domain.Entities;

public class Product : BaseEntity
{
    public Product(string name, string? brief, string? description, string? thumbnailUrl)
    {
        Name = name;
        Brief = brief;
        Description = description;
        ThumbnailUrl = thumbnailUrl;
    }

    public string Name { get; set; } = string.Empty;
    public string? Brief { get; set; }
    public string? Description { get; set; }
    public string? ThumbnailUrl { get; set; }

    public EnumProductStatus Status { get; set; }
}