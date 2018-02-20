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
        /// <param name="maxIter">Макимальное число итераций</param>
        /// <param name="eps">Значение относительной невязки для выхода</param>
        /// <param name="malloc">false - результат сохраняется в вектор x0, true - выделяется новый вектор</param>
        /// <returns>Вектор с результатом</returns>
        IVector InitMethod(ILinearOperator A, IVector x0, IVector b, int maxIter, double eps, bool malloc = false);

        /// <summary>
        /// Делает шаг иттерационного метода
        /// </summary>
        /// <param name="iter">номер текущей иттерации</param>
        /// <param name="discrepancy">текущая относительная невязка</param>
        /// <returns>true-решение найдено</returns>
        bool MakeStep(out int iter, out double discrepancy);

    }
}
