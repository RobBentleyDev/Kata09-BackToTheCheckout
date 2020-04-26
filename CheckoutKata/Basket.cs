using System.Collections.Generic;

namespace CheckoutKata
{
    internal class Basket
    {
        private Queue<Product> products;
        
        public Basket()
        {
            products = new Queue<Product>();
        }

        public bool HasItems => products.Count > 0;

        internal void Add(Product product)
        {
            products.Enqueue(product);
        }

        internal Product RemoveNext()
        {
            return products.Dequeue();
        }
    }
}