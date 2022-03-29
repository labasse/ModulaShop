using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catalog;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using System.Text.Json;
using Catalog.Models;

namespace CatalogFunctionalTest
{
    [TestClass]
    public class BrandsApiTest
    {
        [TestMethod]
        public async Task GetBrands()
        {
            var appFactory = new WebApplicationFactory<Program>();
            var client = appFactory.CreateClient();

            var response = await client.GetAsync("api/brands");
            var brands = JsonSerializer.Deserialize<Brand[]>(
                await response.Content.ReadAsStreamAsync()
            );

            response.EnsureSuccessStatusCode();
            //CollectionAssert.AreEqual(new Brand[] {
            //    new Brand() { Id=1, Name="Azure" },
            //    new Brand() { Id=2, Name=".Net" },
            //    new Brand() { Id=3, Name="Visual Studio" },
            //    new Brand() { Id=6, Name="SQL Server" },
            //    new Brand() { Id=7, Name="Other" }
            //}, brands);
        }
    }
}