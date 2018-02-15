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
            var di = new double[] { 1, 1, 1, 1 };
            var al = new double[] { 1, 2, 3 };
            var au = new double[] { 4, 5, 6 };
            var ia = new int[] { 1, 1, 1, 2, 4 };
            var ja = new int[] { 1, 1, 2 };
            var matrix = new SolverCore.SparseRowColumnMatrix(di, al, au, ia, ja);

            foreach(var elem in matrix)
            {
                Console.WriteLine($"({elem.row}, {elem.col}): {elem.value}");
            }
        }
    }
}
