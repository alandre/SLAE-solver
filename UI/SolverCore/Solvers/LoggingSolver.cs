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
    class LoggingSolver : ISolver
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

        public IVector Solve(ILinearOperator A, IVector x0, IVector b, int maxiter, double eps, bool malloc = false)
        {
            IVector result;
            int iter;
            double discrepancy;
            bool step_result;

            result = Method.InitMethod(A, x0, b, maxiter, eps, malloc);

            if (result == null)
                return null;

            while (true)
            {
                try
                {
                    step_result = Method.MakeStep(out iter, out discrepancy);
                }
                catch (Exception e)
                {
                    return null;
                }
                Logger.write();
                if (step_result)
                    break;
            }
            return result;
        }
    }
}
