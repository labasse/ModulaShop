using Catalog.Models;

namespace Catalog.Dtos
{
    public record ProductCreateDto(
        string Name,
        string? Description,
        decimal Prix,
        string? UrlImage,
        int BrandId,
        int TypeId
        )
    {
        public Product ToProduct(Brand brand, ProductType type) => new Product() {
            Name = Name,
            Description = Description,
            Prix = Prix,
            UrlImage = UrlImage,
            Brand = brand,
            Type = type
        };
    }
}
