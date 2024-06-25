using Product_library;
using System;
using System.Globalization;

namespace ProductLibraryConsoleApp
{
    /// <summary>
    /// Класс для генерации тестовых данных для заказов и товаров.
    /// </summary>
    public static class TestDataGenerator
    {
        /// <summary>
        /// Генерирует заказ с указанным количеством товаров.
        /// </summary>
        /// <param name="count">Количество товаров для добавления в заказ.</param>
        /// <returns>Сгенерированный заказ.</returns>
        public static Cart GenerateOrder(int count)
        {
            Cart cart = new Cart();
            for (int i = 0; i < count; i++)
            {
                cart.AddItem(GenerateProduct());
            }
            return cart;
        }

        /// <summary>
        /// Генерирует заказ с общей стоимостью не более указанной суммы.
        /// </summary>
        /// <param name="maxSum">Максимальная стоимость заказа.</param>
        /// <returns>Сгенерированный заказ.</returns>
        public static Cart GenerateOrderSum(double maxSum)
        {
            var cart = new Cart();
            while (cart.TotalPrice() <= maxSum)
            {
                cart.AddItem(GenerateProduct());
            }
            return cart;
        }

        /// <summary>
        /// Генерирует заказ с общей стоимостью в диапазоне от минимальной до максимальной суммы.
        /// </summary>
        /// <param name="minSum">Минимальная стоимость заказа.</param>
        /// <param name="maxSum">Максимальная стоимость заказа.</param>
        /// <returns>Сгенерированный заказ.</returns>
        public static Cart GenerateOrderSumMinMax(double minSum, double maxSum)
        {
            var cart = new Cart();
            while (cart.TotalPrice() < minSum || cart.TotalPrice() > maxSum)
            {
                cart.AddItem(GenerateProduct());
            }
            return cart;
        }

        /// <summary>
        /// Генерирует заказ с указанным максимальным количеством товаров.
        /// </summary>
        /// <param name="maxCount">Максимальное количество товаров для добавления в заказ.</param>
        /// <returns>Сгенерированный заказ.</returns>
        public static Cart GenerateOrderCount(int maxCount)
        {
            Cart cart = new Cart();

            for (int i = 0; i < maxCount; i++)
            {
                cart.AddItem(GenerateProduct());
            }

            return cart;
        }

        /// <summary>
        /// Генерирует случайный товар.
        /// </summary>
        /// <returns>Сгенерированный товар.</returns>
        public static Product GenerateProduct()
        {
            Random random = new Random();
            int productType = random.Next(1, 4);
            switch (productType)
            {
                case 1:
                    return GenerateFoodProduct(random);
                case 2:
                    return GenerateFurnitureProduct(random);
                case 3:
                    return GenerateElectronicsProduct(random);
                default:
                    throw new InvalidOperationException($"Invalid product type: {productType}");
            }
        }

        private static FoodProduct GenerateFoodProduct(Random random)
        {
            string name = $"Food Product {random.Next(1, 5)}";
            double price = Math.Round(random.NextDouble() * 100, 2);
            double weight = Math.Round(random.NextDouble() * 100, 3);
            DateTime deliveryDate = DateTime.Now.AddDays(random.Next(-30, 30));
            int stock = random.Next(1, 100);
            string description = $"Описание для {name}";
            DateTime expirationDate = DateTime.Now.AddDays(random.Next(0, 365));
            return new FoodProduct
            {
                Name = name,
                Price = price,
                Weight = weight,
                DeliveryDate = deliveryDate,
                ExpirationDate = expirationDate,
                Stock = stock,
                Description = description
            };
        }

        private static FurnitureProduct GenerateFurnitureProduct(Random random)
        {
            string name = $"Furniture Product {random.Next(1, 200)}";
            double price = Math.Round(random.NextDouble() * 1000, 2);
            double weight = Math.Round(random.NextDouble() * 1000, 3);
            DateTime deliveryDate = DateTime.Now.AddDays(random.Next(-30, 30));
            int stock = random.Next(1, 100);
            string description = $"Описание для {name}";
            string material = $"Material {random.Next(1, 10)}";
            return new FurnitureProduct
            {
                Name = name,
                Price = price,
                Weight = weight,
                DeliveryDate = deliveryDate,
                Description = description,
                Stock = stock,
                Material = material 
            };
        }

