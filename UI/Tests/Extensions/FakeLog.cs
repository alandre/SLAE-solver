using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolverCore;

namespace Extensions
{
    internal class FakeLog : ILogger
    {
        public void read()
        {
            return;
        }

        public void write()
        {
            return;
        }

        public void Write(int Iter, double Residual)
        {
            throw new NotImplementedException();
        }
    }
}
