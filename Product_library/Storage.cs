using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

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
                stock = JsonSerializer.Deserialize<List<Product>>(json);
            }
        }

        public void SaveStock(string filePath)
        {
            string json = JsonSerializer.Serialize(stock);
            File.WriteAllText(filePath, json);
        }
    }
}
