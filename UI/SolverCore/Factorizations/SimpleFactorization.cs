using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverCore.Factorizations
{
    public class SimpleFactorization : IFactorization
    {
        // M = [L+sqrt(D)]*[U+sqrt(D)]
        CoordinationalMatrix FactorizedMatrix;
        public SimpleFactorization(CoordinationalMatrix M)
        {
            Factorize(M);
        }
        public void Factorize(CoordinationalMatrix M)
        {
            FactorizedMatrix = (CoordinationalMatrix) M.Clone();
            for (int i = 0; i < FactorizedMatrix.Size; i++)
                FactorizedMatrix.Set(i, i, Math.Sqrt(FactorizedMatrix[i,i]));
        }
        public IVector LMult(IVector x)
        {
            return FactorizedMatrix.LMult(x, true);
        }

        public IVector LSolve(IVector x)
        {
            return FactorizedMatrix.LSolve(x, true);
        }

        public IVector LTransposeMult(IVector x)
        {
            return FactorizedMatrix.LMultTranspose(x, true);
        }

        public IVector LTransposeSolve(IVector x)
        {
            return FactorizedMatrix.LSolveTranspose(x, true);
        }

        public IVector UMult(IVector x)
        {
            return FactorizedMatrix.UMult(x, true);
        }

        public IVector USolve(IVector x)
        {
            return FactorizedMatrix.USolve(x, true);
        }

        public IVector UTransposeMult(IVector x)
        {
            return FactorizedMatrix.UMultTranspose(x, true);
        }

        public IVector UTransposeSolve(IVector x)
        {
            return FactorizedMatrix.USolveTranspose(x, true);
        }
    }
}
