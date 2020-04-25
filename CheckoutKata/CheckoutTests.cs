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
    }

    internal class Checkout
    {
        private Product scanned;

        public Checkout()
        {
        }

        internal void Scan(Product product)
        {
            scanned = product;
        }

        internal decimal Total()
        {
            return scanned.Price;
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