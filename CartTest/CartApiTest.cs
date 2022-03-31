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

        [TestMethod] public async Task GetCartsEmpty()
        {
            var client = CreateCartApiClient();

            var carts = (await client.GetFromJsonAsync<CartDto[]>("api/carts"))!;

            Assert.AreEqual(0, carts.Length);
        }

        [TestMethod] public async Task GetCarts()
        {
            var (client, cart1) = await CreateClientWithCart(); 
            var cart2 = await CreateCart(client); 
            
            var carts = (await client.GetFromJsonAsync<CartDto[]>("api/carts"))!;

            CollectionAssert.AreEquivalent(new CartDto[] { cart1, cart2 }, carts);
        }

        [TestMethod] public async Task GetExistingEmptyCart()
        {
            var (client, cart1) = await CreateClientWithCart();

            var actual = (await client.GetFromJsonAsync<CartDto>($"api/carts/{cart1.Id}"))!;

            Assert.AreEqual(cart1, actual);
        }
        [TestMethod] public async Task GetExistingFilledCart()
        {
            var (client, cart1) = await CreateClientWithCart();
            // TODO : Fill Cart

            var actual = (await client.GetFromJsonAsync<CartDto>($"api/carts/{cart1.Id}"))!;

            Assert.AreEqual(cart1, actual);
        }
        [TestMethod] public async Task GetUnknownCart()
        {
            var (client, cart1) = await CreateClientWithCart();

            var response = client.GetAsync($"api/carts/{UnknownCartId}");

            Assert.AreEqual(HttpStatusCode.NotFound, response.Result.StatusCode);
        }
        [TestMethod] public async Task CreateCart()
        {
            var client = CreateCartApiClient();

            var response = await client.PostAsJsonAsync<CartCreateDto>("api/carts", new CartCreateDto());
            var cart = (await response.Content.ReadFromJsonAsync<CartDto>())!;

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.AreEqual(0m, cart.TotalPrice);
            Assert.AreEqual(Array.Empty<LineDto>(), cart.Lines);
        }
        [TestMethod] public async Task DeleteExistingCart() {
            var (client, cart1) = await CreateClientWithCart();

            var response = (await client.DeleteAsync($"api/carts/{cart1.Id}"));

            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }
        [TestMethod] public async Task DeleteUnknownCart() {
            var (client, cart1) = await CreateClientWithCart();

            var response = (await client.DeleteAsync($"api/carts/{UnknownCartId}"));

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
        [TestMethod, TestCategory("Lines")] public async Task CreateFirstLine() {}
        [TestMethod, TestCategory("Lines")] public async Task CreateSecondLine() {}
        [TestMethod, TestCategory("Lines")] public async Task CreateLineProductAlreadyInCart() {}
        [TestMethod, TestCategory("Lines")] public async Task CreateLineInUnknownCart() {}
        [TestMethod, TestCategory("Lines")] public async Task CreateLineUnknownProduct() {}
        [TestMethod, TestCategory("Lines")] public async Task CreateLineQuantity0() {}
        [TestMethod, TestCategory("Lines")] public async Task CreateLineQuantityNegative() { }
        [TestMethod, TestCategory("Lines")] public async Task UpdateLine() {}
        [TestMethod, TestCategory("Lines")] public async Task UpdateLineProductChange() {}
        [TestMethod, TestCategory("Lines")] public async Task UpdateLineSameQuantity() {}
        [TestMethod, TestCategory("Lines")] public async Task UpdateLineQuantity0() {}
        [TestMethod, TestCategory("Lines")] public async Task UpdateLineQuantityNegative() { }
        [TestMethod, TestCategory("Lines")] public async Task DeleteLine() { }
        [TestMethod, TestCategory("Lines")] public async Task DeleteLineUnknown() {  }
    }
}