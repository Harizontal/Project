using System;
using System.Text.Json.Serialization;

namespace Product_library
{
    /// <summary>
    /// Наследуемый класс Product
    /// </summary>
    public class FoodProduct : Product
    {

        [JsonPropertyName("expiration_date")]
        public DateTime ExpirationDate { get; set; }


        public override string ToString()
        {
            return $"Name: {Name}, Price: {Price}, Weight: {Weight}, DeliveryDate: {DeliveryDate.ToShortDateString()}, " +
            $"Stcok: {Stock}, Description: {Description} ExpirationDate: {ExpirationDate.ToShortDateString()}";
        }
    }
}