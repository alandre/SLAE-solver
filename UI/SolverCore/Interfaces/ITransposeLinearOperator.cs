namespace SolverCore
{
    public interface ITransposeLinearOperator
    {
        IVector MultiplyTranspose(IVector vector);
        IVector LMultTranspose(IVector vector, bool isUseDiagonal);
        IVector UMultTranspose(IVector vector, bool isUseDiagonal);
        IVector LSolveTranspose(IVector vector, bool isUseDiagonal);
        IVector USolveTranspose(IVector vector, bool isUseDiagonal);
    }
}
