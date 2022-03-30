    namespace Cart.Dtos
{
    public record CartDto(Guid Id, LineDto[] Lines, decimal TotalPrice)
    {
    }
}
