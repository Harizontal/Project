using System;
using System.Collections.Generic;
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
            List<Product> stock = storage.stock;

            Cart cart = new Cart();

            OrderCalculator calculator = new OrderCalculator();

            while (true)
            {
                Console.WriteLine("1. Добавить товар в корзину");
                Console.WriteLine("2. Увеличить количество товара в корзине");
                Console.WriteLine("3. Увеличить количество в число раз в корзине");
                Console.WriteLine("4. Уменьшить количество товара в корзине");
                Console.WriteLine("5. Вывести содержимое корзины");
                Console.WriteLine("6. Очистить корзину");
                Console.WriteLine("7. Выход");

                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Неверное значение. Попробуйте снова.");
                }

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Доступные товары:");
                        foreach (var products in stock)
                        {
                            Console.WriteLine($"{products.Name}, {products.Price}, {products.Weight}, {products.DeliveryDate}, {products.Stock}, {products.Description}");
                        }
                        Console.Write("Введите имя товара: ");
                        string name = Console.ReadLine();
                        Product product = stock.FirstOrDefault(p => p.Name == name);
                        if (product != null)
                        {
                            if (cart.items.Any(p => p.Name == name))
                                Console.WriteLine($"Товар {name} уже есть в корзине.");
                            else
                            {
                                Console.Write("Введите количество: ");
                                int quantity;
                                while (!int.TryParse(Console.ReadLine(), out quantity))
                                {
                                    Console.WriteLine("Неверное значение. Попробуйте снова.");
                                }
                                cart.AddItem(product, quantity);
                            }
                        }
                        else
                            Console.WriteLine("Товар не найден.");
                        break;
                    case 2:
                        Console.Write("Введите имя товара: ");
                        name = Console.ReadLine();
                        product = stock.FirstOrDefault(p => p.Name == name);
                        if (product != null)
                        {
                            PositionInCart position = cart.items.FirstOrDefault(p => p.Name == name);
                            if (position != null)
                            {
                                Console.Write("Введите количество для увеличение: ");
                                int quantity;
                                while (!int.TryParse(Console.ReadLine(), out quantity))
                                {
                                    Console.WriteLine("Неверное значение. Попробуйте снова.");
                                }
                                calculator.Add(position, quantity);
                            }
                            else
                                Console.WriteLine("Товар не найден в корзине.");
                        }
                        else
                            Console.WriteLine("Товар не найден.");
                        break;
                    case 3:
                        Console.Write("Введите имя товара: ");
                        name = Console.ReadLine();
                        product = stock.FirstOrDefault(p => p.Name == name);
                        if (product != null)
                        {
                            PositionInCart position = cart.items.FirstOrDefault(p => p.Name == name);
                            if (product != null)
                            {
                                Console.Write("Введите количество для увеличение: ");
                                int quantity;
                                while (!int.TryParse(Console.ReadLine(), out quantity))
                                {
                                    Console.WriteLine("Неверное значение. Попробуйте снова.");
                                }
                                calculator.Multiply(position, quantity);
                            }
                            else
                                Console.WriteLine("Товар не найден в корзине.");
                        }
                        else
                            Console.WriteLine("Товар не найден.");
                        break;
                    case 4:
                        Console.Write("Введите имя товара: ");
                        name = Console.ReadLine();
                        product = stock.FirstOrDefault(p => p.Name == name);
                        if (product != null)
                        {
                            PositionInCart position = cart.items.FirstOrDefault(p => p.Name == name);
                            if (position != null)
                            {
                                Console.Write("Введите количество для уменьшения: ");
                                int quantity;
                                while (!int.TryParse(Console.ReadLine(), out quantity))
                                {
                                    Console.WriteLine("Неверное значение. Попробуйте снова.");
                                }
                                calculator.Subtract(position, quantity);
                            }
                            else
                                Console.WriteLine("Товар не найден в корзине.");
                        }
                        else
                            Console.WriteLine("Товар не найден.");
                        break;
                    case 5:
                            Console.WriteLine(cart.GetOrderInformation(storage));
                        break;
                    case 6:
                        cart.items.Clear();
                        Console.WriteLine("Корзина очищена");
                        break;
                    case 7:
                        return;
                }
            }
        }
    }
}