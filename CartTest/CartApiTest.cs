using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cart.Dtos;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CartTest
{
    [TestClass]
    public class CartApiTest
    {
        private async Task<CartDto> CreateCart(HttpClient client)
        {
            var response = await client.PostAsJsonAsync<CartCreateDto>("api/carts", new CartCreateDto());

            return (await response.Content.ReadFromJsonAsync<CartDto>())!;
        }

        [TestMethod]
        public async void GetCartsEmpty()
        {
            var client = new WebApplicationFactory<Program>().CreateClient();

            var carts = (await client.GetFromJsonAsync<CartDto[]>("api/carts"))!;

            Assert.AreEqual(0, carts.Length);
        }

        [TestMethod]
        public async void GetCarts()
        {
            var client = new WebApplicationFactory<Program>().CreateClient();
            var cart1 = await CreateCart(client); 
            var cart2 = await CreateCart(client); 
            
            var carts = (await client.GetFromJsonAsync<CartDto[]>("api/carts"))!;

            CollectionAssert.AreEquivalent(new CartDto[] { cart1, cart2 }, carts);
        }

        [TestMethod] public async void GetExistingCart() {}
        [TestMethod] public async void GetUnknownCart() {}
        [TestMethod] public async void CreateCart() {}
        [TestMethod] public async void DeleteExistingCart() {}
        [TestMethod] public async void DeleteUnknownCart() { }
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