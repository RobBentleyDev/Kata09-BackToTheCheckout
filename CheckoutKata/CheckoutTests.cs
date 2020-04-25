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
            var product = new Product("A99", 50);

            var checkout = new Checkout();

            checkout.Scan(product);
            
            checkout.Total().Should().Be(50);
        }

        [Test]
        public void GivenTwoProducts_WhenScanned_ThenTotalIsPriceOfBothProducts()
        {
            var product1 = new Product("A99", 50);
            var product2 = new Product("B15", 30);

            var checkout = new Checkout();

            checkout.Scan(product1);
            checkout.Scan(product2);

            checkout.Total().Should().Be(80);
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
        private string sku;
        private int price;

        public Product(string sku, int price)
        {
            this.sku = sku;
            this.price = price;
        }

        public decimal Price => this.price;
    }
}