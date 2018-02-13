using System;
using System.Collections.Generic;

namespace SolverCore
{
    /// <summary>
    /// Функция для заполнения матрицы по элементам
    /// </summary>
    /// <param name="i"></param>
    /// <param name="j"></param>
    /// <returns></returns>
    public delegate double FillFunc(int i, int j);
    public interface IMatrix : ILinearOperator, IEnumerable<(double value, int row, int col)>
    {
        double this[int i, int j] { get; }
        CoordinationalMatrix ConvertToCoordinationalMatrix();
        void Fill(FillFunc elems);
    }
}
