using Product_library;
using System;
using System.Text.Json.Serialization;

public class ElectronicsProduct : Product
{
    [JsonPropertyName("has_dryer")]
    public bool HasDryer { get; set; }
    public override string ToString()
    {
        return $"Name: {Name}, Price: {Price}, Weight: {Weight}, DeliveryDate: {DeliveryDate.ToShortDateString()}, Stcok: {Stock}, Description: {Description}" +
            $"HasDryer: {HasDryer}";
    }
}   