using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductLibraryConsoleApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Product_library.Tests
{
    [TestClass]
    public class ProductTests
    {
        [TestMethod]
        public void EditProduct_WithEmptyName()
        {
            var product = new Product("Old Name", 100.0, 10.0, new DateTime(2022, 12, 31), 10, "Old Description");
            var editedProduct = new Product("", 200.0, 20.0, new DateTime(2023, 1, 1), 20, "New Description");

            Assert.ThrowsException<ArgumentException>(() => TestDataGenerator.EditProduct(ref product, editedProduct, false));
        }

        [TestMethod]
        public void EditProduct_WithVeryLargeName()
        {
            var product = new Product("Old Name", 100.0, 10.0, new DateTime(2022, 12, 31), 10, "Old Description");
            var editedProduct = new Product(new string('A', 256), 200.0, 20.0, new DateTime(2023, 1, 1), 20, "New Description");

            Assert.ThrowsException<ArgumentException>(() => TestDataGenerator.EditProduct(ref product, editedProduct, false));
        }

        [TestMethod]
        public void EditProduct_WithNegativePrice()
        {
            var product = new Product("Old Name", 100.0, 10.0, new DateTime(2022, 12, 31), 10, "Old Description");
            var editedProduct = new Product("New Name", -200.0, 20.0, new DateTime(2023, 1, 1), 20, "New Description");

            Assert.ThrowsException<ArgumentException>(() => TestDataGenerator.EditProduct(ref product, editedProduct, false));
        }

        [TestMethod]
        public void EditProduct_WithZeroPrice()
        {
            var product = new Product("Old Name", 100.0, 10.0, new DateTime(2022, 12, 31), 10, "Old Description");
            var editedProduct = new Product("New Name", 0.0, 20.0, new DateTime(2023, 1, 1), 20, "New Description");

            Assert.ThrowsException<ArgumentException>(() => TestDataGenerator.EditProduct(ref product, editedProduct, false));
        }

        [TestMethod]
        public void EditProduct_WithVeryLargePrice()
        {
            var product = new Product("Old Name", 100.0, 10.0, new DateTime(2022, 12, 31), 10, "Old Description");
            var editedProduct = new Product("New Name", double.MaxValue, 20.0, new DateTime(2023, 1, 1), 20, "New Description");

            Assert.ThrowsException<ArgumentException>(() => TestDataGenerator.EditProduct(ref product, editedProduct, false));
        }

        [TestMethod]
        public void EditProduct_WithNegativeWeight()
        {
            var product = new Product("Old Name", 100.0, 10.0, new DateTime(2022, 12, 31), 10, "Old Description");
            var editedProduct = new Product("New Name", 200.0, -20.0, new DateTime(2023, 1, 1), 20, "New Description");

            Assert.ThrowsException<ArgumentException>(() => TestDataGenerator.EditProduct(ref product, editedProduct, false));
        }

        [TestMethod]
        public void EditProduct_WithZeroWeight()
        {
            var product = new Product("Old Name", 100.0, 10.0, new DateTime(2022, 12, 31), 10, "Old Description");
            var editedProduct = new Product("New Name", 200.0, 0.0, new DateTime(2023, 1, 1), 20, "New Description");

            Assert.ThrowsException<ArgumentException>(() => TestDataGenerator.EditProduct(ref product, editedProduct, false));
        }
        [TestMethod]
        public void EditProduct_WithVeryLargeWeight()
        {
            var product = new Product("Old Name", 100.0, 10.0, new DateTime(2022, 12, 31), 10, "Old Description");
            var editedProduct = new Product("New Name", 200.0, double.MaxValue, new DateTime(2023, 1, 1), 20, "New Description");

            Assert.ThrowsException<ArgumentException>(() => TestDataGenerator.EditProduct(ref product, editedProduct, false));
        }
        [TestMethod]
        public void EditProduct_WithNegativeDeliveryDate()
        {
            var product = new Product("Old Name", 100.0, 10.0, new DateTime(2022, 12, 31), 10, "Old Description");
            var editedProduct = new Product("New Name", 200.0, 20.0, DateTime.MinValue, 20, "New Description");

            Assert.ThrowsException<ArgumentException>(() => TestDataGenerator.EditProduct(ref product, editedProduct, false));
        }
        [TestMethod]
        public void EditProduct_WithFutureDeliveryDate()
        {
            var product = new Product("Old Name", 100.0, 10.0, new DateTime(2022, 12, 31), 10, "Old Description");
            var editedProduct = new Product("New Name", 200.0, 20.0, new DateTime(2023, 2, 1), 20, "New Description");

            Assert.ThrowsException<ArgumentException>(() => TestDataGenerator.EditProduct(ref product, editedProduct, false));
        }
        [TestMethod]
        public void EditProduct_WithEmptyDescription()
        {
            var product = new Product("Old Name", 100.0, 10.0, new DateTime(2022, 12, 31), 10, "Old Description");
            var editedProduct = new Product("New Name", 200.0, 20.0, new DateTime(2023, 1, 1), 20, "");

            Assert.ThrowsException<ArgumentException>(() => TestDataGenerator.EditProduct(ref product, editedProduct, false));
        }
        [TestMethod]
        public void EditProduct_WithVeryLongDescription()
        {
            var product = new Product("Old Name", 100.0, 10.0, new DateTime(2022, 12, 31), 10, "Old Description");
            var editedProduct = new Product("New Name", 200.0, 20.0, new DateTime(2023, 1, 1), 20, new string('A', 256));

            Assert.ThrowsException<ArgumentException>(() => TestDataGenerator.EditProduct(ref product, editedProduct, false));
        }
    }
}