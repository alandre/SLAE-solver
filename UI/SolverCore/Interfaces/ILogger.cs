using System.Collections.Immutable;

namespace SolverCore
{
    public interface ILogger
    {
        /// <summary>
        /// Запись текущей невязки в лог. Производится ровно один раз за итерацию.
        /// </summary>
        /// <param name="residual">Текущая невязка</param>
        void Write(double residual);

        /// <summary>
        /// Получает последние записанные итерацию и невязку.
        /// </summary>
        /// <returns>Возвращает текущую итерацию и невязку на ней.</returns>
        (int currentIter, double residual) GetCurrentState();

        /// <summary>
        /// Получает полный список невязок, расположенных подряд поитерационно.
        /// </summary>
        /// <returns></returns>
        ImmutableList<double> GetList();
    }
}
