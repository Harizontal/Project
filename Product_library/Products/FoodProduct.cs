    using System;

    namespace Product_library
    {
        public class FoodProduct : Product
        {
            public DateTime ExpirationDate { get; set; }

            public FoodProduct(string name, double price, double weight, DateTime deliveryDate, int stock, string description, DateTime expirationDate)
                : base(name, price, weight, deliveryDate, stock, description)
            {
                ExpirationDate = expirationDate;
            }

            public override string ToString()
            {
                return $"{Name}, {Price}, {Weight}, {ExpirationDate}";
            }
        }
    }