using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SolverCore;

namespace Tests
{
    [TestClass]
    public class TestsVector
    {
        [TestMethod]
        public void Vector_TestConstructor()
        {
            var array = new[] { 1.0, 2.0, 3.0, 4.0, 5.0 };
            IVector vector = new Vector(array);

            Assert.AreEqual(array.Length, vector.Size);
            Assert.AreEqual(array[2], vector[2], 1e-8);     
        }

        [TestMethod]
        public void Vector_TestNegativeIndex()
        {
            var array = new[] { 1.0, 2.0, 3.0, 4.0, 5.0 };
            IVector vector = new Vector(array);

            //можно использовать NUnit и будет красивее выглядеть проверка. не хочу подгружать доп библиотеки которые не факт что есть у всех
            try
            {
                var element = vector[-1];
                Assert.Fail();
            }
            catch (IndexOutOfRangeException)
            {
                //так специально
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wrong exception : {ex.Message}");
                Assert.Fail();
            }
        }

        [TestMethod]
        public void Vector_TestIndexOverSize()
        {
            var array = new[] { 1.0, 2.0, 3.0, 4.0, 5.0 };
            IVector vector = new Vector(array);

            //можно использовать NUnit и будет красивее выглядеть проверка. не хочу подгружать доп библиотеки которые не факт что есть у всех
            try
            {
                var element = vector[vector.Size + 1];
                Assert.Fail();
            }
            catch (IndexOutOfRangeException)
            {
                //так специально
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wrong exception : {ex.Message}");
                Assert.Fail();
            }
        }

        [TestMethod]
        public void Vector_TestNullArgumentInConstructor()
        {
            //можно использовать NUnit и будет красивее выглядеть проверка. не хочу подгружать доп библиотеки которые не факт что есть у всех
            try
            {
                IVector vector = new Vector(null);
            }
            catch (ArgumentNullException)
            {
                //так специально
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wrong exception : {ex.Message}");
                Assert.Fail();
            }
        }
    }
}
