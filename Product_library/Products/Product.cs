using System;
using System.Text.Json.Serialization;

namespace Product_library
{
    public abstract class Product
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        public double Price { get; set; }

        [JsonPropertyName("weight")]
        public double Weight { get; set; }

        [JsonPropertyName("deliveryDate")]
        public DateTime DeliveryDate { get; set; }

        [JsonPropertyName("stock")]
        public int Stock { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}, Price: {Price}, Weight: {Weight}, DeliveryDate: {DeliveryDate.Date}, Stcok: {Stock}, Description: {Description}";
        }
    }

}   