using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;
using SolverCore;
using SolverCore.Factorizations;

namespace Factorizations
{
    
    

    public class TestLU 
    {
        CoordinationalMatrix FA;

        public TestLU()
        {
            int[] rows = new int[] { 0, 0, 0, 1, 1, 1, 2, 2, 2 };
            int[] collumns = new int[] { 0, 1, 2, 0, 1, 2, 0, 1, 2 };
            double[] values = new double[] { 10, 1, 2, 1, 10, 3, 2, 3, 10 };
            FA = new CoordinationalMatrix(rows, collumns, values, 3);
        }

        
        [Fact]
        public void FactorizationL()
        {
            IncompleteLU incompleteLU = new IncompleteLU(FA);
            var result = incompleteLU.LMult(new Vector(new double[] { 1, 1, 1 }));
            double[] resultActual = new double[]
            {
                1,
                1.0/10 + 1,
                1.0/5 + 28.0/99 + 1
            };

            for (int i = 0; i < result.Size; i++)
            {
                Assert.Equal(result[i], resultActual[i], 8);
            }



        }

        [Fact]
        public void FactorizationU()
        {
            IncompleteLU incompleteLU = new IncompleteLU(FA);
            var result = incompleteLU.UMult(new Vector(new double[] { 1, 1, 1 }));
            double[] resultActual = new double[]
            {
                10 + 1 + 2,
                99.0/10 + 14.0/5,
                872.0/99
            };

            for (int i = 0; i < result.Size; i++)
            {
                Assert.Equal(result[i], resultActual[i], 8);
            }



        }





    }

}
