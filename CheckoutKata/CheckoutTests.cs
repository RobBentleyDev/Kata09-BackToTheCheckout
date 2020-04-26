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
            var checkout = Checkout();

            checkout.Scan(productA99());

            checkout.Total().Should().Be(50);
        }

        [Test]
        public void GivenTwoProducts_WhenScanned_ThenTotalIsSumOfBothProducts()
        {
            var checkout = Checkout();

            checkout.Scan(productA99());
            checkout.Scan(productB15());

            checkout.Total().Should().Be(80);
        }

        [Test]
        public void Given3Products_WhenScanned_ThenTotalIsSumOf3Products()
        {
            var checkout = Checkout();

            checkout.Scan(productA99());
            checkout.Scan(productB15());
            checkout.Scan(productC40());

            checkout.Total().Should().Be(140);
        }

        [Test]
        public void GivenABasketOf1Product_WhenScanned_ThenTotalIsPriceOfProduct()
        {
            var checkout = Checkout();

            var basket = Basket();
            basket.Add(productA99());

            checkout.Scan(basket);

            checkout.Total().Should().Be(50);
        }

        [Test]
        public void GivenABasketOf2Products_WhenScanned_ThenTotalIsSumOfBothProducts()
        {
            var checkout = Checkout();

            var basket = Basket();
            basket.Add(productA99());
            basket.Add(productT34());

            checkout.Scan(basket);

            checkout.Total().Should().Be(149);
        }

        [Test]
        public void GivenABasketOf3A99Products_WhenScanned_ThenTotalIsSpecialOfferPriceFor3()
        {
            var checkout = Checkout();

            var basket = Basket();
            basket.Add(productA99());
            basket.Add(productA99());
            basket.Add(productA99());

            checkout.Scan(basket);

            checkout.Total().Should().Be(130);
        }

        [Test]
        public void GivenABasketOf2B15Products_WhenScanned_ThenTotalIsSpecialOfferPriceFor2()
        {
            var checkout = Checkout();

            var basket = Basket();
            basket.Add(productB15());
            basket.Add(productB15());

            checkout.Scan(basket);

            checkout.Total().Should().Be(45);
        }

        [Test]
        public void GivenABasketOf3A99Products_WhenScannedWithPricingStrategy_ThenTotalIsSpecialOfferPriceFor3()
        {
            var checkout = Checkout();

            var basket = Basket();
            basket.Add(productA99());
            basket.Add(productA99());
            basket.Add(productA99());

            checkout.Scan(basket);

            checkout.Total().Should().Be(130);
        }

        [Test]
        public void GivenABasketOf2B15Products_WhenScannedWithPricingStrategy_ThenTotalIsSpecialOfferPriceFor2()
        {
            var checkout = Checkout();

            var basket = Basket();
            basket.Add(productB15());
            basket.Add(productB15());

            checkout.Scan(basket);

            checkout.Total().Should().Be(45);
        }

        [Test]
        public void GivenABasketOf2B15ProductsAnd3A99Products_WhenScannedWithMultiplePricingStrategies_ThenTotalIsCombinedSpecialOfferPrice()
        {
            var checkout = Checkout();

            var basket = Basket();
            basket.Add(productB15());
            basket.Add(productB15());
            basket.Add(productA99());
            basket.Add(productA99());
            basket.Add(productA99());

            checkout.Scan(basket);

            checkout.Total().Should().Be(175);
        }


        private Basket Basket()
        {
            return new Basket();
        }

        private Checkout Checkout()
        {
            return new Checkout(DiscountPricingStrategies());
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

        private Product productT34()
        {
            return new Product("T34", 99);
        }

        private List<DiscountPricingStrategy> DiscountPricingStrategies()
        {
            return new List<DiscountPricingStrategy>
            {
                DiscountPricingStrategyForProductA99(),
                DiscountPricingStrategyForProductB15()
            };
        }

        private DiscountPricingStrategy DiscountPricingStrategyForProductA99()
        {
            return new DiscountPricingStrategy("A99", 3, 20);
        }

        private DiscountPricingStrategy DiscountPricingStrategyForProductB15()
        {
            return new DiscountPricingStrategy("B15", 2, 15);
        }
    }

    internal class Checkout
    {
        private readonly List<DiscountPricingStrategy> discountPricingStrategies;
        private List<Product> scanned;

        public Checkout(List<DiscountPricingStrategy> discountPricingStrategies)
        {
            scanned = new List<Product>();
            this.discountPricingStrategies = discountPricingStrategies;
        }

        internal void Scan(Product product)
        {
            scanned.Add(product);
        }

        internal void Scan(Basket basket)
        {
            while(basket.HasItems)
            {
                Scan(basket.RemoveNext());
            }
        }

        internal decimal Total()
        {
            var total = scanned.Sum(product => product.Price) - TotalDiscount();

            return total;
        }

        private int TotalDiscount()
        {
            var totalDiscount = 0;

            foreach (var pricingStrategy in discountPricingStrategies)
            {
                totalDiscount = totalDiscount + DiscountForStrategy(pricingStrategy);
            }

            return totalDiscount;
        }

        private int DiscountForStrategy(DiscountPricingStrategy pricingStrategy)
        {
            var discount = 0;

            var productsForStrategy = scanned
                .Where(product => product.Sku == pricingStrategy.Sku);

            if (productsForStrategy.Count() == pricingStrategy.QualifyingQuantity)
            {
                discount = pricingStrategy.DiscountGiven;
            }

            return discount;
        }
    }
}