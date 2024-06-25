using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product_library
{
    /// <summary>
    /// Класс, содержащий методы для фильтрации и сортировки товаров в корзине.
    /// </summary>
    public class LinqQuery
    {
        /// <summary>
        /// Фильтрует товары в корзине по заданной цене.
        /// </summary>
        /// <param name="cart">Корзина, содержащая товары.</param>
        public static void FilterByPrice(Cart cart)
        {
            Console.Write("Введите цену: ");
            double price;
            while (!double.TryParse(Console.ReadLine(), out price))
            {
                Console.WriteLine("Неверное значение. Попробуйте снова.");
            }

            var expensiveProducts = cart.items.Where(p => p.Price > price);
            var cheapProducts = cart.items.Where(p => p.Price < price);

            Console.WriteLine("Дороже заданной цены:");
            foreach (var producte in expensiveProducts)
            {
                Console.WriteLine(producte);
            }

            Console.WriteLine("Дешевле заданной цены:");
            foreach (var producte in cheapProducts)
            {
                Console.WriteLine(producte);
            }
        }

        /// <summary>
        /// Фильтрует товары в корзине по заданному типу.
        /// </summary>
        /// <param name="cart">Корзина, содержащая товары.</param>
        /// <param name="storage">Хранилище, содержащее информацию о товарах.</param>
        public static void FilterByType(Cart cart, Storage storage)
        {
            Console.WriteLine("Выберите тип товара:");
            Console.WriteLine("1. ElectronicsProduct");
            Console.WriteLine("2. FoodProduct");
            Console.WriteLine("3. FurnitureProduct");

            int typeChoice;
            while (!int.TryParse(Console.ReadLine(), out typeChoice))
            {
                Console.WriteLine("Неверное значение. Попробуйте снова.");
            }

            switch (typeChoice)
            {
                case 1:
                    FilterByTypeHelper(cart, storage, "ElectronicsProduct");
                    break;
                case 2:
                    FilterByTypeHelper(cart, storage, "FoodProduct");
                    break;
                case 3:
                    FilterByTypeHelper(cart, storage, "FurnitureProduct");
                    break;
                default:
                    Console.WriteLine("Неверный выбор");
                    break;
            }
        }

        private static void FilterByTypeHelper(Cart cart, Storage storage, string typeName)
        {
            var productsOfType = storage.stock.FindAll(p => p.GetType().Name == typeName);

            foreach (var product in productsOfType)
            {
                PositionInCart posCart = cart.items.FirstOrDefault(p => p.Name == product.Name);
                Console.WriteLine(posCart.ToString() + posCart.GetItemDescription(storage));
            }
        }

        /// <summary>
        /// Сортирует товары в корзине по весу.
        /// </summary>
        /// <param name="cart">Корзина, содержащая товары.</param>
        public static void SortByWeight(Cart cart)
        {
            var sortedByWeight = cart.items.OrderBy(p => p.Weight);
            Console.WriteLine("Отсортированные по весу:");
            foreach (var product in sortedByWeight)
            {
                Console.WriteLine(product);
            }
        }

        /// <summary>
        /// Выводит уникальные названия товаров в корзине.
        /// </summary>
        /// <param name="cart">Корзина, содержащая товары.</param>
        public static void UniqueNames(Cart cart)
        {
            var uniqueNames = cart.items.Select(p => p.Name).Distinct();
            Console.WriteLine("Уникальные названия:");
            foreach (var name in uniqueNames)
            {
                Console.WriteLine(name);
            }
        }

        /// <summary>
        /// Фильтрует товары в корзине по заданной дате доставки.
        /// </summary>
        /// <param name="cart">Корзина, содержащая товары.</param>
        public static void FilterByDate(Cart cart)
        {
            Console.Write("Введите дату: ");
            DateTime date;
            while (!DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", null, DateTimeStyles.None, out date))
            {
                Console.WriteLine("Неверный формат даты. Попробуйте снова.");
            }

            var productsBeforeDate = cart.items.Where(p => p.DeliveryDate < date);
            Console.WriteLine("Отправленные до указанной даты:");
            foreach (var product in productsBeforeDate)
            {
                Console.WriteLine(product);
            }
        }
    }
}
