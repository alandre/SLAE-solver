using System;
using System.Collections.Immutable;
using SolverCore;

namespace Extensions
{
    internal class FakeLog : ILogger
    {
        public (int currentIter, double residual) GetCurrentState()
        {
            throw new NotImplementedException();
        }

        public ImmutableList<double> GetList()
        {
            throw new NotImplementedException();
        }

        public void Write(double residual)
        {
            throw new NotImplementedException();
        }
    }
}
