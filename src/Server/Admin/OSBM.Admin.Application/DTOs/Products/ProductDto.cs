namespace OSBM.Admin.Application.DTOs.Products;

public class ProductDto
{
    public ProductDto(long id, string name, string? brief, string? description, string? thumbnailUrl)
    {
        Id = id;
        Name = name;
        Brief = brief;
        Description = description;
        ThumbnailUrl = thumbnailUrl;
    }

    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Brief { get; set; }
    public string? Description { get; set; }
    public string? ThumbnailUrl { get; set; }
}