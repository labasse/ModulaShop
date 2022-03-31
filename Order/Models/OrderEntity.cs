namespace Order.Models
{
    public class OrderEntity
    {
        private List<OrderCmd> history = new();
        public string? ShippingAddress { get; set; } = null;
        public decimal ShippingFees { get; set; } = 0m;

        public bool Paid { get; set; } = false;

        public IEnumerable<OrderCmd> Actions => history;

        public void AddAction(OrderCmd action)
        {

        }

    }
}
