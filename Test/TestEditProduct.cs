using Microsoft.VisualStudio.TestTools.UnitTesting;
using Product_library;
using ProductLibrary;
using ProductLibraryConsoleApp;
using System;

namespace ProductLibrary.Tests
{
    [TestClass]
    public class ProductTests
    {
        [TestMethod]
        public void EditProduct_WithValidData()
        {
            var product = new ElectronicsProduct
            {
                Name = "Product1",
                Price = 100.0,
                Weight = 1.0,
                DeliveryDate = new DateTime(2022, 1, 1),
                Stock = 10,
                Description = "Описание",
                HasDryer = false
            };

            var editedProduct = new ElectronicsProduct
            {
                Name = "New Name",
                Price = 200.0,
                Weight = 2.0,
                DeliveryDate = new DateTime(2023, 1, 1),
                Stock = 20,
                Description = "New Description",
                HasDryer = true
            };

            TestDataGenerator.EditProduct(ref product, editedProduct, false);

            Assert.AreEqual("New Name", product.Name);
            Assert.AreEqual(200.0, product.Price);
            Assert.AreEqual(2.0, product.Weight);
            Assert.AreEqual(new DateTime(2023, 1, 1), product.DeliveryDate);
            Assert.AreEqual(20, product.Stock);
            Assert.AreEqual("New Description", product.Description);
            Assert.AreEqual(true, product.HasDryer);
        }

        [TestMethod]
        public void EditProduct_WithEmptyName()
        {
            var product = new ElectronicsProduct
            {
                Name = "Product1",
                Price = 100.0,
                Weight = 1.0,
                DeliveryDate = new DateTime(2022, 1, 1),
                Stock = 10,
                Description = "Описание",
                HasDryer = false
            };

            var editedProduct = new ElectronicsProduct
            {
                Name = "",
                Price = 200.0,
                Weight = 2.0,
                DeliveryDate = new DateTime(2023, 1, 1),
                Stock = 20,
                Description = "New Description",
                HasDryer = true
            };

            Assert.ThrowsException<ArgumentException>(() => TestDataGenerator.EditProduct(ref product, editedProduct, false));
        }

        [TestMethod]
        public void EditProduct_WithNegativePrice()
        {
            var product = new FurnitureProduct
            {
                Name = "Product1",
                Price = 100.0,
                Weight = 10.0,
                DeliveryDate = new DateTime(2022, 1, 1),
                Stock = 10,
                Description = "Описание",
                Material = "Материал"
            };

            var editedProduct = new FurnitureProduct
            {
                Name = "New Name",
                Price = -200.0,
                Weight = 20.0,
                DeliveryDate = new DateTime(2023, 1, 1),
                Stock = 20,
                Description = "New Description",
                Material = "New Material"
            };

            Assert.ThrowsException<ArgumentException>(() => TestDataGenerator.EditProduct(ref product, editedProduct, false));
        }

