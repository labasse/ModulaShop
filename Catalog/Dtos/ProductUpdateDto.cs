using Catalog.Models;

namespace Catalog.Dtos
{
    /// <summary>
    /// TODO : documentation
    /// </summary>
    /// <param name="Name"></param>
    /// <param name="Description"></param>
    /// <param name="Price"></param>
    /// <param name="UrlImage"></param>
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
