using System;
using System.Collections.Generic;
using Calculator_library;

namespace Product_library
{
    public class OrderCalculator : Calculator
    {
        private Dictionary<Product, int> orderItems;

        public OrderCalculator(ILogger logger = null, bool logOperations = true)
            : base(logger, logOperations)
        {
            orderItems = new Dictionary<Product, int>();
        }

        public void AddItem(Product product, int quantity)
        {
            if (!orderItems.ContainsKey(product))
                orderItems[product] = 0;

            orderItems[product] += quantity;
        }

        public void SubtractItem(Product product, int quantity)
        {
            if (!orderItems.ContainsKey(product))
                throw new InvalidOperationException($"Товар {product.Name} не найден в заказе");

            if (orderItems[product] < quantity)
                throw new InvalidOperationException($"Недостаточно товаров {product.Name} в заказе");

            orderItems[product] -= quantity;
        }

        public void MultiplyItem(Product product, int multiplier)
        {
            if (!orderItems.ContainsKey(product))
                orderItems[product] = 0;

            orderItems[product] *= multiplier;
        }

        public void DivideItem(Product product, int divisor)
        {
            if (!orderItems.ContainsKey(product))
                throw new InvalidOperationException($"Товар {product.Name} не найден в заказе");

            if (divisor == 0)
                throw new DivideByZeroException("Деление на ноль не допускается");

            orderItems[product] /= divisor;
        }

        public Dictionary<Product, int> GetOrderItems()
        {
            return orderItems;
        }
    }
}
