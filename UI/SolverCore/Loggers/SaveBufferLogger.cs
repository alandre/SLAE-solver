using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace SolverCore.Loggers
{
    class SaveBufferLogger : ILogger
    {
        ConcurrentDictionary<int, double> LogTable;

        //Удалить
        public void read()
        {
            throw new NotImplementedException();
        }

        public void write()
        {
            throw new NotImplementedException();
        }

        public void Write(int Iter, double Residual)
        {
            LogTable.TryAdd(Iter, Residual);
            throw new NotImplementedException();
        }

    }
}
