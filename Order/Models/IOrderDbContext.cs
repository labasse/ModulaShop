namespace Order.Models
{
    public interface IOrderDbContext
    {

        IEnumerable<OrderEntity> Orders { get; }

        Task<OrderEntity?> GetOrderByIdAsync(int id);
    }
}
