using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverCore.Solvers
{
    /// <summary>
    /// Решатель сохраняющий результат в объект логгер
    /// </summary>
    class LoggingSolver
    {
        IMethod Method;
        ILogger Logger;
        /// <summary>
        /// Констуктор решателя
        /// </summary>
        /// <param name="Method">Конкретный метод решения</param>
        /// <param name="Logger">Логгер процесса решения</param>
        LoggingSolver(IMethod Method, ILogger Logger)
        {
            this.Method = Method;
            this.Logger = Logger;
        }


    }
}
