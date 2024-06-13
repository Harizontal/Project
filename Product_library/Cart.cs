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

        public void AddItem(Product product, int quantity)
        {
            items.Add(new PositionInCart(product, quantity));
        }

        public double TotalPrice() => items.Sum(item => item.GetTotalPrice());
        public double TotalWeight() => items.Sum(item => item.GetTotalWeight());

        public string GetOrderInformation(Storage product)
        {

            var info = new List<string>();
            foreach (var item in items)
            {
                info.Add(item.GetItemDescription(product.stock));
            }
            return string.Join("\n", info);
        }
        public string AlphabeticalOrder()
        {
            var itemDescriptions = items.OrderBy(item => item.Name).Select(item => item.ToString()).ToList();

            return string.Join("\n", itemDescriptions);
        }
    }

}

