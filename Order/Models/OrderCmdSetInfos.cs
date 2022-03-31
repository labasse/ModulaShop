namespace Order.Models
{
    public class OrderCmdSetInfos : OrderCmd
    {
        public decimal ShippingFees { get; set; }
        public string ShippingAddress { get; set; } = null!;
        public override void Apply(OrderEntity order)
        {
            if(order.Status!=OrderStatus.Draft)
            {
                throw new InvalidOperationException("Order is already validated");
            }
            order.Status = OrderStatus.Validated;
            order.ShippingFees = ShippingFees;
        }
    }
}
