namespace CheckoutKata
{
    internal class Product
    {
        public Product(string sku, int price)
        {
            Sku = sku;
            Price = price;
        }

        public decimal Price { get; }
        public string Sku { get; }
    }
}