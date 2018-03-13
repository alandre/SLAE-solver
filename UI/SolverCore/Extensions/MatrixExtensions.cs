using System.Linq;

namespace SolverCore
{
    public static class MatrixExtensions
    {
        public static CoordinationalMatrix ConvertToCoordinationalMatrix(this IMatrix matrix) => new CoordinationalMatrix(matrix.Select(x => (x.row, x.col, x.value)), matrix.Size);
    }
}