        [TestMethod]
        public void EditProduct_WithZeroPrice()
        {
            var product = new FurnitureProduct
            {
                Name = "Product1",
                Price = 100.0,
                Weight = 10.0,
                DeliveryDate = new DateTime(2022, 1, 1),
                Stock = 10,
                Description = "Описание",
                Material = "Материал"
            };

            var editedProduct = new FurnitureProduct
            {
                Name = "New Name",
                Price = 0.0,
                Weight = 10.0,
                DeliveryDate = new DateTime(2021, 1, 1),
                Stock = 10,
                Description = "Описание",
                Material = "Материал",
            };

            Assert.ThrowsException<ArgumentException>(() => TestDataGenerator.EditProduct(ref product, editedProduct, false));

        }
        [TestMethod]
        public void EditProduct_WithNegativeWeight()
        {
            var product = new FurnitureProduct
            {
                Name = "Product1",
                Price = 100.0,
                Weight = 10.0,
                DeliveryDate = new DateTime(2022, 1, 1),
                Stock = 10,
                Description = "Описание",
                Material = "Материал"
            };

            var editedProduct = new FurnitureProduct
            {
                Name = "New Name",
                Price = 11.1,
                Weight = -12.0,
                DeliveryDate = new DateTime(2021, 1, 1),
                Stock = 10,
                Description = "Описание",
                Material = "Материал",
            };

            Assert.ThrowsException<ArgumentException>(() => TestDataGenerator.EditProduct(ref product, editedProduct, false));
        }
        [TestMethod]
        public void EditProduct_WithZeroWeight()
        {
            var product = new FurnitureProduct
            {
                Name = "Product1",
                Price = 100.0,
                Weight = 10.0,
                DeliveryDate = new DateTime(2022, 1, 1),
                Stock = 10,
                Description = "Описание",
                Material = "Материал"
            };

            var editedProduct = new FurnitureProduct
            {
                Name = "New Name",
                Price = 11.1,
                Weight = 0.0,
                DeliveryDate = new DateTime(2021, 1, 1),
                Stock = 10,
                Description = "Описание",
                Material = "Материал",
            };

            Assert.ThrowsException<ArgumentException>(() => TestDataGenerator.EditProduct(ref product, editedProduct, false));
        }
        [TestMethod]
        public void EditProduct_WithNegativeDeliveryDate()
        {
            var product = new FurnitureProduct
            {
                Name = "Product1",
                Price = 100.0,
                Weight = 10.0,
                DeliveryDate = new DateTime(2022, 1, 1),
                Stock = 10,
                Description = "Описание",
                Material = "Материал"
            };

            var editedProduct = new FurnitureProduct
            {
                Name = "New Name",
                Price = 11.1,
                Weight = 30.0,
                DeliveryDate = new DateTime(-2021, -1, -1),
                Stock = 10,
                Description = "Описание",
                Material = "Материал",
            };

            Assert.ThrowsException<ArgumentException>(() => TestDataGenerator.EditProduct(ref product, editedProduct, false));
        }
        [TestMethod]
        public void EditProduct_WithZeroDeliveryDate()
        {
            var product = new FurnitureProduct
            {
                Name = "Product1",
                Price = 100.0,
                Weight = 10.0,
                DeliveryDate = new DateTime(0, 0, 0),
                Stock = 10,
                Description = "Описание",
                Material = "Материал"
            };

            var editedProduct = new FurnitureProduct
            {
                Name = "New Name",
                Price = 11.1,
                Weight = 30.0,
                DeliveryDate = new DateTime(-2021, -1, -1),
                Stock = 10,
                Description = "Описание",
                Material = "Материал",
            };

            Assert.ThrowsException<ArgumentException>(() => TestDataGenerator.EditProduct(ref product, editedProduct, false));
        }
        [TestMethod]
        public void EditProduct_WithNegativeStock()
        {
            var product = new FurnitureProduct
            {
                Name = "Product1",
                Price = 100.0,
                Weight = 10.0,
                DeliveryDate = new DateTime(2022, 1, 1),
                Stock = 10,
                Description = "Описание",
                Material = "Материал"
            };

            var editedProduct = new FurnitureProduct
            {
                Name = "New Name",
                Price = 11.1,
                Weight = 30.0,
                DeliveryDate = new DateTime(2021, 1, 1),
                Stock = -20,
                Description = "Описание",
                Material = "Материал",
            };

            Assert.ThrowsException<ArgumentException>(() => TestDataGenerator.EditProduct(ref product, editedProduct, false));
        }
        [TestMethod]
        public void EditProduct_WithZeroStock()
        {
            var product = new FurnitureProduct
            {
                Name = "Product1",
                Price = 100.0,
                Weight = 10.0,
                DeliveryDate = new DateTime(2022, 1, 1),
                Stock = 10,
                Description = "Описание",
                Material = "Материал"
            };

            var editedProduct = new FurnitureProduct
            {
                Name = "New Name",
                Price = 11.1,
                Weight = 30.0,
                DeliveryDate = new DateTime(2021, 1, 1),
                Stock = 0,
                Description = "Описание",
                Material = "Материал",
            };

            Assert.ThrowsException<ArgumentException>(() => TestDataGenerator.EditProduct(ref product, editedProduct, false));
        }
        [TestMethod]
        public void EditProduct_WithEmptyDescription()
        {
            var product = new FurnitureProduct
            {
                Name = "Product1",
                Price = 100.0,
                Weight = 10.0,
                DeliveryDate = new DateTime(2022, 1, 1),
                Stock = 10,
                Description = "Описание",
                Material = "Материал"
            };

            var editedProduct = new FurnitureProduct
            {
                Name = "New Name",
                Price = 11.1,
                Weight = 30.0,
                DeliveryDate = new DateTime(2021, 1, 1),
                Stock = -20,
                Description = "",
                Material = "Материал",
            };

            Assert.ThrowsException<ArgumentException>(() => TestDataGenerator.EditProduct(ref product, editedProduct, false));
        }
        [TestMethod]
        public void EditProduct_WithEmptyMaterial()
        {
            var product = new FurnitureProduct
            {
                Name = "Product1",
                Price = 100.0,
                Weight = 10.0,
                DeliveryDate = new DateTime(2022, 1, 1),
                Stock = 10,
                Description = "Описание",
                Material = "Материал"
            };

            var editedProduct = new FurnitureProduct
            {
                Name = "New Name",
                Price = 11.1,
                Weight = 30.0,
                DeliveryDate = new DateTime(2021, 1, 1),
                Stock = 0,
                Description = "Описание",
                Material = "",
            };

            Assert.ThrowsException<ArgumentException>(() => TestDataGenerator.EditProduct(ref product, editedProduct, false));
        }
        [TestMethod]
        public void EditProduct_WithEmptyExpirationDate()
        {
            var product = new FoodProduct
            {
                Name = "Product1",
                Price = 100.0,
                Weight = 10.0,
                DeliveryDate = new DateTime(2022, 1, 1),
                Stock = 10,
                Description = "Описание",
                ExpirationDate = new DateTime(2023, 1, 1)
            };

            var editedProduct = new FoodProduct
            {
                Name = "New Name",
                Price = 11.1,
                Weight = 30.0,
                DeliveryDate = new DateTime(2021, 1, 1),
                Stock = -20,
                Description = "Описание",
                ExpirationDate = new DateTime(0, 0, 0)
            };

            Assert.ThrowsException<ArgumentException>(() => TestDataGenerator.EditProduct(ref product, editedProduct, false));
        }
}   }   
               