using System.ComponentModel.DataAnnotations;

namespace Catalog.Models
{
    public class ProductType
    {
        public int? Id { get; set; }

        [StringLength(50, MinimumLength = 2)]
        public string Label { get; set; } = null!;

        public override bool Equals(object? obj) =>
            obj != null 
            && obj is ProductType
            && Id == ((ProductType)obj).Id
            && Label == ((ProductType)obj).Label;

        public override int GetHashCode() =>
            (Id, Label).GetHashCode();
    }
}
