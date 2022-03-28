using Catalog.Models;

namespace Catalog.Dtos
{
    public record ProductUpdateDto(
        string Name, 
        string? Description, 
        decimal Prix,
        string? UrlImage
    )
    {
        public void CopyTo(Product product)
        {
            product.Name = Name;
            product.Description = Description;
            product.Prix = Prix;
            product.UrlImage = UrlImage;
        }
    }
}
