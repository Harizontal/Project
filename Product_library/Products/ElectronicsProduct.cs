using Product_library;
using System.Diagnostics;
using System.Xml.Linq;
using System;
using System.Text.Json.Serialization;

public class ElectronicsProduct : Product
{
    [JsonPropertyName("has_dryer")]
    public bool HasDryer { get; set; }

    public ElectronicsProduct(string name, double price, double weight, DateTime deliveryDate, int stock, string description, bool hasDryer)
        : base(name, price, weight, deliveryDate, stock, description)
    {
        HasDryer = hasDryer;
    }

    public override string ToString()
    {
        return $"{Name}, {Price}, {Weight}, {DeliveryDate}, {Stock}, {Description}, {HasDryer}";
    }
}   