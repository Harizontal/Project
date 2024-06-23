using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using Product_library;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

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
                        Console.WriteLine(cart.ProcessOrderFile($@"{path}"));
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
                                calculator.RemoveItemsByType("ElectronicsProduct",cart,storage);
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
                        var productToEdit = storage.stock.FirstOrDefault(p => p.Name == productName);
                        if (productToEdit != null)
                        {
                            var editedProduct = new Product("New Name", 12, 12, new DateTime(2023, 1, 1), 10, "dddd");
                            var index = storage.stock.IndexOf(productToEdit);
                            TestDataGenerator.EditProduct(ref productToEdit, editedProduct, false);
                            storage.stock[index] = productToEdit;
                            Console.WriteLine("Товар успешно отредактирован.");
                        }
                        else
                        {
                            Console.WriteLine("Товар не найден.");
                        }
                        break;

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
                                    break;
                                case 2:
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
                                            var electronicsProducts = storage.stock.OfType<ElectronicsProduct>();
                                            foreach (var producte in electronicsProducts)
                                            {
                                                PositionInCart posCart = cart.items.FirstOrDefault(p => p.Name == producte.Name);
                                                Console.WriteLine(posCart.ToString() + posCart.GetItemDescription(storage));
                                            }
                                            break;
                                        case 2:
                                            var foodProducts = storage.stock.OfType<FoodProduct>();
                                            foreach (var producte in foodProducts)
                                            {
                                                PositionInCart posCart = cart.items.FirstOrDefault(p => p.Name == producte.Name);
                                                Console.WriteLine(posCart.ToString() + posCart.GetItemDescription(storage));
                                            }
                                            break;
                                        case 3:
                                            var furnitureProducts = storage.stock.OfType<FurnitureProduct>();
                                            foreach (var producte in furnitureProducts)
                                            {
                                                PositionInCart posCart = cart.items.FirstOrDefault(p => p.Name == producte.Name);
                                                Console.WriteLine(posCart.ToString() + posCart.GetItemDescription(storage));
                                            }
                                            break;
                                        default:
                                            Console.WriteLine("Неверный выбор");
                                            break;
                                    }
                                    break;
                                case 3:
                                    var sortedByWeight = cart.items.OrderBy(p => p.Weight);
                                    Console.WriteLine("Отсортированные по весу:");
                                    foreach (var producte in sortedByWeight)
                                    {
                                        Console.WriteLine(producte);
                                    }
                                    break;
                                case 4:
                                    var uniqueNames = cart.items.Select(p => p.Name).Distinct();
                                    Console.WriteLine("Уникальные названия:");
                                    foreach (var Name in uniqueNames)
                                    {
                                        Console.WriteLine(Name);
                                    }
                                    break;
                                case 5:
                                    Console.Write("Введите дату: ");
                                    DateTime date;
                                    while (!DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", null, DateTimeStyles.None, out date))
                                    {
                                        Console.WriteLine("Неверный формат даты. Попробуйте снова.");
                                    }
                                    var productsBeforeDate = cart.items.Where(p => p.DeliveryDate < date);
                                    Console.WriteLine("Отправленные до указанной даты:");
                                    foreach (var producte in productsBeforeDate)
                                    {
                                        Console.WriteLine(producte);
                                    }
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
                        return;
                }
            }

        }
    }
}
