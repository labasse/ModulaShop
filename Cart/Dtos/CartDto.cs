using Cart.Models;

namespace Cart.Dtos
{
    public record CartDto(Guid Id, LineDto[] Lines, decimal TotalPrice)
    {
        public static CartDto FromCart(ShoppingCart cart) =>
            new CartDto(
                cart.Id, 
                cart.Lines.Select(
                    (line, index) => LineDto.FromCartItem(index, line)
                ).ToArray(),
                cart.TotalPrice
            );
        
    }
}
