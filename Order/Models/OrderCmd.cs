namespace Order.Models
{
    public abstract class OrderCmd
    {
        public int IdOrder { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public abstract void Apply(OrderEntity order);
    }
}
