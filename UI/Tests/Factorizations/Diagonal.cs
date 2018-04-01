using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolverCore;
using SolverCore.Factorizations;
using Xunit;

namespace Factorizations
{

    public class TestDiagonal
    {
        CoordinationalMatrix FA;

        public TestDiagonal()
        {
            int[] rows = new int[] { 0, 0, 0, 1, 1, 1, 2, 2, 2 };
            int[] collumns = new int[] { 0, 1, 2, 0, 1, 2, 0, 1, 2 };
            double[] values = new double[] { 10, 1, 2, 1, 10, 3, 2, 3, 10 };
            FA = new CoordinationalMatrix(rows, collumns, values, 3); 
        }

        [Fact]
        public void FactorizationDiag()
        {
            DioganalFactorization diag = new DioganalFactorization(FA);
            var result = diag.LMult(new Vector(new double[] { 1, 1, 1 }));
            double[] resultActual = new double[]
            {
                Math.Sqrt(10),
                Math.Sqrt(10),
                Math.Sqrt(10)
            };

            for (int i = 0; i < result.Size; i++)
            {
                Assert.Equal(result[i], resultActual[i], 8);
            }
        }

        [Fact]
        public void FactorizationSolve()
        {
            DioganalFactorization diag = new DioganalFactorization(FA);
            var result = diag.LSolve(new Vector(new double[] { 2, 3, 4 }));
            double[] resultActual = new double[]
            {
                2.0 / Math.Sqrt(10),
                3.0 / Math.Sqrt(10),
                4.0 / Math.Sqrt(10)
            };

            for (int i = 0; i < result.Size; i++)
            {
                Assert.Equal(result[i], resultActual[i], 8);
            }
        }
    }
}
