using System;
using System.Linq;

namespace Product_library
{
    /// <summary>
    /// Класс представляющий позицию в корзине.
    /// Содержит информацию о продукте, его количестве и общей стоимости.
    /// </summary>
    public class PositionInCart
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public double Weight { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int Quantity { get; set; }

        /// <summary>
        /// Создает новую позицию в корзине на основе переданного продукта.
        /// </summary>
        /// <param name="product">Продукт, на основе которого создается позиция в корзине.</param>
        public PositionInCart(Product product)
        {
            Name = product.Name;
            Price = product.Price;
            Weight = product.Weight;
            DeliveryDate = product.DeliveryDate;
            Quantity = 1;
        }

        /// <summary>
        /// Возвращает общую стоимость позиции в корзине.
        /// </summary>
        /// <returns>Общая стоимость позиции в корзине.</returns>
        public double GetTotalPrice() => Price * Quantity;

        /// <summary>
        /// Возвращает общий вес позиции в корзине.
        /// </summary>
        /// <returns>Общий вес позиции в корзине.</returns>
        public double GetTotalWeight() => Weight * Quantity;

        /// <summary>
        /// Возвращает описание продукта для данной позиции в корзине.
        /// </summary>
        /// <param name="storage">Хранилище, содержащее информацию о продуктах.</param>
        /// <returns>Описание продукта для данной позиции в корзине.</returns>
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
