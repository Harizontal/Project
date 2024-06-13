using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Product_library
{
    public class Product
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("price")]
        public double Price { get; set; }

        [JsonPropertyName("weight")]
        public double Weight { get; set; }

        [JsonPropertyName("deliveryDate")]
        public DateTime DeliveryDate { get; set; }

        [JsonPropertyName("stock")]
        public int Stock { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonConstructor]
        public Product(string name, double price, double weight, DateTime deliveryDate, int stock, string description)
        {
            Name = name;
            Price = price;
            Weight = weight;
            DeliveryDate = deliveryDate;
            Stock = stock;
            Description = description;
        }

        public override string ToString()
        {
            return $"{Name}, {Price}, {Weight}, {DeliveryDate}, {Stock}, {Description}";
        }
    }

}