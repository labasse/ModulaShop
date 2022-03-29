using System.ComponentModel.DataAnnotations;

namespace Catalog.Models
{
    /// <summary>
    /// Product information.
    /// </summary>
    public class Product
    {
        public int? Id { get; set; } = null;
        
        [Required(ErrorMessage ="Product name is required")]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; } = null!;
        public string? Description { get; set; } = null;
        
        [Required, Range(0.0, 1_000_000.00)]
        public decimal Price { get; set; }

        [Url]
        public string? UrlImage { get; set; } = null;

        [Required]
        public Brand Brand { get; set; } = null!;
        [Required]
        public ProductType Type { get; set; } = null!;
    }
}
