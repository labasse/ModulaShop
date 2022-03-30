using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cart.Dtos;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;

namespace CartTest
{
    [TestClass]
    public class CartApiTest
    {
        private readonly Guid UnknownCartId = Guid.Parse("218ED374-F2F0-4BB0-BADC-944C8C03B137");

        private HttpClient CreateCartApiClient() => new WebApplicationFactory<Program>().CreateClient();
        
        private async Task<CartDto> CreateCart(HttpClient client)
        {
            var response = await client.PostAsJsonAsync<CartCreateDto>("api/carts", new CartCreateDto());

            return (await response.Content.ReadFromJsonAsync<CartDto>())!;
        }

        private async Task<(HttpClient, CartDto)> CreateClientWithCart()
        {
            var client = CreateCartApiClient();
            
            return (client, await CreateCart(client));
        }

        [TestMethod] public async void GetCartsEmpty()
        {
            var client = CreateCartApiClient();

            var carts = (await client.GetFromJsonAsync<CartDto[]>("api/carts"))!;

            Assert.AreEqual(0, carts.Length);
        }

        [TestMethod] public async void GetCarts()
        {
            var (client, cart1) = await CreateClientWithCart(); 
            var cart2 = await CreateCart(client); 
            
            var carts = (await client.GetFromJsonAsync<CartDto[]>("api/carts"))!;

            CollectionAssert.AreEquivalent(new CartDto[] { cart1, cart2 }, carts);
        }

        [TestMethod] public async void GetExistingCart()
        {
            var (client, cart1) = await CreateClientWithCart();

            var actual = (await client.GetFromJsonAsync<CartDto>($"api/carts/{cart1.Id}"))!;

            Assert.AreEqual(cart1, actual);
        }
        [TestMethod] public async void GetUnknownCart()
        {
            var (client, cart1) = await CreateClientWithCart();

            var response = client.GetAsync($"api/carts/{UnknownCartId}");

            Assert.AreEqual(HttpStatusCode.NotFound, response.Result.StatusCode);
        }
        [TestMethod]
        public async void CreateCart()
        {
            var client = CreateCartApiClient();

            var response = await client.PostAsJsonAsync<CartCreateDto>("api/carts", new CartCreateDto());
            var cart = (await response.Content.ReadFromJsonAsync<CartDto>())!;

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.AreEqual(0m, cart.TotalPrice);
            Assert.AreEqual(Array.Empty<LineDto>(), cart.Lines);
        }
        [TestMethod] public async void DeleteExistingCart() {
            var (client, cart1) = await CreateClientWithCart();

            var response = (await client.DeleteAsync($"api/carts/{cart1.Id}"));

            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }
        [TestMethod] public async void DeleteUnknownCart() {
            var (client, cart1) = await CreateClientWithCart();

            var response = (await client.DeleteAsync($"api/carts/{UnknownCartId}"));

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
        [TestMethod] public async void CreateFirstLine() {}
        [TestMethod] public async void CreateSecondLine() {}
        [TestMethod] public async void CreateLineProductAlreadyInCart() {}
        [TestMethod] public async void CreateLineInUnknownCart() {}
        [TestMethod] public async void CreateLineUnknownProduct() {}
        [TestMethod] public async void CreateLineQuantity0() {}
        [TestMethod] public async void CreateLineQuantityNegative() { }
        [TestMethod] public async void UpdateLine() {}
        [TestMethod] public async void UpdateLineProductChange() {}
        [TestMethod] public async void UpdateLineSameQuantity() {}
        [TestMethod] public async void UpdateLineQuantity0() {}
        [TestMethod] public async void UpdateLineQuantityNegative() { }
        [TestMethod] public async void DeleteLine() { }
        [TestMethod] public async void DeleteLineUnknown() {  }
    }
}