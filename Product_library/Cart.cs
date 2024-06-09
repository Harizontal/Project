using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Product_library
{
    public class Cart
    {
        public List<PositionInCart> items = new List<PositionInCart>();

        public void AddItem(Product item, int quantity)
        {
            items.Add(new PositionInCart { Product = item, Quantity = quantity });
        }

        public double TotalPrice() => items.Sum(item => item.GetTotalPrice());
        public double TotalWeight() => items.Sum(item => item.GetTotalWeight());

        public string GetOrderInformation()
        {
            var info = new List<string>();
            foreach (var item in items)
            {
                info.Add(item.GetItemDescription());
            }
            return string.Join("\n", info);
        }

        public void CalculateOptimalOrder(Dictionary<string, List<Product>> productSets)
        {

            foreach (var kvp in productSets)
            {
                var set = kvp.Value;
                var cheapestItem = set.OrderBy(item => item.Price).First();
                AddItem(cheapestItem, 1);
            }
        }
        public string GetItemsInAlphabeticalOrder()
        {
            var itemsByName = new List<Product>();
            foreach (var position in items)
            {
                itemsByName.Add(position.Product);
            }

            itemsByName.Sort((a, b) => a.Name.CompareTo(b.Name));

            var info = new List<string>();
            foreach (var item in itemsByName)
            {
                info.Add(item.ToString());
            }
            return string.Join("\n", info);
        }
    }

}

