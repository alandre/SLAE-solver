using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverCore.Factorizations
{
    public class SimpleFactorization : IFactorization
    {
        // M = L*(U+D)
        CoordinationalMatrix FactorizedMatrix;
        public SimpleFactorization(CoordinationalMatrix M)
        {
            Factorize(M);
        }
        public void Factorize(CoordinationalMatrix M)
        {
            FactorizedMatrix = (CoordinationalMatrix) M.Clone();
        }
        public IVector LMult(IVector x)
        {
            return FactorizedMatrix.LMult(x, false, DiagonalElement.One);
        }

        public IVector LSolve(IVector x)
        {
            return FactorizedMatrix.LSolve(x, false);
        }

        public IVector LTransposeMult(IVector x)
        {
            return FactorizedMatrix.LMultTranspose(x, false, DiagonalElement.One);
        }

        public IVector LTransposeSolve(IVector x)
        {
            return FactorizedMatrix.LSolveTranspose(x, false);
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
