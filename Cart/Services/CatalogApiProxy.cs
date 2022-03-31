namespace Cart.Services
{
    public partial class CatalogApiProxy
    {
        public async Task<Product> GetProductAsync(int id) => await Products3Async(id);
    }
}
