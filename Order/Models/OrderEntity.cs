using System.ComponentModel.DataAnnotations.Schema;

namespace Order.Models
{
    public class OrderEntity
    {
        private List<OrderCmd> history = new();
        private List<OrderLine> lines = new();

        public OrderStatus Status { get; set; } = OrderStatus.Draft;
        public decimal ShippingFees { get; set; } = 0m;
        [NotMapped] public bool Paid => Status >= OrderStatus.Shipped && Due == 0m;
        public decimal TotalPaid { get; set; } = 0;
        [NotMapped] public decimal Due => TotalPrice - TotalPaid;
        [NotMapped] public decimal TotalPrice => ShippingFees + lines.Sum(l => l.TotalPrice);
        public IEnumerable<OrderCmd> Actions => history;
        public IEnumerable<OrderLine> Lines => lines;

        public void AddAction(OrderCmd action)
        {
            action.Apply(this);
            history.Add(action);
        }

    }
}
