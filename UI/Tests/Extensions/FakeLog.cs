using System;
using System.Collections.Immutable;
using SolverCore;

namespace Extensions
{
    internal class FakeLog : ILogger
    {
        public (int currentIter, double residual) GetCurrentState()
        {
            return (0,0);
        }

        public ImmutableList<double> GetList()
        {
            return ImmutableList.CreateRange(new double[0] { });
        }

        public void Write(double residual)
        {
            return;
        }
    }
}
