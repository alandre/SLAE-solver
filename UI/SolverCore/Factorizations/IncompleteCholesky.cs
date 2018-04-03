using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverCore.Factorizations
{
    public class IncompleteCholesky : IFactorization
    {
        CoordinationalMatrix factorizedMatix;

        public IncompleteCholesky(CoordinationalMatrix M)
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
                        if (k > j - 1)
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
}
