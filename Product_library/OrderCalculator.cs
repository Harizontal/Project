using System;
using Calculator_library;
using Product_library;

namespace Product_library
{
    /// <summary>
    /// Класс для выполнения операций с позициями в корзине заказа.
    /// </summary>
    public class OrderCalculator : Calculator
    {
        public OrderCalculator(ILogger logger = null, bool logOperations = true) : base(logger, logOperations)
        {
        }

        /// <summary>
        /// Добавляет указанное количество товара в корзину.
        /// </summary>
        /// <param name="posCart">Позиция товара в корзине.</param>
        /// <param name="quantity">Количество товара для добавления.</param>
        public void Add(PositionInCart posCart, int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Количество для добавления меньше или равно нулю");

            posCart.Quantity += quantity;
            LogOperation($"Добавление в корзину: Товар '{posCart.Name}', Количество добавлено: {quantity}");
        }

        /// <summary>
        /// Удаляет указанное количество товара из корзины.
        /// </summary>
        /// <param name="posCart">Позиция товара в корзине.</param>
        /// <param name="quantity">Количество товара для удаления.</param>
        public void Subtract(PositionInCart posCart, int quantity)
        {
            if (quantity > posCart.Quantity || quantity <= 0)
                throw new ArgumentException("Количество для вычитания больше текущего количества или меньше или равно нулю");

            posCart.Quantity -= quantity;
            LogOperation($"Вычитание из корзины: Товар '{posCart.Name}', Количество вычтено: {quantity}");
        }

        /// <summary>
        /// Умножает количество товара в корзине на указанное значение.
        /// </summary>
        /// <param name="posCart">Позиция товара в корзине.</param>
        /// <param name="quantity">Значение, на которое нужно умножить количество товара.</param>
        public void Multiply(PositionInCart posCart, int quantity)
        {
            if (quantity > int.MaxValue / posCart.Quantity || quantity <= 0)
                throw new OverflowException("Переполнение при умножении или количество меньше или равно нулю");

            posCart.Quantity *= quantity;
            LogOperation($"Умножение в корзине: Товар '{posCart.Name}', Количество умножено на: {quantity}");
        }

        /// <summary>
        /// Делит количество товара в корзине на указанное значение.
        /// </summary>
        /// <param name="posCart">Позиция товара в корзине.</param>
        /// <param name="divisor">Значение, на которое нужно разделить количество товара.</param>
        public void Divide(PositionInCart posCart, int divisor)
        {
            if (divisor <= 0)
                throw new DivideByZeroException("Деление на ноль не допускается или количество меньше или равно нулю");

            posCart.Quantity /= divisor;
            LogOperation($"Деление в корзине: Товар '{posCart.Name}', Количество поделено на: {divisor}");
        }

        /// <summary>
        /// Удаляет все позиции товаров данного типа из корзины.
        /// </summary>
        /// <param name="productType">Тип товара для удаления из корзины.</param>
        /// <param name="cart">Корзина заказа.</param>
        /// <param name="storage">Хранилище, содержащее информацию о товарах.</param>
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
                LogOperation($"Удаление из корзины товары данного типа: '{productType}'");

            }
        }
    }
}