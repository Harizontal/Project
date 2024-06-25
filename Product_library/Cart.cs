using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;


namespace Product_library
{
    public class Cart
    {
        public List<PositionInCart> items = new List<PositionInCart>();

        public void AddItem(Product product)
        {
            items.Add(new PositionInCart(product));

        }
        public double TotalPrice() => items.Sum(item => item.GetTotalPrice());
        public double TotalWeight() => items.Sum(item => item.GetTotalWeight());

        public string GetOrderInformation(Storage storage)
        {
            var orderInfo = items.Select(item => $"{item.ToString()}{item.GetItemDescription(storage)}").ToList();

            return string.Join("\n", orderInfo);
        }
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

