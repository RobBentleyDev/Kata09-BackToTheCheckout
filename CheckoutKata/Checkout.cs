using System.Collections.Generic;
using System.Linq;

namespace CheckoutKata
{
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