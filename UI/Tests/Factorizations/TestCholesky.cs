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
    class FakeIncompleteCholesky
    {
        public CoordinationalMatrix factorizedMatix { get; private set; }

        public FakeIncompleteCholesky(CoordinationalMatrix M)
        {
            Factorize(M);
        }

        public void Factorize(CoordinationalMatrix M)
        {
            factorizedMatix = (CoordinationalMatrix)M.Clone();
            var rows = factorizedMatix.GetMatrixRows();

            if (Math.Abs(factorizedMatix[0, 0]) < 1.0E-14)
                return;

            foreach (var i in rows)
            {
                double sumD = 0;

                var columns = factorizedMatix.GetMatrixColumnsForRow(i);

                foreach (var j in columns)
                {
                    if (j >= i)
                        break;

                    double sumL = 0;

                    foreach (var k in columns)
                    {
                        if (k >= j - 1)
                            break;

                        sumL += factorizedMatix[i, k] * factorizedMatix[j, k];
                    }

                    var value = (M[i, j] - sumL) / factorizedMatix[j, j];
                    factorizedMatix.Set(i, j, value);
                    factorizedMatix.Set(j, i, value);

                    sumD += factorizedMatix[i, j] * factorizedMatix[i, j];
                }

                factorizedMatix.Set(i, i, Math.Sqrt(M[i, i] - sumD));
            }

        }

        public IVector LMult(IVector x)
        {
            return factorizedMatix.LMult(x, true);
        }

        public IVector LSolve(IVector x)
        {
            return factorizedMatix.LSolve(x, true);
        }

        public IVector LTransposeMult(IVector x)
        {
            return factorizedMatix.LMultTranspose(x, true);
        }

        public IVector LTransposeSolve(IVector x)
        {
            return factorizedMatix.LSolveTranspose(x, true);
        }

        public IVector UMult(IVector x)
        {
            return factorizedMatix.UMult(x, true);
        }

        public IVector USolve(IVector x)
        {
            return factorizedMatix.USolve(x, true);
        }

        public IVector UTransposeMult(IVector x)
        {
            return factorizedMatix.UMultTranspose(x, true);
        }

        public IVector UTransposeSolve(IVector x)
        {
            return factorizedMatix.USolveTranspose(x, true);
        }
    }

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
                Math.Sqrt(10) / 10 + 3 * Math.Sqrt(10) / 10,
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
                Math.Sqrt(7),
            };

            for (int i = 0; i < result.Size; i++)
            {
                Assert.Equal(result[i], resultActual[i], 8);
            }
        }
    }
}
