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
        public static Cart GenerateOrder()
        {
            Random rnd = new Random();
            Cart cart = new Cart();
            for (int i = 0; i < rnd.Next(0, 11); i++)
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
        /// <returns>Сгенерированный заказ.</returns>
        public static void EditProduct<T>(T product, T editedProduct) where T : Product
        {
            editedProduct.Name = product.Name;
            editedProduct.Description = product.Description;
            editedProduct.Price = product.Price;
            editedProduct.Weight = product.Weight;
            editedProduct.DeliveryDate = product.DeliveryDate;

            switch (product)
            {
                case FurnitureProduct furnitureProduct:
                    editedProduct = (T)(object)new FurnitureProduct
                    {
                        Material = furnitureProduct.Material
                    };
                    break;
                case FoodProduct foodProduct:
                    editedProduct = (T)(object)new FoodProduct
                    {
                        Stock = foodProduct.Stock
                    };
                    break;
                case ElectronicsProduct electronicsProduct:
                    editedProduct = (T)(object)new ElectronicsProduct
                    {
                        HasDryer = electronicsProduct.HasDryer
                    };
                    break;
            }
        }
    }
}
