namespace Cart.Models
{
    public class ShoppingCart
    {
        public class Item
        {
            public Item(int id, string name, decimal price)
            {
                ProductId = id;
                Name = name;
                Price = price;
            }
            public int ProductId { get; init; }
            public int Qty { get; set; } = 1;
            public string Name { get; init; }
            public decimal Price { get; init; }
        }
        private List<Item> lines = new ();

        public Guid Id { get; } = Guid.NewGuid ();
        public IEnumerable<Item> Lines => lines;
        public decimal TotalPrice => lines.Sum(l => l.Price * l.Qty);
        public Item GetItem(int index) => lines[index];
        public void AddItem(Item item) => lines.Add(item);
        public void RemoveItem(int index) => lines.RemoveAt(index);
    }
}
