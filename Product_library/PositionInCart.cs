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

        public PositionInCart(Product product, int quantity)
        {
            Product clonedProduct = (Product)product.Clone();
            Name = clonedProduct.Name;
            Price = clonedProduct.Price;
            Weight = clonedProduct.Weight;
            Quantity = quantity;
        }

        public double GetTotalPrice() => Price * Quantity;
        public double GetTotalWeight() => Weight * Quantity;

        public string GetItemDescription(List<Product> product)
        {
            foreach (var item in product)
            {
                return $"{item.Name}, {GetTotalPrice()}, {GetTotalWeight()}, {item.Description}";
            }
            return "";
        }
    }
}
