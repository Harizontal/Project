using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product_library
{
    public class Product
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public double Weight { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }

    }

    public class FoodProduct : Product
    {
        public DateTime ExpirationDate { get; set; }

        public override string ToString()
        {
            return $"{Name}, {Price}, {Weight}, {ExpirationDate}";
        }
    }

    public class ElectronicsProduct : Product
    {
        public bool HasDryer { get; set; }

        public override string ToString()
        {
            return $"{Name}, {Price}, {Weight}, {HasDryer}";
        }
    }
    public class FurnitureProduct : Product
    {
        public string Material { get; set; }

        public override string ToString()
        {
            return $"{Name}, {Price}, {Weight}, {Material}";
        }
    }

}
