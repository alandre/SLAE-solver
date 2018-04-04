using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Immutable;


namespace SolverCore.Loggers
{
    public class SaveBufferLogger : ILogger
    {
        ImmutableList<double> logList = ImmutableList.CreateRange(new double[0] { });

        /// <summary>
        /// Получает последние записанные итерацию и невязку.
        /// </summary>
        /// <returns>Возвращает текущую итерацию и невязку на ней.</returns>
        public (int currentIter, double residual) GetCurrentState()
        {
            if (!logList.IsEmpty)
            {
                int iter = logList.Count() - 1;
                double residual = logList[iter];
                return (iter, residual);
            }

            return (0, 0);
        }

        /// <summary>
        /// Получает полный список невязок, расположенных подряд поитерационно.
        /// </summary>
        /// <returns></returns>
        public ImmutableList<double> GetList()
        {
            return logList;
        }

        public void Write(double residual)
        {
            logList = logList.Add(residual);
        }

        public void RemoveLast()
        {
            int i = logList.Count() - 1;
            logList = logList.RemoveAt(i);
        }

    }
}
