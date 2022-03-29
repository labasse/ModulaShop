using Catalog.Models;
using System.ComponentModel.DataAnnotations;

namespace Catalog.Dtos
{
    /// <summary>
    /// Describes the product to be created.
    /// </summary>
    /// <param name="Name">Name of the product</param>
    /// <param name="Description">Product description</param>
    /// <param name="Price">Sale price</param>
    /// <param name="UrlImage">Relative Url for the product picture</param>
    /// <param name="BrandId">Id of the product brand</param>
    /// <param name="TypeId">Id of the product type</param>
    public record ProductCreateDto(
        string Name,
        string? Description,
        decimal Price,
        string? UrlImage,
        int BrandId,
        int TypeId
        )
    {
        public Product ToProduct(Brand brand, ProductType type) => new Product() {
            Name = Name,
            Description = Description,
            Price = Price,
            UrlImage = UrlImage,
            Brand = brand,
            Type = type
        };
    }
}
