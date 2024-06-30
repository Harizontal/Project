using System;
using System.Globalization;
using System.Linq;
using Product_library;

namespace ProductLibraryConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Storage storage = new Storage();
            storage.LoadStock(@"C:\Users\Alexey\Desktop\Product_library\Products.json");

            Cart cart = new Cart();

            OrderCalculator calculator = new OrderCalculator();

            while (true)
            {
                Console.WriteLine("1. Добавить товар в корзину");
                Console.WriteLine("2. Ссылка на свой заказ(json)");
                Console.WriteLine("3. Увеличить количество товара в корзине");
                Console.WriteLine("4. Увеличить количество в число раз в корзине");
                Console.WriteLine("5. Уменьшить количество товара в корзине");
                Console.WriteLine("6. Убрать товары определенного типа");
                Console.WriteLine("7. Редактировать товар");
                Console.WriteLine("8. Вывод заказ:");
                Console.WriteLine("9. Вывести содержимое корзины");
                Console.WriteLine("10. Очистить корзину");
                Console.WriteLine("11. Выход");

                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Неверное значение. Попробуйте снова.");
                }

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Доступные товары:");
                        foreach (var products in storage.stock)
                        {
                            Console.WriteLine(products.ToString());
                        }
                        Console.Write("Введите имя товара: ");
                        string name = Console.ReadLine();
                        Product product = storage.stock.FirstOrDefault(p => p.Name == name);
                        if (product is null)
                        {
                            Console.WriteLine("Товар не найден.");
                            continue;
                        }
                        if (cart.items.Any(p => p.Name == name))
                            Console.WriteLine($"Товар {name} уже есть в корзине.");
                        else
                            cart.AddItem(product);
                        break;
                    case 2:
                        Console.WriteLine("Укажите путь на файл:");
                        string path = Console.ReadLine();
                        Console.WriteLine(cart.LoadOrderFile($@"{path}"));
                        break;
                    case 3:
                        Console.Write("Введите имя товара: ");
                        string nameToUpdate = Console.ReadLine();
                        PositionInCart positionToUpdate = cart.items.FirstOrDefault(p => p.Name == nameToUpdate);
                        if (positionToUpdate is null)
                        {
                            Console.WriteLine("Товар не найден в корзине.");
                            continue;
                        }
                        Console.Write("Введите количество для увеличение: ");
                        int quantityToUpdate;
                        while (!int.TryParse(Console.ReadLine(), out quantityToUpdate))
                        {
                            Console.WriteLine("Неверное значение. Попробуйте снова.");
                        }
                        calculator.Add(positionToUpdate, quantityToUpdate);
                        break;
                    case 4:
                        Console.Write("Введите имя товара: ");
                        string nameToMultiply = Console.ReadLine();
                        PositionInCart positionToMultiply = cart.items.FirstOrDefault(p => p.Name == nameToMultiply);
                        if (positionToMultiply is null)
                        {
                            Console.WriteLine("Товар не найден в корзине.");
                            continue;
                        }
                        Console.Write("Введите количество для увеличение: ");
                        int quantityToMultiply;
                        while (!int.TryParse(Console.ReadLine(), out quantityToMultiply))
                        {
                            Console.WriteLine("Неверное значение. Попробуйте снова.");
                        }
                        calculator.Multiply(positionToMultiply, quantityToMultiply);
                        break;
                    case 5:
                        Console.Write("Введите имя товара: ");
                        string nameToRemove = Console.ReadLine();
                        PositionInCart positionToRemove = cart.items.FirstOrDefault(p => p.Name == nameToRemove);
                        if (positionToRemove is null)
                        {
                            Console.WriteLine("Товар не найден в корзине.");
                            continue;
                        }
                        Console.Write("Введите количество для уменьшения: ");
                        int quantityToRemove;
                        while (!int.TryParse(Console.ReadLine(), out quantityToRemove))
                        {
                            Console.WriteLine("Неверное значение. Попробуйте снова.");
                        }
                        calculator.Subtract(positionToRemove, quantityToRemove);
                        break;

                    case 6:
                        Console.WriteLine("Выберите тип товара для удаления:");
                        Console.WriteLine("1. ElectronicsProduct");
                        Console.WriteLine("2. FoodProduct");
                        Console.WriteLine("3. FurnitureProduct");

                        int choices;
                        while (!int.TryParse(Console.ReadLine(), out choices))
                        {
                            Console.WriteLine("Неверное значение. Попробуйте снова.");
                        }

                        switch (choices)
                        {
                            case 1:
                                calculator.RemoveItemsByType("ElectronicsProduct", cart, storage);
                                break;
                            case 2:
                                calculator.RemoveItemsByType("FoodProduct", cart, storage);
                                break;
                            case 3:
                                calculator.RemoveItemsByType("FurnitureProduct", cart, storage);
                                break;
                            default:
                                Console.WriteLine("Неверный выбор");
                                break;
                        }

                        break;
                    case 7:
                        Console.Write("Введите имя товара для редактирования: ");
                        string productName = Console.ReadLine();
                        var productEdit = storage.stock.FirstOrDefault(p => p.Name == productName);
                        Product produtcClone = productEdit;
                        {
                            if (productEdit != null)
                            {
                                var index = storage.stock.IndexOf(productEdit);
                                Console.WriteLine("Введите новое название товара:");
                                string newName = Console.ReadLine();
                                while (string.IsNullOrEmpty(newName))
                                {
                                    Console.WriteLine("Некорректное название. Попробуйте еще раз:");
                                    newName = Console.ReadLine();
                                }
                                produtcClone.Name = newName;

                                Console.WriteLine("Введите новое описание товара:");
                                string newDescription = Console.ReadLine();
                                while (string.IsNullOrEmpty(newDescription))
                                {
                                    Console.WriteLine("Некорректное описание. Попробуйте еще раз:");
                                    newDescription = Console.ReadLine();
                                }
                                produtcClone.Description = newDescription;

                                Console.WriteLine("Введите новую цену товара:");
                                string newPriceString = Console.ReadLine();
                                double newPrice;
                                while (!double.TryParse(newPriceString, out newPrice))
                                {
                                    Console.WriteLine("Некорректная цена. Попробуйте еще раз:");
                                    newPriceString = Console.ReadLine();
                                }
                                produtcClone.Price = newPrice;

                                Console.WriteLine("Введите новый вес товара:");
                                string newWeightString = Console.ReadLine();
                                double newWeight;
                                while (!double.TryParse(newWeightString, out newWeight))
                                {
                                    Console.WriteLine("Некорректный вес. Попробуйте еще раз:");
                                    newWeightString = Console.ReadLine();
                                }
                                produtcClone.Weight = newWeight;

                                Console.WriteLine("Введите новую дату доставки товара:");
                                string newDeliveryDateString = Console.ReadLine();
                                DateTime newDeliveryDate;
                                while (!DateTime.TryParseExact(newDeliveryDateString, "dd.MM.yyyy", null, DateTimeStyles.None, out newDeliveryDate))
                                {
                                    Console.WriteLine("Некорректная дата. Попробуйте еще раз:");
                                    newDeliveryDateString = Console.ReadLine();
                                }
                                produtcClone.DeliveryDate = newDeliveryDate;

                                switch (produtcClone)
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
                                TestDataGenerator.EditProduct(productEdit, produtcClone);
                                storage.stock[index] = productEdit;
                                Console.WriteLine("Товар успешно отредактирован.");
                            }
                            else
                                Console.WriteLine("Товар не найден.");
                            break;
                        }

                    case 8:
                        while (true)
                        {
                            Console.WriteLine("Выберите операцию:");
                            Console.WriteLine("1. Дороже/дешевле заданной цены");
                            Console.WriteLine("2. Товары определенного типа");
                            Console.WriteLine("3. Отсортировать по весу");
                            Console.WriteLine("4. Уникальные названия");
                            Console.WriteLine("5. Отправленные до указанной даты");
                            Console.WriteLine("6. Выход");

                            int choose;
                            while (!int.TryParse(Console.ReadLine(), out choose))
                            {
                                Console.WriteLine("Неверное значение. Попробуйте снова.");
                            }

                            switch (choose)
                            {
                                case 1:
                                    LinqQuery.FilterByPrice(cart);
                                    break;
                                case 2:
                                    LinqQuery.FilterByType(cart, storage);
                                    break;
                                case 3:
                                    LinqQuery.SortByWeight(cart);
                                    break;
                                case 4:
                                    LinqQuery.UniqueNames(cart);
                                    break;
                                case 5:
                                    LinqQuery.FilterByDate(cart);
                                    break;
                                case 6:
                                    return;
                            }
                        }
                    case 9:
                        Console.WriteLine(cart.GetOrderInformation(storage));
                        break;
                    case 10:
                        cart.items.Clear();
                        Console.WriteLine("Корзина очищена");
                        break;
                    case 11:
                        storage.SaveStock(@"C:\Users\Alexey\Desktop\Product_library\Products.json");
                        return;
                }
            }
        }
    }
}
