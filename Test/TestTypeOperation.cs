using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Product_library.Tests
{
    [TestClass]
    public class OrderCalculatorTests
    {
        [TestMethod]
        public void TestAdd()
        {
            var calculator = new OrderCalculator();
            var position = new PositionInCart(new ElectronicsProduct
            {
                Name = "Product1",
                Price = 100.0,
                Weight = 1.0,
                DeliveryDate = new DateTime(2022, 1, 1),
                Stock = 10,
                Description = "Описание",
                HasDryer = false
            });
            calculator.Add(position, 2);
            Assert.AreEqual(3, position.Quantity);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]

        public void TestAddNegativ()
        {
            var calculator = new OrderCalculator();
            var position = new PositionInCart(new ElectronicsProduct
            {
                Name = "Product1",
                Price = 100.0,
                Weight = 1.0,
                DeliveryDate = new DateTime(2022, 1, 1),
                Stock = 10,
                Description = "Описание",
                HasDryer = false
            });
            calculator.Add(position, -2);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddZero()
        {
            var calculator = new OrderCalculator();
            var position = new PositionInCart(new ElectronicsProduct
            {
                Name = "Product1",
                Price = 100.0,
                Weight = 1.0,
                DeliveryDate = new DateTime(2022, 1, 1),
                Stock = 10,
                Description = "Описание",
                HasDryer = false
            });
            calculator.Add(position, 0);
        }
        [TestMethod]
        public void TestSubtract()
        {
            var calculator = new OrderCalculator();
            var position = new PositionInCart(new ElectronicsProduct
            {
                Name = "Product1",
                Price = 100.0,
                Weight = 1.0,
                DeliveryDate = new DateTime(2022, 1, 1),
                Stock = 10,
                Description = "Описание",
                HasDryer = false
            });
            calculator.Add(position, 2);
            calculator.Subtract(position, 1);
            Assert.AreEqual(2, position.Quantity);
        }

        [TestMethod]
        public void TestMultiply()
        {
            var calculator = new OrderCalculator();
            var position = new PositionInCart(new ElectronicsProduct
            {
                Name = "Product1",
                Price = 100.0,
                Weight = 1.0,
                DeliveryDate = new DateTime(2022, 1, 1),
                Stock = 10,
                Description = "Описание",
                HasDryer = false
            });
            calculator.Add(position, 2);
            calculator.Multiply(position, 3);
            Assert.AreEqual(9, position.Quantity);
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void TestDivideByZero()
        {
            var calculator = new OrderCalculator();
            var position = new PositionInCart(new ElectronicsProduct
            {
                Name = "Product1",
                Price = 100.0,
                Weight = 1.0,
                DeliveryDate = new DateTime(2022, 1, 1),
                Stock = 10,
                Description = "Описание",
                HasDryer = false
            });
            calculator.Add(position, 2);
            calculator.Divide(position, 0);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddNegativeQuantity()
        {
            var calculator = new OrderCalculator();
            var position = new PositionInCart(new ElectronicsProduct
            {
                Name = "Product1",
                Price = 100.0,
                Weight = 1.0,
                DeliveryDate = new DateTime(2022, 1, 1),
                Stock = 10,
                Description = "Описание",
                HasDryer = false
            });
            calculator.Add(position, -2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddZeroQuantity()
        {
            var calculator = new OrderCalculator();
            var position = new PositionInCart(new ElectronicsProduct
            {
                Name = "Product1",
                Price = 100.0,
                Weight = 1.0,
                DeliveryDate = new DateTime(2022, 1, 1),
                Stock = 10,
                Description = "Описание",
                HasDryer = false
            });
            calculator.Add(position, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestSubtractNegativeQuantity()
        {
            var calculator = new OrderCalculator();
            var position = new PositionInCart(new ElectronicsProduct
            {
                Name = "Product1",
                Price = 100.0,
                Weight = 1.0,
                DeliveryDate = new DateTime(2022, 1, 1),
                Stock = 10,
                Description = "Описание",
                HasDryer = false
            });
            calculator.Add(position, 5);
            calculator.Subtract(position, -2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestSubtractZeroQuantity()
        {
            var calculator = new OrderCalculator();
            var position = new PositionInCart(new ElectronicsProduct
            {
                Name = "Product1",
                Price = 100.0,
                Weight = 1.0,
                DeliveryDate = new DateTime(2022, 1, 1),
                Stock = 10,
                Description = "Описание",
                HasDryer = false
            });
            calculator.Add(position, 5);
            calculator.Subtract(position, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void TestMultiplyOverflow()
        {
            var calculator = new OrderCalculator();
            var position = new PositionInCart(new ElectronicsProduct
            {
                Name = "Product1",
                Price = 100.0,
                Weight = 1.0,
                DeliveryDate = new DateTime(2022, 1, 1),
                Stock = 10,
                Description = "Описание",
                HasDryer = false
            });
            calculator.Add(position, 2);
            calculator.Multiply(position, int.MaxValue);
        }
    }
}