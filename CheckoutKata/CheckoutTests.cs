using NUnit.Framework;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckoutKata
{
    // https://github.com/davelobban/checkout-kata

    public class CheckoutTests
    {
        [SetUp]
        public void Setup()
        {
        }

        // Total of a Single product
        // Calculate the total price of all individual products
        // Calculate the total price of all items, taking into account weekly offers

        [Test]
        public void GivenSingleProduct_WhenScanned_ThenTotalIsPriceOfProduct()
        {
            var checkout = new Checkout();

            checkout.Scan(productA99());

            checkout.Total().Should().Be(50);
        }

        [Test]
        public void GivenTwoProducts_WhenScanned_ThenTotalIsSumOfBothProducts()
        {
            var checkout = new Checkout();

            checkout.Scan(productA99());
            checkout.Scan(productB15());

            checkout.Total().Should().Be(80);
        }

        [Test]
        public void Given3Products_WhenScanned_ThenTotalIsSumOf3Products()
        {
            var checkout = new Checkout();

            checkout.Scan(productA99());
            checkout.Scan(productB15());
            checkout.Scan(productC40());

            checkout.Total().Should().Be(140);
        }

        private Product productA99()
        {
            return new Product("A99", 50);
        }

        private Product productB15()
        {
            return new Product("B15", 30);
        }

        private Product productC40()
        {
            return new Product("C40", 60);
        }
    }

    internal class Checkout
    {
        private List<Product> scanned;

        public Checkout()
        {
            scanned = new List<Product>();
        }

        internal void Scan(Product product)
        {
            scanned.Add(product);
        }

        internal decimal Total()
        {
            var total = scanned.Sum(product => product.Price);

            return total;
        }
    }

    internal class Product
    {
        private readonly string sku;
        private readonly int price;

        public Product(string sku, int price)
        {
            this.sku = sku;
            this.price = price;
        }

        public decimal Price => price;
    }
}