using System;
using System.Collections.Generic;
using System.Linq;
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
            List<Product> productsToRemove = GetProductsByType(productType, storage);

            foreach (var product in productsToRemove)
            {
                var positionInCart = cart.items.FirstOrDefault(p => p.Name == product.Name);
                if (positionInCart != null)
                {
                    cart.items.Remove(positionInCart);
                }
            }
        }

        private List<Product> GetProductsByType(string productType, Storage storage)
        {
            List<Product> products = new List<Product>();

            switch (productType)
            {
                case "ElectronicsProduct":
                    foreach (var product in storage.stock.OfType<ElectronicsProduct>())
                    {
                        products.Add(product);
                    }
                    break;
                case "FoodProduct":
                    foreach (var product in storage.stock.OfType<FoodProduct>())
                    {
                        products.Add(product);
                    }
                    break;
                case "FurnitureProduct":
                    foreach (var product in storage.stock.OfType<FurnitureProduct>())
                    {
                        products.Add(product);
                    }
                    break;
                default:
                    throw new ArgumentException("Неверный тип продукта");
            }

            return products;
        }
    }
}
