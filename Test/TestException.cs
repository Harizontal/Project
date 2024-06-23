using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Product_library;

namespace Product_library.Tests
{
    [TestClass]
    public class OrderCalculatorException
    {
        [TestMethod]
        public void TestDivideByZero()
        {
            var calculator = new OrderCalculator();
            var position = new PositionInCart(new Product("Product1", 10, 1, new DateTime(2022, 1, 1), 10, "Description"));
            calculator.Add(position, 2);

            try
            {
                calculator.Divide(position, 0);
                Assert.Fail("Divide by zero exception expected");
            }
            catch (DivideByZeroException)
            {
            }
        }

        [TestMethod]
        public void TestSubtractLargerQuantity()
        {
            var calculator = new OrderCalculator();
            var position = new PositionInCart(new Product("Product1", 10, 1, new DateTime(2022, 1, 1), 10, "Description"));
            try
            {
                calculator.Subtract(position, 3);
                Assert.Fail("Argument exception expected");
            }
            catch (ArgumentException)
            {
            }
        }

        [TestMethod]
        public void TestMultiplyOverflow()
        {
            var calculator = new OrderCalculator();
            var position = new PositionInCart(new Product("Product1", 10, 1, new DateTime(2022, 1, 1), 10, "Description"));
            calculator.Add(position, 2);
            try
            {
                calculator.Multiply(position, int.MaxValue);
                Assert.Fail("Overflow exception expected");
            }
            catch (OverflowException)
            {
            }
        }
    }
}