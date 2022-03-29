using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catalog.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace CatalogTest
{
    [TestClass]
    public class ProductTest
    {
        public (bool, IEnumerable<ValidationResult>) ValidateProduct(Product prod)
        {
            var context = new ValidationContext(prod);
            var results = new List<ValidationResult>();
            var ok = Validator.TryValidateObject(prod, context, results, true);

            return (ok, results);
        }

        public Product NewUsualProduct() => new Product()
        {
            Id = 123,
            Name = "Aaa",
            Description = "...",
            Price = 12.5m,
            UrlImage = "img/foo.jpg",
            Brand = new Brand() { Id = 1, Name = "M1" },
            Type = new ProductType() { Id = 1, Label = "T1" }
        };

        public void AssertValidProductAfterPropertySet(Action<Product> act)
        {
            var test = NewUsualProduct();
            act(test);

            var (ok, results) = ValidateProduct(test);

            Assert.IsTrue(ok);
        }

        [TestMethod] 
        public void SetNormalProduct() 
            => AssertValidProductAfterPropertySet(p => { });
        
        [TestMethod]
        public void SetNameMinimal() 
            => AssertValidProductAfterPropertySet(p => {
                p.Name = "Aa";
            });
        
        [TestMethod]
        public void SetNameMaximal() 
        {
            
        }
        [TestMethod] public void SetNullDescription() { }
        [TestMethod] public void SetEmptyDescription() { }
        [TestMethod] public void SetMinPrice() { }
        [TestMethod] public void SetMaxPrice() { }
        

        [TestMethod] public void SetNameTooShort() { }
        [TestMethod] public void SetNameTooLong() { }
        [TestMethod] public void SetPriceNegative() { }
        [TestMethod] public void SetPriceTooExpensive() { }
        [TestMethod] public void SetUrlImageBadExtension() { }
        [TestMethod] public void SetNullBrand() { }
        [TestMethod] public void SetNullType() { }

        [TestMethod] public void GetVatPriceUsual() { }
        [TestMethod] public void GetVatPriceAfterPriceChange() { }
        [TestMethod] public void GetVatPriceNulRate() { }
        [TestMethod] public void GetVatPriceNegativeRate() { }

    }
}