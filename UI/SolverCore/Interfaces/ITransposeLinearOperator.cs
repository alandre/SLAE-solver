namespace SolverCore
{
    public interface ITransposeLinearOperator
    {
        IVector MultiplyTranspose(IVector vector);
        IVector LMultTranspose(IVector vector, bool isUseDiagonal, int diagonalElement = 1);
        IVector UMultTranspose(IVector vector, bool isUseDiagonal, int diagonalElement = 1);
        IVector LSolveTranspose(IVector vector, bool isUseDiagonal);
        IVector USolveTranspose(IVector vector, bool isUseDiagonal);
    }
}
