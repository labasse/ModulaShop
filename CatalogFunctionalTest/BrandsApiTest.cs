using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using System;
using Catalog;
using Catalog.Models;
using Catalog.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace CatalogFunctionalTest
{
    /// <summary>
    /// Inspired from https://docs.microsoft.com/fr-fr/aspnet/core/test/integration-tests?view=aspnetcore-6.0
    /// </summary>
    [TestClass]
    public class BrandsApiTest
    {
        private HttpClient LaunchServerAndGetClient()
        {
            var databaseName = Guid.NewGuid().ToString();
            var appFactory = new WebApplicationFactory<Program>().WithWebHostBuilder(
                builder => {
                    builder.UseEnvironment("Test");
                    builder.ConfigureServices(services =>
                        services.AddDbContext<CatalogContext>(options =>
                            options.UseInMemoryDatabase(databaseName)
                        )
                    );
                }
            );
            var context = appFactory.Server.Services.GetService<CatalogContext>()!;

            context.Brand.Add(new Brand() { Name = "Azure" });
            context.Brand.Add(new Brand() { Name = ".Net" });
            context.SaveChanges();

            return appFactory.CreateClient();
        }

        [TestMethod]
        public async Task GetBrands()
        {
            var client = LaunchServerAndGetClient();

            var brands = await client.GetFromJsonAsync<Brand[]>("api/brands");
            
            CollectionAssert.AreEqual(new Brand[] {
                new Brand() { Id=1, Name="Azure" },
                new Brand() { Id=2, Name=".Net" }
            }, brands);
        }

        [TestMethod]
        public async Task CreateNewBrandUsual()
        {
            var client = LaunchServerAndGetClient();
            
            var response = await client.PostAsJsonAsync<Brand>("api/brands", new Brand() { Name="foo" });
            var newBrand = (await response.Content.ReadFromJsonAsync<Brand>())!;

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.AreEqual(3, newBrand.Id);
        }

        // TODO: CreateNewBrandNoName
        // TODO: CreateNewBrandEmptyName
        // TODO: CreateNewBrandNameTooLong
        // TODO: UpdateBrandUsual
        // TODO: ...
    }
}