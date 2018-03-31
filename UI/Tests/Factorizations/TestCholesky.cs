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
    

    public class TestCholesky
    {
        CoordinationalMatrix FA;

        public TestCholesky()
        {
            int[] rows = new int[] { 0, 0, 0, 1, 1, 1, 2, 2, 2 };
            int[] collumns = new int[] { 0, 1, 2, 0, 1, 2, 0, 1, 2 };
            double[] values = new double[] { 10, 1, 2, 1, 10, 3, 2, 3, 10 };
            FA = new CoordinationalMatrix(rows, collumns, values, 3); // симметричная или нет?
        }

        [Fact]
        public void Factorization()
        {
            IncompleteCholesky incompleteCholesky = new IncompleteCholesky(FA);
            var result = incompleteCholesky.LMult(new Vector(new double[] { 1, 1, 1 }));
            double[] resultActual = new double[]
            {
                Math.Sqrt(10),
                Math.Sqrt(10) / 10 + 3 * Math.Sqrt(110) / 10,
                Math.Sqrt(10) / 5 + 14*Math.Sqrt(110) / 165 + 2 * Math.Sqrt(2398) / 33
            };
            
            for(int i=0;i< result.Size;i++)
            {
                Assert.Equal(result[i], resultActual[i], 8);
            }
        }

        [Fact]
        public void FactorizationDiag()
        {
            int[] rows = new int[] { 0, 1, 2};
            int[] collumns = new int[] { 0, 1, 2};
            double[] values = new double[] { 10, 5, 7};
            FA = new CoordinationalMatrix(rows, collumns, values, 3); // симметричная или нет?

            IncompleteCholesky incompleteCholesky = new IncompleteCholesky(FA);
            var result = incompleteCholesky.LMult(new Vector(new double[] { 1, 1, 1 }));
            double[] resultActual = new double[]
            {
                Math.Sqrt(10),
                Math.Sqrt(5),
                Math.Sqrt(7)
            };

            for (int i = 0; i < result.Size; i++)
            {
                Assert.Equal(result[i], resultActual[i], 8);
            }
        }



        [Fact]
        public void FactorizationWithZeros()
        {
            int[] rows = new int[] { 0, 0, 1, 1, 1, 2, 2 };
            int[] collumns = new int[] { 0, 1, 0, 1, 2, 1, 2 };
            double[] values = new double[] { 10, 1, 1, 10, 2, 2, 10 };
            FA = new CoordinationalMatrix(rows, collumns, values, 3); // симметричная или нет?

            IncompleteCholesky incompleteCholesky = new IncompleteCholesky(FA);
            var result = incompleteCholesky.LMult(new Vector(new double[] { 1, 1, 1 }));
            double[] resultActual = new double[]
            {
                Math.Sqrt(10),
                Math.Sqrt(10) / 10 + 3*Math.Sqrt(110) / 10,
                2 * Math.Sqrt(110) / 33 + 5 * Math.Sqrt(418) / 33,
            };

            for (int i = 0; i < result.Size; i++)
            {
                Assert.Equal(result[i], resultActual[i], 8);
            }
        }


        [Fact]
        public void FactorizationWithZeros4dim()
        {
            int[] rows = new int[] { 0, 0, 1, 1, 1, 2, 2, 2, 3, 3 };
            int[] collumns = new int[] { 0, 1, 0, 1, 2, 1, 2, 3, 2, 3 };
            double[] values = new double[] { 100, 10, 10, 101, 20, 20, 104, 30, 30, 109 };
            FA = new CoordinationalMatrix(rows, collumns, values, 4); 

            IncompleteCholesky incompleteCholesky = new IncompleteCholesky(FA);
            var result = incompleteCholesky.LMult(new Vector(new double[] { 1, 1, 1, 1 }));
            double[] resultActual = new double[]
            {
               10,
               11,
               12,
               13
            };

            for (int i = 0; i < result.Size; i++)
            {
                Assert.Equal(result[i], resultActual[i], 8);
            }
        }
    }
}
