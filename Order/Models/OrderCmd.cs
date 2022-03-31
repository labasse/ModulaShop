namespace Order.Models
{
    public abstract class OrderCmd
    {
        public DateTime Created { get; set; } = DateTime.Now;
        public abstract void Apply(OrderEntity order);
    }
}
