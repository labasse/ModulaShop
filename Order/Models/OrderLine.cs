namespace Order.Models
{
    public class OrderLine
    {
        public int ProductId { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }

        public decimal TotalPrice => Price * Qty;
    }
}