        private static ElectronicsProduct GenerateElectronicsProduct(Random random)
        {
            string name = $"Electronics Product {Guid.NewGuid()}";
            double price = Math.Round(random.NextDouble() * 1000, 2);
            double weight = Math.Round(random.NextDouble() * 100, 3);
            DateTime deliveryDate = DateTime.Now.AddDays(random.Next(-30, 30));
            int stock = random.Next(1, 100);
            string description = $"Описание для {name}";
            bool hasDryer = random.Next(0, 2) == 1;
            return new ElectronicsProduct
            {
                Name= name,
                Price = price,
                Weight = weight,
                DeliveryDate = deliveryDate,
                Description = description,
                Stock = stock,
                HasDryer = hasDryer
            };
        }

        /// <summary>
        /// Изменяет ифнормацию о товаре
        /// </summary>
        /// <param name="product">Ссылка на объект, представляющий товар, который будет изменен.</param>
        /// <param name="editedProduct">Объект, содержащий новые значения для товара.</param>
        /// <param name="readFromConsole"> флаг, указывающий, нужно ли считывать новые значения из консоли.</param>
        /// <returns>Сгенерированный заказ.</returns>
        public static void EditProduct<T>(ref T product, T editedProduct, bool readFromConsole = true) where T : Product
        {
            if (readFromConsole)
            {
                Console.WriteLine("Введите новое название товара:");
                string newName = Console.ReadLine();
                while (string.IsNullOrEmpty(newName))
                {
                    Console.WriteLine("Некорректное название. Попробуйте еще раз:");
                    newName = Console.ReadLine();
                }
                product.Name = newName;

                Console.WriteLine("Введите новое описание товара:");
                string newDescription = Console.ReadLine();
                while (string.IsNullOrEmpty(newDescription))
                {
                    Console.WriteLine("Некорректное описание. Попробуйте еще раз:");
                    newDescription = Console.ReadLine();
                }
                product.Description = newDescription;

                Console.WriteLine("Введите новую цену товара:");
                string newPriceString = Console.ReadLine();
                double newPrice;
                while (!double.TryParse(newPriceString, out newPrice))
                {
                    Console.WriteLine("Некорректная цена. Попробуйте еще раз:");
                    newPriceString = Console.ReadLine();
                }
                product.Price = newPrice;

                Console.WriteLine("Введите новый вес товара:");
                string newWeightString = Console.ReadLine();
                double newWeight;
                while (!double.TryParse(newWeightString, out newWeight))
                {
                    Console.WriteLine("Некорректный вес. Попробуйте еще раз:");
                    newWeightString = Console.ReadLine();
                }
                product.Weight = newWeight;

                Console.WriteLine("Введите новую дату доставки товара:");
                string newDeliveryDateString = Console.ReadLine();
                DateTime newDeliveryDate;
                while (!DateTime.TryParseExact(newDeliveryDateString, "dd.MM.yyyy", null, DateTimeStyles.None, out newDeliveryDate))
                {
                    Console.WriteLine("Некорректная дата. Попробуйте еще раз:");
                    newDeliveryDateString = Console.ReadLine();
                }
                product.DeliveryDate = newDeliveryDate;

                switch (product)
                {
                    case FurnitureProduct furnitureProduct:
                        Console.WriteLine("Введите новый материал мебели:");
                        string newMaterial = Console.ReadLine();
                        while (string.IsNullOrEmpty(newMaterial))
                        {
                            Console.WriteLine("Некорректный материал. Попробуйте еще раз:");
                            newMaterial = Console.ReadLine();
                        }
                        furnitureProduct.Material = newMaterial;
                        break;
                    case FoodProduct foodProduct:
                        Console.WriteLine("Введите новую дату истечения срока годности:");
                        string newExpirationDateString = Console.ReadLine();
                        DateTime newExpirationDate;
                        while (!DateTime.TryParseExact(newExpirationDateString, "dd.MM.yyyy", null, DateTimeStyles.None, out newExpirationDate))
                        {
                            Console.WriteLine("Некорректная дата. Попробуйте еще раз:");
                            newExpirationDateString = Console.ReadLine();
                        }
                        foodProduct.ExpirationDate = newExpirationDate;
                        break;
                    case ElectronicsProduct electronicsProduct:
                        Console.WriteLine("Введите новое значение для наличия сушилки (true/false):");
                        string newHasDryerString = Console.ReadLine();
                        bool newHasDryer;
                        while (!bool.TryParse(newHasDryerString, out newHasDryer))
                        {
                            Console.WriteLine("Некорректное значение. Попробуйте еще раз:");
                            newHasDryerString = Console.ReadLine();
                        }
                        electronicsProduct.HasDryer = newHasDryer;
                        break;
                    default:
                        Console.WriteLine("Тип продукта не поддерживается.");
                        break;
                }
            }
            product = editedProduct;
        }
    }
}
