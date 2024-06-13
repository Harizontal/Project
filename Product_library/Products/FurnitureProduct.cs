using System;

namespace Product_library
{
    public class FurnitureProduct : Product
    {
        public string Material { get; set; }

        public FurnitureProduct(string name, double price, double weight, DateTime deliveryDate, int stock, string description, string material)
            : base(name, price, weight, deliveryDate, stock, description)
        {
            Material = material;
        }

        public override string ToString()
        {
            return $"{Name}, {Price}, {Weight}, {Material}";
        }
    }
}