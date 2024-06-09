using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Product_library
{
    public class PositionInCart
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public double GetTotalPrice()
        {
            return Product.Price * Quantity;
        }

        public double GetTotalWeight()
        {
            return Product.Weight * Quantity;
        }

        public string GetItemDescription()
        {
            return $"{Product.ToString()}, Количество: {Quantity}";
        }
    }
}
