using System;
using System.Collections.Generic;
using System.Linq;
using Calculator_library;

namespace Product_library
{
    public class OrderCalculator: Calculator
    {
        public PositionInCart Add(PositionInCart posCart, int quantity)
        {
            posCart.Quantity += quantity;
            return posCart;
        }

        public PositionInCart Subtract(PositionInCart posCart, int quantity)
        {
            if (quantity > posCart.Quantity)
                throw new ArgumentException("Количество для вычитания больше текущего количества");
            posCart.Quantity -= quantity;
            return posCart;
        }

        public PositionInCart Multiply(PositionInCart posCart, int quantity)
        {
            posCart.Quantity *= quantity;
            return posCart;
        }

        public PositionInCart Divide(PositionInCart posCart, int divisor)
        {
            if (divisor == 0)
                throw new DivideByZeroException("Деление на ноль не допускается");

            posCart.Quantity /= divisor;    
            return posCart;
        }
        public void RemoveItemsByType(List<PositionInCart> cart, Type productType)
        {
            foreach (var item in cart)
            {
                if(item.GetType() == productType)
                    cart.Remove(item);
            }
        }
    }
}
