using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tests
{
    [TestClass]
    public class TestsDenseMatrix
    {
        [TestMethod]
        public void DenseMatrix_Foreach()
        {
            var matrix = new[,] { { 1.0, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            var denseMatrix = new SolverCore.DenseMatrix(matrix);

            foreach(var elem in matrix)
            {
                Console.WriteLine(elem);
            }
        }
    }
}
