using Microsoft.VisualStudio.TestTools.UnitTesting;
using Product_library;
using System;

namespace ProductLibrary.Tests
{
    [TestClass]
    public class OrderCalculatorTests
    {
        [TestMethod]
        public void TestDivideByZero()
        {
            var calculator = new OrderCalculator();
            var position = new PositionInCart(new FoodProduct
            {
                Name = "Product1",
                Price = 10,
                Weight = 1,
                DeliveryDate = new DateTime(2022, 1, 1),
                Stock = 10,
                Description = "Описание",
                ExpirationDate = new DateTime(2023, 1, 1)
            });
            calculator.Add(position, 2);

            try
            {
                calculator.Divide(position, 0);
                Assert.Fail("Деление на ноль нельзя");
            }
            catch (DivideByZeroException)
            {
            }
        }

        [TestMethod]
        public void TestSubtractLargerQuantity()
        {
            var calculator = new OrderCalculator();
            var position = new PositionInCart(new ElectronicsProduct
            {
                Name = "Product1",
                Price = 10,
                Weight = 1,
                DeliveryDate = new DateTime(2022, 1, 1),
                Stock = 10,
                Description = "Описание",
                HasDryer = false
            });
            try
            {
                calculator.Subtract(position, 3);
                Assert.Fail("Ошибка, количество продукта выходит за границы");
            }
            catch (ArgumentException)
            {
            }
        }

        [TestMethod]
        public void TestMultiplyOverflow()
        {
            var calculator = new OrderCalculator();
            var position = new PositionInCart(new FurnitureProduct
            {
                Name = "Product1",
                Price = 10,
                Weight = 1,
                DeliveryDate = new DateTime(2022, 1, 1),
                Stock = 10,
                Description = "Описание",
                Material = "Материал"
            });
            calculator.Add(position, 2);
            try
            {
                calculator.Multiply(position, int.MaxValue);
                Assert.Fail("Ошибка, количество превышает границы");
            }
            catch (OverflowException)
            {
            }
        }

        [TestMethod]
        public void TestAddNegativeQuantity()
        {
            var calculator = new OrderCalculator();
            var position = new PositionInCart(new ElectronicsProduct
            {
                Name = "Product1",
                Price = 10,
                Weight = 1,
                DeliveryDate = new DateTime(2022, 1, 1),
                Stock = 10,
                Description = "Описание",
                HasDryer = false
            });
            try
            {
                calculator.Add(position, -1);
                Assert.Fail("Добавление отрицательного количества нельзя");
            }
            catch (ArgumentException)
            {
            }
        }
    }
}