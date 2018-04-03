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
    public class LoggingSolver : ISolver
    {
        IMethod Method;
        ILogger Logger;
        /// <summary>
        /// Констуктор решателя
        /// </summary>
        /// <param name="Method">Конкретный метод решения</param>
        /// <param name="Logger">Логгер процесса решения</param>
        public LoggingSolver(IMethod Method, ILogger Logger)
        {
            this.Method = Method;
            this.Logger = Logger;
        }

        /// <summary>
        /// Решает СЛАУ Ax=b
        /// </summary>
        /// <param name="A">Матрица СЛАУ</param>
        /// <param name="x0">Начальное приблежение</param>
        /// <param name="b">Вектор правой части</param>
        /// <param name="maxIter">Максимальное число итераций</param>
        /// <param name="eps">Относительня невязка для выхода</param>
        /// <param name="malloc">false - результат сохранится в x0, true - результат сохранится в новый вектор</param>
        /// <returns></returns>
        public IVector Solve(ILinearOperator A, IVector x0, IVector b, int maxIter, double eps, bool malloc = false)
        {
            IVector result;
            int iter;
            double residual;

            if (!Method.InitMethod(A, x0, b, malloc))
                return null;

            while (true)
            {
                try
                {
                    Method.MakeStep(out iter, out residual);
                }
                catch (Exception e)
                {
                    return null;
                }

                Logger.Write(residual);
                if (iter >= maxIter || residual <= eps)
                    break;
            }

            return Method.x;
        }
    }
}
