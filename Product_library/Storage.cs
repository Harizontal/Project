using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Product_library;

namespace Product_library
{
    /// <summary>
    /// Класс для хранения товаров.
    /// </summary>
    public class Storage
    {
        public List<Product> stock = new List<Product>();

        /// <summary>
        /// Загружает данные о товарах из указанного JSON-файла.
        /// </summary>
        /// <param name="filePath">Путь к JSON-файлу с данными о товарах.</param>
        public void LoadStock(string filePath)
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                JArray jArray = JArray.Parse(json);

                foreach (var element in jArray)
                {
                    string type = element["type"].Value<string>();
                    Product product = null;

                    switch (type)
                    {
                        case "FurnitureProduct":
                            product = element.ToObject<FurnitureProduct>();
                            break;
                        case "FoodProduct":
                            product = element.ToObject<FoodProduct>();
                            break;
                        case "ElectronicsProduct":
                            product = element.ToObject<ElectronicsProduct>();
                            break;
                        default:
                            throw new Exception($"Не существует товара с таким типом: {type}");
                    }

                    stock.Add(product);
                }
            }
        }

        /// <summary>
        /// Сохраняет данные о товарах в указанный JSON-файл.
        /// </summary>
        /// <param name="filePath">Путь к JSON-файлу, в который будут сохранены данные о товарах.</param>
        public void SaveStock(string filePath)
        {
            JArray jArray = new JArray();

            foreach (var product in stock)
            {
                JObject jObject = JObject.FromObject(product);
                jObject.Add("type", product.GetType().Name);
                jArray.Add(jObject);
            }
            
            string json = jArray.ToString();
            File.WriteAllText(filePath, json);
        }
    }
}