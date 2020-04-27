using NUnit.Framework;
using FluentAssertions;
using System;
using System.Collections.Generic;

namespace CheckoutKata
{
    // https://github.com/davelobban/checkout-kata

    public class CheckoutTests
    {
        // [done] Total of a Single product
        // [done] Calculate the total price of all individual products
        // [done] Calculate the total price of all items, taking into account weekly offers
        // Refactor Checkout to use a PricingGroups class 

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
        public void GivenABasketOf2B15ProductsAnd3A99Products_WhenScanned_ThenTotalIsCombinedSpecialOfferPrice()
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

        [Test]
        public void GivenABasketOf4B15Products_WhenScanned_ThenTotalIsCombinedSpecialOfferPrice()
        {
            var checkout = Checkout();

            var basket = Basket();
            basket.Add(productB15());
            basket.Add(productB15());
            basket.Add(productB15());
            basket.Add(productB15());

            checkout.Scan(basket);

            checkout.Total().Should().Be(90);
        }

        [Test]
        public void GivenABasketOf9A99Products_WhenScanned_ThenTotalIsCombinedSpecialOfferPrice()
        {
            var checkout = Checkout();

            var basket = Basket();
            basket.Add(productA99());
            basket.Add(productA99());
            basket.Add(productA99());
            basket.Add(productA99());
            basket.Add(productA99());
            basket.Add(productA99());
            basket.Add(productA99());
            basket.Add(productA99());
            basket.Add(productA99());

            checkout.Scan(basket);

            checkout.Total().Should().Be(390);
        }

        [Test]
        public void GivenABasketOf5B15Products_WhenScanned_ThenTotalIsCombinedSpecialOfferPricePlusPriceOfASingleB15()
        {
            var checkout = Checkout();

            var basket = Basket();
            basket.Add(productB15());
            basket.Add(productB15());
            basket.Add(productB15());
            basket.Add(productB15());
            basket.Add(productB15());

            checkout.Scan(basket);

            checkout.Total().Should().Be(120);
        }

        [Test]
        public void GivenABasketOf5A99Products_WhenScanned_ThenTotalIsCombinedSpecialOfferPricePlusPriceOfTwoA99()
        {
            var checkout = Checkout();

            var basket = Basket();
            basket.Add(productA99());
            basket.Add(productA99());
            basket.Add(productA99());
            basket.Add(productA99());
            basket.Add(productA99());

            checkout.Scan(basket);

            checkout.Total().Should().Be(230);
        }

        [Test]
        public void GivenABasketOfNonOfferProducts_WhenScanned_ThenTotalIsCombinedTotalOfAllProducts()
        {
            var checkout = Checkout();

            // C = 60 * 3 = 180
            // T = 99 * 4 = 396
            // Expected = 576
            var basket = Basket();
            basket.Add(productC40());
            basket.Add(productT34());
            basket.Add(productT34());
            basket.Add(productC40());
            basket.Add(productT34());
            basket.Add(productC40());
            basket.Add(productT34());

            checkout.Scan(basket);

            checkout.Total().Should().Be(576);
        }

        [Test]
        public void GivenAMixedBasketProducts_WhenScanned_ThenTotalIsCombinedTotalOfAllProductsWithOffersApplied()
        {
            var checkout = Checkout();

            // C = 60 * 3 = 180
            // T = 99 * 4 = 396
            // A = 50 * 10 - 3 * 20 = 440
            // B = 30 * 5 - 2 * 15 = 120
            // Expected = 1136
            var basket = Basket();
            basket.Add(productC40());
            basket.Add(productA99());
            basket.Add(productB15());
            basket.Add(productT34());
            basket.Add(productA99());
            basket.Add(productC40());
            basket.Add(productA99());
            basket.Add(productC40());
            basket.Add(productT34());
            basket.Add(productA99());
            basket.Add(productB15());
            basket.Add(productA99());
            basket.Add(productT34());
            basket.Add(productA99());
            basket.Add(productA99());
            basket.Add(productB15());
            basket.Add(productT34());
            basket.Add(productB15());
            basket.Add(productA99());
            basket.Add(productA99());
            basket.Add(productB15());
            basket.Add(productA99());

            checkout.Scan(basket);

            checkout.Total().Should().Be(1136);
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
}