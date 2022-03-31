using Cart.Models;

namespace Cart.Dtos
{
    public record LineDto(int Index, int ProductId, string Name, int Qty, decimal ProductPrice)
    {
        public static LineDto FromCartItem(int index, ShoppingCart.Item line) =>
            new LineDto(index, line.ProductId, line.Name, line.Qty, line.Price);
    }
}
