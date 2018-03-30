using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverCore.Factorizations
{
    public class DioganalFactorization : IFactorization
    {
        // M = D*D
        IVector D;
        IVector DInvert;
        public DioganalFactorization(CoordinationalMatrix M)
        {
            Factorize(M);
        }

        public void Factorize(CoordinationalMatrix M)
        {
            D = M.Diagonal.Clone();
            for (int i = 0; i < D.Size; i++)
            {
                D[i] = Math.Sqrt(D[i]);
            }
            DInvert = D.Clone();
            for(int i = 0; i < DInvert.Size; i++)
            {
                DInvert[i] = 1 / DInvert[i];
            }

        }

        public IVector LMult(IVector x)
        {
            return D.HadamardProduct(x);
        }

        public IVector LSolve(IVector x)
        {
            return DInvert.HadamardProduct(x);
        }

        public IVector LTransposeMult(IVector x) => LMult(x);

        public IVector LTransposeSolve(IVector x) => LSolve(x);

        public IVector UMult(IVector x) => LMult(x);

        public IVector USolve(IVector x) => LSolve(x);

        public IVector UTransposeMult(IVector x) => UMult(x);

        public IVector UTransposeSolve(IVector x) => USolve(x);
    }
}
