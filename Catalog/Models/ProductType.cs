using System.ComponentModel.DataAnnotations;

namespace Catalog.Models
{
    public class ProductType
    {
        public int? Id { get; set; }

        [StringLength(50, MinimumLength = 2)]
        public string Label { get; set; } = null!;
    }
}
