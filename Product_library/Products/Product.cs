using System;
using System.Text.Json.Serialization;

namespace Product_library
{
    /// <summary>
    /// Абстрактный класс представляющий продукт.
    /// Содержит свойства и методы для описания товара.
    /// </summary>
    public abstract class Product
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

        /// <summary>
        /// Возвращает строковое представление продукта с основными характеристиками.
        /// </summary>
        public override string ToString()
        {
            return $"Name: {Name}, Price: {Price}, Weight: {Weight}, DeliveryDate: {DeliveryDate.Date}, Stcok: {Stock}, Description: {Description}";
        }
    }

}   