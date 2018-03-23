using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverCore.Factorizations
{
    public class IncompleteLU : IFactorization
    {
        CoordinationalMatrix FA;

        public IncompleteLU(CoordinationalMatrix M)
        {
            Factorize(M);
        }

        public void Factorize(CoordinationalMatrix M)
        {
            FA = (CoordinationalMatrix) M.Clone();
            var rows = FA.GetMatrixRows();
            if (Math.Abs(FA[0, 0]) < 1.0E-14)
                return;

            foreach(var i in rows)
            { 
                double sumD = 0;
                var cols_i = FA.GetMatrixColumnsForRow(i);
                foreach (var j in cols_i)
                {
                    if (j >= i)
                        break;
                    double sumL = 0, sumU = 0;
                    foreach (var k in cols_i)
                    {
                        if (k >= j-1)
                            break;

                        sumL += FA[i, k] * FA[k, j];
                    }
                    foreach(var k in FA.GetMatrixRowsForColumn(j))
                    {
                        if (k >= i - 1)
                            break;
                        sumU += FA[i, k] * FA[k, j];
                    }
                    FA.Set(i, j, (M[i, j] - sumL) / FA[j, j]);
                    FA.Set(j, i, (M[j, i] - sumU));
                    sumD += FA[i, j] * FA[j, i];
                }
                FA.Set(i, i, M[i, i] - sumD);
            }
              
        }

        public IVector LMult(IVector x)
        {
            return FA.LMult(x, false, DiagonalElement.One);
        }

        public IVector LSolve(IVector x)
        {
            return FA.LSolve(x, false);
        }

        public IVector LTransposeMult(IVector x)
        {
            return FA.LMultTranspose(x, false, DiagonalElement.One);
        }

        public IVector LTransposeSolve(IVector x)
        {
            return FA.LSolveTranspose(x, false);
        }

        public IVector UMult(IVector x)
        {
            return FA.UMult(x, true);
        }

        public IVector USolve(IVector x)
        {
            return FA.USolve(x, true);
        }

        public IVector UTransposeMult(IVector x)
        {
            return FA.UMultTranspose(x, true);
        }

        public IVector UTransposeSolve(IVector x)
        {
            return FA.USolveTranspose(x, true);
        }
    }
}
