using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catalog.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using System.Linq;

namespace CatalogTest
{
    [TestClass]
    public class ProductTest
    {
        private readonly Brand M1 = new Brand() { Id=1, Name="M1" };
        private readonly ProductType T1 = new ProductType() { Id=1, Label="T1" };

        private readonly string _50chars_ = "!".PadRight(50, '!');
        private readonly string _51chars_ = "!".PadRight(51, '!');

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
            Brand = M1,
            Type = T1
        };

        public void AssertValidProductAfterPropertySet(
            Action<Product> act,
            string expectedName = "Aaa",
            string? expectedDescription = "...",
            decimal expectedPrice = 12.5m,
            string? expectedUrlImage = "img/foo.jpg"
        )
        {
            var test = NewUsualProduct();
            act(test);

            var (ok, results) = ValidateProduct(test);

            Assert.IsTrue(ok);
            Assert.AreEqual(expectedName, test.Name);
            Assert.AreEqual(expectedDescription, test.Description);
            Assert.AreEqual(expectedPrice, test.Price);
            Assert.AreEqual(expectedUrlImage, test.UrlImage);
            Assert.AreEqual(M1, test.Brand);
            Assert.AreEqual(T1, test.Type);
        }

        private void AssertNotValidAfterPropertySet(string propertyName, Action<Product> act)
        {
            var test = NewUsualProduct();

            act(test);
            var (ok, results) = ValidateProduct(test);

            Assert.IsFalse(ok);
            Assert.AreEqual(1, results.Count());
            Assert.AreEqual(propertyName, results.First().MemberNames.First());
        }


        [TestMethod] public void SetNormalProduct() => 
            AssertValidProductAfterPropertySet(
                p => { }
            );

        [TestMethod] public void SetNameMinimal() =>
            AssertValidProductAfterPropertySet(
                expectedName: "Aa",
                act: p => p.Name = "Aa"
            );
        
        [TestMethod] public void SetNameMaximal() =>
            AssertValidProductAfterPropertySet(
                expectedName: _50chars_,
                act: p => p.Name = _50chars_
            );

        [TestMethod] public void SetNullDescription() =>
            AssertValidProductAfterPropertySet(
                expectedDescription: null,
                act: p => p.Description = null
            );

        [TestMethod] public void SetEmptyDescription() =>
            AssertValidProductAfterPropertySet(
                expectedDescription: "",
                act: p => p.Description = ""
            );

        [TestMethod] public void SetNullUrlImage() =>
            AssertValidProductAfterPropertySet(
                expectedUrlImage: null,
                act: p => p.UrlImage = null
            );

        [TestMethod] public void SetMinPrice() =>
            AssertValidProductAfterPropertySet(
                expectedPrice: 0.0m,
                act: p => p.Price = 0.0m
            );

        [TestMethod] public void SetMaxPrice() =>
            AssertValidProductAfterPropertySet(
                expectedPrice: 1_000_000m,
                act: p => p.Price = 1_000_000m
            );

        
        [TestMethod] public void SetNameTooShort() => 
            AssertNotValidAfterPropertySet(
                nameof(Product.Name), 
                p=>p.Name = "A"
            );
            
        [TestMethod] public void SetNameTooLong() =>
            AssertNotValidAfterPropertySet(
                nameof(Product.Name),
                p => p.Name = _51chars_
            );

        [TestMethod] public void SetPriceNegative() =>
            AssertNotValidAfterPropertySet(
                nameof(Product.Price),
                p => p.Price = -1.2m
            );

        [TestMethod] public void SetPriceTooExpensive() =>
            AssertNotValidAfterPropertySet(
                nameof(Product.Price),
                p => p.Price = 1_000_000.01m
            );

        [TestMethod] public void SetUrlImageBadExtension() =>
            AssertNotValidAfterPropertySet(
                nameof(Product.UrlImage),
                p => p.UrlImage = "img/foo.pdf"
            );

        [TestMethod] public void SetNullBrand() =>
            AssertNotValidAfterPropertySet(
                nameof(Product.Brand),
                p => p.Brand = null!
            );

        [TestMethod] public void SetNullType() =>
            AssertNotValidAfterPropertySet(
                nameof(Product.Type),
                p => p.Type = null!
            );

        private void AssertGetVatPrice(decimal expected, decimal price, decimal rate)
        {
            var test = NewUsualProduct();

            test.Price = price;
            var actual = test.GetVatPrice(rate);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod] public void GetVatPriceUsual() =>
            AssertGetVatPrice(25m, price: 12.5m, rate: 100m);
        
        [TestMethod] public void GetVatPriceUpRoundedAfterPriceChange() =>
            AssertGetVatPrice(0.02m, price: 0.01m, rate: 50m);
        
        [TestMethod] public void GetVatPriceDownRoundedAfterPriceChange() =>
            AssertGetVatPrice(0.01m, price: 0.01m, rate: 40m);

        [TestMethod] public void GetVatPriceNulRate() =>
            AssertGetVatPrice(12.5m, price: 12.5m, rate: 0m);

        [TestMethod] public void GetVatPriceNegativeRate() 
        {
            var test = NewUsualProduct();
            
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                ()=> test.GetVatPrice(-20m)
            );
        }
    }
}