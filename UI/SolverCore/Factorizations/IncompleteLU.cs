using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverCore.Factorizations
{
    public class IncompleteLU : IFactorization
    {
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
