using System.ComponentModel.DataAnnotations;

namespace Catalog.Models
{
    /// <summary>
    /// TODO : Summary
    /// </summary>
    public class Brand
    {
        public int? Id { get; set; }

        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; } = null!;  
    }
}
