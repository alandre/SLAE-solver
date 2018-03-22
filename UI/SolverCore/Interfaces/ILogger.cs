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
    }
}
