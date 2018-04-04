using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverCore
{
    public interface IMethod
    {
        /// <summary>
        /// Инициализация метода для решения слау Ax=b
        /// </summary>
        /// <param name="A">Матрица СЛАУ</param>
        /// <param name="x0">Начальное приблежение</param>
        /// <param name="b">Вектор правой части СЛАУ</param>
        /// <param name="malloc">false - результат сохраняется в вектор x0, true - выделяется новый вектор</param>
        /// <param name="Factorizer">Разложение, по умолчанию отсутствует</param>
        /// <returns>true - инициализация прошла успешно</returns>
        bool InitMethod(ILinearOperator A, IVector x0, IVector b, bool malloc = false, IFactorization Factorizer = null);

        /// <summary>
        /// Делает шаг иттерационного метода
        /// </summary>
        /// <param name="iter">номер текущей иттерации</param>
        /// <param name="residual">текущая относительная невязка</param>
        void MakeStep(out int iter, out double residual);
    
        IVector x { get; }

    }
}
