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
            double[] values = new double[] { 10, 1, 2, 1, 10, 3, 2, 3, 10 };
            //double[] values = new double[] { 16,24,8,8,21,13,12,30,22 };
            FA = new CoordinationalMatrix(rows, collumns, values, 3); // симметричная или нет?
        }


        [Fact]
        public void FactorizationL()
        {
            IncompleteLUsq incompleteLUsq = new IncompleteLUsq(FA);
            var result = incompleteLUsq.LMult(new Vector(new double[] { 1, 1, 1 }));
            double[] resultActual = new double[]
            {
                Math.Sqrt(10),
                1.0/Math.Sqrt(10) + Math.Sqrt(9.9),
                Math.Sqrt(2.0 / Math.Sqrt(10) +
                (3.0 - 2.0/10 ) / Math.Sqrt(9.9)
                + Math.Sqrt(10 - 0.4 - Math.Pow((3.0 - 2.0 / 10 ) / Math.Sqrt(9.9), 2)))
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
                Math.Sqrt(10),
                1.0/Math.Sqrt(10) + Math.Sqrt(9.9),
                Math.Sqrt(2.0 / Math.Sqrt(10) +
                (3.0 - 2.0/10 ) / Math.Sqrt(9.9)
                + Math.Sqrt(10 - 0.4 - Math.Pow((3.0 - 2.0 / 10 ) / Math.Sqrt(9.9), 2)))
            };

            for (int i = 0; i < result.Size; i++)
            {
                Assert.Equal(result[i], resultActual[i], 8);
            }



        }





    }

}
