namespace Order.Models
{
    public class OrderEntity
    {
        private List<OrderCmd> history = new();


        public OrderStatus Status { get; set; } = OrderStatus.Draft;

        public decimal ShippingFees { get; set; } = 0m;

        public bool Paid => Status >= OrderStatus.Shipped;

        public IEnumerable<OrderCmd> Actions => history;

        public void AddAction(OrderCmd action)
        {
            action.Apply(this);
            history.Add(action);
        }

    }
}
