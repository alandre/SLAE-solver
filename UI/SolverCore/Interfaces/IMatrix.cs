using System.Collections.Generic;

namespace SolverCore
{
    public interface IMatrix : ILinearOperator, IEnumerable<(double value, int row, int col)>
    {
        double this[int i, int j] { get; set; }
    }
}
