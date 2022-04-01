using System.ComponentModel.DataAnnotations.Schema;

namespace Order.Models
{
    public class OrderEntity
    {
        private List<OrderCmd> history = new();
        private List<OrderLine> lines = new();

        public int? Id { get; set; }
        public DateTime? Validated { get; set; } = null;
        public OrderStatus Status { get; set; } = OrderStatus.Draft;
        public decimal ShippingFees { get; set; } = 0m;
        [NotMapped] public bool Paid => Status >= OrderStatus.Shipped && Due == 0m;
        public decimal TotalPaid { get; set; } = 0;
        [NotMapped] public decimal Due => TotalPrice - TotalPaid;
        [NotMapped] public decimal TotalPrice => ShippingFees + lines.Sum(l => l.TotalPrice);
        public IEnumerable<OrderCmd> Actions
        {
            get => history;
            set
            {
                history.Clear();
                history.AddRange(value);
            }
        }
        public IEnumerable<OrderLine> Lines
        {
            get => lines;
            set
            {
                lines.Clear();
                lines.AddRange(value);
            }
        }

        public void AddAction(OrderCmd action)
        {
            action.Apply(this);
            history.Add(action);
        }

    }
}
