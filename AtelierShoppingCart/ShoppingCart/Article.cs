using System;

namespace ShoppingCart
{
    public class Article
    {
        private int quantity;

        public Article(string productName, int qty, decimal price)
        {
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(price, 0m, "Price cannot be negative or zero.");
            ProductName = productName;
            Quantity = qty;
            Price = price;
        }
        public string ProductName;
        public decimal Price;
        public decimal TotalPrice => Quantity * Price;
        public int Quantity
        {
            get => quantity;
            set
            {
                ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(value, 0, "Quantity cannot be negative or zero.");
                quantity = value;
            }
        }
        public override bool Equals(object obj)
            => obj is Article
            && ProductName == (obj as Article).ProductName
            && Price == (obj as Article).Price
            && Quantity == (obj as Article).Quantity;

        public override int GetHashCode()
            => $"{ProductName},{quantity},{Price}".GetHashCode();
    }
}
