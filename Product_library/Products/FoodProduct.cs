using System;
using System.Text.Json.Serialization;

namespace Product_library
    {
        public class FoodProduct : Product
        {

            [JsonPropertyName("expiration_date")]
            public DateTime ExpirationDate { get; set; }

            public FoodProduct(string name, double price, double weight, DateTime deliveryDate, int stock, string description, DateTime expirationDate)
                : base(name, price, weight, deliveryDate, stock, description)
            {
                ExpirationDate = expirationDate;
            }

            public override string ToString()
            {
                return $"{Name}, {Price}, {Weight}, {DeliveryDate}, {Stock}, {Description}, {ExpirationDate}";
            }
        }
    }