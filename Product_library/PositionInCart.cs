using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Product_library
{
    public class PositionInCart
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public double Weight { get; set; }
        public int Quantity { get; set; }

        public PositionInCart(Product product)
        {
            Name = product.Name;
            Price = product.Price;
            Weight = product.Weight;
            Quantity = 1;
        }

        public double GetTotalPrice() => Price * Quantity;
        public double GetTotalWeight() => Weight * Quantity;

        public string GetItemDescription(Storage storage)
        {
            Product product = storage.stock.FirstOrDefault(p => p.Name == Name);
            if (product != null)
            {
                return product.Description;
            }
            return "";
        }

        public override string ToString()
        {
            return $"{Name},{GetTotalPrice()},{GetTotalWeight()},{Quantity} ";
        }
    }
}
