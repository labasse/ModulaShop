namespace Order.Models
{
    public class OrderCmdPay : OrderCmd
    {
        public decimal Amount { get; set; }
        public string Transaction { get; set; } = null!;
        public override void Apply(OrderEntity order)
        {
            if (order.Status != OrderStatus.Validated)
            {
                throw new InvalidOperationException("Order is draft or already paid");
            }
            if(Amount <= 0)
            {
                throw new InvalidOperationException("Payment amount must be strictly positive");
            }
            if(Amount > order.Due)
            {
                throw new InvalidOperationException("Payment amount must be less than due amount");
            }
            order.TotalPaid += Amount;
            if(order.Due == 0)
            {
                order.Status = OrderStatus.Paid;
            }
        }
    }
}
