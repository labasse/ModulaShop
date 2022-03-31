namespace Order.Models
{
    public class OrderCmdShip : OrderCmd
    {
        public string TrackingNumber { get; set; } = null!;
        public override void Apply(OrderEntity order)
        {
            if(order.Status == OrderStatus.Paid)
            {
                order.Status = OrderStatus.Shipped;
            }
        }
    }
}
