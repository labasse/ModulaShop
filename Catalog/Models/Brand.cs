using System.ComponentModel.DataAnnotations;

namespace Catalog.Models
{
    /// <summary>
    /// Product brand
    /// </summary>
    public class Brand
    {
        public int? Id { get; set; }

        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; } = null!;

        public override bool Equals(object? obj) =>
            obj != null
            && obj is Brand
            && Id == ((Brand)obj).Id
            && Name == ((Brand)obj).Name;

        public override int GetHashCode() => 
            (Id, Name).GetHashCode();
    }
}
