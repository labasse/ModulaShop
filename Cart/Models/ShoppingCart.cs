namespace Cart.Models
{
    public class ShoppingCart
    {
        public class Item
        {
            public Item(int id, string name, decimal price)
            {
                IdProduct = id;
                Name = name;
                Price = price;
            }
            public int IdProduct { get; init; }
            public int Qty { get; set; } = 1;
            public string Name { get; init; }
            public decimal Price { get; init; }
        }
        private List<Item> _items = new ();

        public Guid Id { get; } = Guid.NewGuid ();
        public IEnumerable<Item> Lines => throw new NotImplementedException(); // TODO
        public decimal TotalPrice => throw new NotImplementedException(); // TODO

        public Item GetItem(int index)
        {
            // TODO
            throw new NotImplementedException ();
        }
        public void AddItem()
        {
            // TODO
            throw new NotImplementedException();
        }
        public void RemoveItem(int index)
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}
