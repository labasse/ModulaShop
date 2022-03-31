using Cart.Models;

namespace Cart.Dtos
{
    public record CartDto(Guid Id, LineDto[] Lines, decimal TotalPrice)
    {
        public static CartDto FromCart(ShoppingCart cart)
        {
            // TODO : Créer un CartDto à partir d'un cart
            throw new NotImplementedException();
        }
    }
}
