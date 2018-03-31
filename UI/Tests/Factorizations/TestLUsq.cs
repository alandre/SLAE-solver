using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolverCore;
using Xunit;
using SolverCore.Factorizations;

namespace Factorizations
{
    
    public class TestLUsq 
    {
        CoordinationalMatrix FA;

        public TestLUsq()
        {
            int[] rows = new int[] { 0, 0, 0, 1, 1, 1, 2, 2, 2 };
            int[] collumns = new int[] { 0, 1, 2, 0, 1, 2, 0, 1, 2 };
            double[] values = new double[] { 4, 8, 12, 6, 28, 46, 10, 44, 121 };
            FA = new CoordinationalMatrix(rows, collumns, values, 3); // симметричная или нет?
        }


        [Fact]
        public void FactorizationL()
        {
            IncompleteLUsq incompleteLUsq = new IncompleteLUsq(FA);
            var result = incompleteLUsq.LMult(new Vector(new double[] { 1, 1, 1 }));
            double[] resultActual = new double[]
            {
                2,
                7,
                18
            };

            for (int i = 0; i < result.Size; i++)
            {
                Assert.Equal(result[i], resultActual[i], 8);
            }
        }

        [Fact]
        public void FactorizationU()
        {
            IncompleteLUsq incompleteLUsq = new IncompleteLUsq(FA);
            var result = incompleteLUsq.UMult(new Vector(new double[] { 1, 1, 1 }));
            double[] resultActual = new double[]
            {
               12,
               11,
               7
            };

            for (int i = 0; i < result.Size; i++)
            {
                Assert.Equal(result[i], resultActual[i], 8);
            }

        }

    }

}
