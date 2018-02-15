using System;
using Xunit;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MF_Tests
{
    public class TestsDenseMatrix
    {
        [Fact]
        public void DenseMatrix_Foreach()
        {
            var matrix = new double[3][];
            matrix[0] = new[] { 1.0 };
            matrix[1] = new[] { 1.0, 2.0 };
            matrix[2] = new[] { 1, 2.0, 3 };

            var symm = new SolverCore.SymmetricDenseMatrix(matrix);

            var res = symm.Multiply(new SolverCore.Vector(new[] { 1.0, 1, 1 }));

            foreach(var el in res)
            {
                Console.WriteLine(el);
            }
        }
    }
}
