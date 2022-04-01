namespace Order.Models
{
    public class OrderLine
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; } = null!;
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice => Price * Qty;
    }
}
