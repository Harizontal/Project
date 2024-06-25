using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;


namespace Product_library
{
    /// <summary>
    /// Класс представляющий корзину с товарами.
    /// Содержит список позиций в корзине и методы для работы с ними.
    /// </summary>
    public class Cart
    {
        public List<PositionInCart> items = new List<PositionInCart>();

        /// <summary>
        /// Добавляет товар в корзину.
        /// </summary>
        /// <param name="product">Товар, который нужно добавить в корзину.</param>
        public void AddItem(Product product)
        {
            items.Add(new PositionInCart(product));

        }

        /// <summary>
        /// Возвращает общую стоимость всех товаров в корзине.
        /// </summary>
        /// <returns>Общая стоимость всех товаров в корзине.</returns>
        public double TotalPrice() => items.Sum(item => item.GetTotalPrice());

        /// <summary>
        /// Возвращает общий вес всех товаров в корзине.
        /// </summary>
        /// <returns>Общий вес всех товаров в корзине.</returns>
        public double TotalWeight() => items.Sum(item => item.GetTotalWeight());

        /// <summary>
        /// Возвращает информацию о заказе в виде строки.
        /// </summary>
        /// <param name="storage">Хранилище, содержащее информацию о продуктах.</param>
        /// <returns>Информация о заказе в виде строки.</returns>
        public string GetOrderInformation(Storage storage)
        {
            var orderInfo = items.Select(item => $"{item.ToString()}{item.GetItemDescription(storage)}").ToList();

            return string.Join("\n", orderInfo);
        }

        /// <summary>
        /// Сортирует список товаров по алфавиту.
        /// </summary>
        /// <param name="products">Список товаров, который нужно отсортировать.</param>
        public static void AlphabeticalOrder(List<Product> products)
        {
            int n = products.Count;

            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (products[j].Name.CompareTo(products[j + 1].Name) > 0)
                    {
                        Product temp = products[j];
                        products[j] = products[j + 1];
                        products[j + 1] = temp;
                    }
                }
            }
        }

        /// <summary>
        /// Загружает информацию о заказе из файла.
        /// </summary>
        /// <param name="orderFilePath">Путь к файлу с информацией о заказе.</param>
        /// <returns>Информация о заказе в виде строки.</returns>
        public string LoadOrderFile(string orderFilePath)
        {
            while (!File.Exists(orderFilePath))
            {
                Console.WriteLine("Файл заказа не найден");
                orderFilePath = Console.ReadLine();
            }
            Dictionary<Product, int> productCounts = new Dictionary<Product, int>();

            string json = File.ReadAllText(orderFilePath);
            JArray jArray = JArray.Parse(json);


            foreach (var element in jArray)
            {
                string type = element["type"].Value<string>();
                int count = element["quantity"].Value<int>();
                switch (type)
                {
                    case "FurnitureProduct":
                        productCounts.Add(element.ToObject<FurnitureProduct>(), count);
                        break;
                    case "FoodProduct":
                        productCounts.Add(element.ToObject<FoodProduct>(), count);
                        break;
                    case "ElectronicsProduct":
                        productCounts.Add(element.ToObject<ElectronicsProduct>(), count);
                        break;
                    default:
                        throw new Exception("Не существет товар с таким типом");
                }
            }

            return string.Join("\n", productCounts.Select(kv => $"{kv.Key.Name}: {kv.Value}"));
        }
    }

}

