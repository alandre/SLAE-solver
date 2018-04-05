using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SolverCore;
using SolverCore.Factorizations;
using Xunit;
using Xunit.Abstractions;

namespace Factorizations
{

    public class TestSimple
    {
        CoordinationalMatrix FA;
        ITestOutputHelper _testOutputHelper;

        public TestSimple(ITestOutputHelper testOutputHelper)
        {
            int[] rows = new int[] { 0, 0, 0, 1, 1, 1, 2, 2, 2 };
            int[] collumns = new int[] { 0, 1, 2, 0, 1, 2, 0, 1, 2 };
            double[] values = new double[] { 10, 1, 2,
                                             1, 10, 3,
                                             2, 3, 10 };
            FA = new CoordinationalMatrix(rows, collumns, values, 3);
            _testOutputHelper = testOutputHelper;
        }



        // 10 1 2
        // 1 10 3
        // 2 3 10
        [Fact]
        public void FactorizationL()
        {
            SimpleFactorization simple = new SimpleFactorization(FA);
            var result = simple.LMult(new Vector(new double[] { 1, 1, 1 }));
            double[] resultActual = new double[]
            {
                Math.Sqrt(10),
                1 + Math.Sqrt(10),
                2 + 3 + Math.Sqrt(10)
            };

            foreach (var elem in result)
            {
                _testOutputHelper.WriteLine(elem.ToString());
            }

            for (int i = 0; i < result.Size; i++)
            {
                Assert.Equal(result[i], resultActual[i], 8);
            }



        }

        [Fact]
        public void FactorizationU()
        {
            SimpleFactorization simple = new SimpleFactorization(FA);
            var result = simple.UMult(new Vector(new double[] { 1, 1, 1 }));
            double[] resultActual = new double[]
            {
               Math.Sqrt(10) + 1 + 2,
               Math.Sqrt(10) + 3,
               Math.Sqrt(10)
            };

            foreach (var elem in result)
            {
                _testOutputHelper.WriteLine(elem.ToString());
            }

            for (int i = 0; i < result.Size; i++)
            {
                Assert.Equal(result[i], resultActual[i], 8);
            }



        }
    }
}
