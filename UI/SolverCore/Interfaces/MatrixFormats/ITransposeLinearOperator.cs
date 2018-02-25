namespace SolverCore
{
    public interface ITransposeLinearOperator
    {
        IVector MultiplyTranspose(IVector vector);
        IVector LMultTranspose(IVector vector, bool isUseDiagonal, DiagonalElement diagonalElement = DiagonalElement.Zero);
        IVector UMultTranspose(IVector vector, bool isUseDiagonal, DiagonalElement diagonalElement = DiagonalElement.Zero);
        IVector LSolveTranspose(IVector vector, bool isUseDiagonal);
        IVector USolveTranspose(IVector vector, bool isUseDiagonal);
    }
}
