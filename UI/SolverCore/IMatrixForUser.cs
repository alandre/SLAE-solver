namespace SolverCore
{
    public interface IMatrixForUser : IMatrix
    {
        double this[int i, int j] { get; }
    }
}
