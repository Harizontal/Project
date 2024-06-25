using System;
using Calculator_library;

namespace Product_library
{
    public class OrderCalculator : Calculator
    {
        public void Add(PositionInCart posCart, int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Количество для сложения меньше нуля или равно нулю");
            posCart.Quantity += quantity;
        }

        public void Subtract(PositionInCart posCart, int quantity)
        {
            if (quantity > posCart.Quantity || quantity <= 0)
                throw new ArgumentException("Количество для вычитания больше текущего количества");
            posCart.Quantity -= quantity;
        }

        public void Multiply(PositionInCart posCart, int quantity)
        {
            if (quantity > int.MaxValue / posCart.Quantity || quantity <= 0)
            {
                throw new OverflowException("Переполнение при умножении или количество меньше нуля или равно нулю");
            }

            posCart.Quantity *= quantity;
        }


        public void Divide(PositionInCart posCart, int divisor)
        {
            if (divisor <= 0)
                throw new DivideByZeroException("Деление на ноль не допускается или количество меньше нуля");

            posCart.Quantity /= divisor;
        }
        public void RemoveItemsByType(string productType, Cart cart, Storage storage)
        {
            var productsToRemove = storage.stock.FindAll(p => p.GetType().Name == productType);

            foreach (var productToRemove in productsToRemove)
            {
                var positionInCart = cart.items.Find(p => p.Name == productToRemove.Name);
                if (positionInCart != null)
                {
                    cart.items.Remove(positionInCart);
                }
            }
        }


    }
}
