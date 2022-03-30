namespace Cart.Dtos
{
    public record LineDto(int ProductId, string Name, int Qty, decimal ProductPrice)
    { }
}
