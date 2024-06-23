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
            var position = new PositionInCart(new Product("Product1", 10, 1, new DateTime(2022, 1, 1), 10, "Description"));
            calculator.Add(position, 2);
            Assert.AreEqual(3, position.Quantity);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]

        public void TestAddNegativ()
        {
            var calculator = new OrderCalculator();
            var position = new PositionInCart(new Product("Product1", 10, 1, new DateTime(2022, 1, 1), 10, "Description"));
            calculator.Add(position, -2);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddZero()
        {
            var calculator = new OrderCalculator();
            var position = new PositionInCart(new Product("Product1", 10, 1, new DateTime(2022, 1, 1), 10, "Description"));
            calculator.Add(position, 0);
        }
        [TestMethod]
        public void TestSubtract()
        {
            var calculator = new OrderCalculator();
            var position = new PositionInCart(new Product("Product1", 10, 1, new DateTime(2022, 1, 1), 10, "Description"));
            calculator.Add(position, 2);
            calculator.Subtract(position, 1);
            Assert.AreEqual(2, position.Quantity);
        }

        [TestMethod]
        public void TestMultiply()
        {
            var calculator = new OrderCalculator();
            var position = new PositionInCart(new Product("Product1", 10, 1, new DateTime(2022, 1, 1), 10, "Description"));
            calculator.Add(position, 2);
            calculator.Multiply(position, 3);
            Assert.AreEqual(9, position.Quantity);
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void TestDivideByZero()
        {
            var calculator = new OrderCalculator();
            var position = new PositionInCart(new Product("Product1", 10, 1, new DateTime(2022, 1, 1), 10, "Description"));
            calculator.Add(position, 2);
            calculator.Divide(position, 0);
        }

        [TestMethod]
        public void TestRemoveItemsByType()
        {
            var storage = new Storage();
            storage.LoadStock(@"C:\Users\Alexey\Desktop\Product_library\Products.json");
            var calculator = new OrderCalculator();
            var cart = new Cart();
            var position = new Product("ElectronicsProduct", 10, 1, new DateTime(2022, 1, 1), 10, "Description");
            cart.AddItem(position);
            calculator.RemoveItemsByType("ElectronicsProduct", cart, storage);
            Assert.IsFalse(cart.items.Any());
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddNegativeQuantity()
        {
            var calculator = new OrderCalculator();
            var position = new PositionInCart(new Product("Product1", 10, 1, new DateTime(2022, 1, 1), 10, "Description"));
            calculator.Add(position, -2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddZeroQuantity()
        {
            var calculator = new OrderCalculator();
            var position = new PositionInCart(new Product("Product1", 10, 1, new DateTime(2022, 1, 1), 10, "Description"));
            calculator.Add(position, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestSubtractNegativeQuantity()
        {
            var calculator = new OrderCalculator();
            var position = new PositionInCart(new Product("Product1", 10, 1, new DateTime(2022, 1, 1), 10, "Description"));
            calculator.Add(position, 5);
            calculator.Subtract(position, -2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestSubtractZeroQuantity()
        {
            var calculator = new OrderCalculator();
            var position = new PositionInCart(new Product("Product1", 10, 1, new DateTime(2022, 1, 1), 10, "Description"));
            calculator.Add(position, 5);
            calculator.Subtract(position, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void TestMultiplyOverflow()
        {
            var calculator = new OrderCalculator();
            var position = new PositionInCart(new Product("Product1", 10, 1, new DateTime(2022, 1, 1), 10, "Description"));
            calculator.Add(position, 2);
            calculator.Multiply(position, int.MaxValue);
        }
    }
}