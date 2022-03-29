using Catalog.Models;

namespace Catalog.Dtos
{
    /// <summary>
    /// Data used to update the product.
    /// </summary>
    /// <param name="Name">Product name</param>
    /// <param name="Description">Product description</param>
    /// <param name="Price">Product sale price</param>
    /// <param name="UrlImage">relative url of the product picture</param>
    public record ProductUpdateDto(
        string Name, 
        string? Description, 
        decimal Price,
        string? UrlImage
    )
    {
        public void CopyTo(Product product)
        {
            product.Name = Name;
            product.Description = Description;
            product.Price = Price;
            product.UrlImage = UrlImage;
        }
    }
}
