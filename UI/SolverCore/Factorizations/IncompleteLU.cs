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

        public IncompleteLU()
        {

        }

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
                var cols_i = FA.GetMatrixCollumnsForRow(i);
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
                    foreach(var k in FA.GetMatrixRowsForCollumn(j))
                    {
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
            throw new NotImplementedException();
        }

        public IVector LSolve(IVector x)
        {
            throw new NotImplementedException();
        }

        public IVector LTransposeMult(IVector x)
        {
            throw new NotImplementedException();
        }

        public IVector LTransposeSolve(IVector x)
        {
            throw new NotImplementedException();
        }

        public IVector UMult(IVector x)
        {
            throw new NotImplementedException();
        }

        public IVector USolve(IVector x)
        {
            throw new NotImplementedException();
        }

        public IVector UTransposeMult(IVector x)
        {
            throw new NotImplementedException();
        }

        public IVector UTransposeSolve(IVector x)
        {
            throw new NotImplementedException();
        }
    }
}
