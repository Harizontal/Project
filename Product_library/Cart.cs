using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Text.Json;
using System.Text.Json.Serialization;
using Product_library;


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

            var info = new List<string>();
            foreach (var item in items)
            {
                info.Add(item.ToString() + item.GetItemDescription(storage));
            }
            return string.Join("\n", info);
        }
        public string AlphabeticalOrder()
        {
            List<Product> sortedProducts = new List<Product>();
            sortedProducts.Sort((a, b) => a.Name.CompareTo(b.Name));

            StringBuilder sb = new StringBuilder();
            foreach (Product product in sortedProducts)
            {
                sb.AppendLine(product.ToString());
            }

            return sb.ToString();
        }
        public string ProcessOrderFile(string orderFilePath)
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

