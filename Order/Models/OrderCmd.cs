namespace Order.Models
{
    public abstract class OrderCmd
    {
        public abstract void Apply(OrderEntity order);
    }
}
