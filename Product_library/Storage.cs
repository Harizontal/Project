using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Linq;
using Product_library;

namespace Product_library
{
    public class Storage
    {
        public List<Product> stock = new List<Product>();
        public void LoadStock(string filePath)
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                JArray jArray = JArray.Parse(json);

                foreach (var element in jArray)
                {
                    string type = element["type"].Value<string>();
                    switch (type)
                    {
                        case "FurnitureProduct":
                            stock.Add(element.ToObject<FurnitureProduct>());
                            break;
                        case "FoodProduct":
                            stock.Add(element.ToObject<FoodProduct>());
                            break;
                        case "ElectronicsProduct":
                            stock.Add(element.ToObject<ElectronicsProduct>());
                            break;
                        default:
                            throw new Exception("Не существет товар с таким типом");
                    }
                }
            }
        }

        public void SaveStock(string filePath)
        {
            string json = JsonSerializer.Serialize(stock);
            File.WriteAllText(filePath, json);
        }
    }
}