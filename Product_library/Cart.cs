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
    }

}

