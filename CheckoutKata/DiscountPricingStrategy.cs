namespace CheckoutKata
{
    internal class DiscountPricingStrategy
    {
        public DiscountPricingStrategy(string sku, int qualifyingQuantity, int discountGiven)
        {
            Sku = sku;
            QualifyingQuantity = qualifyingQuantity;
            DiscountGiven = discountGiven;
        }

        public string Sku { get; }
        public int QualifyingQuantity { get; set; }
        public int DiscountGiven { get; set; }
    }
}