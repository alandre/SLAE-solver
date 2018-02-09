namespace SolverCore
{
    public interface ILinearOperator
    {
        int Size { get; }
        IVector Diagonal { get; }

        IVector Multiply(IVector x);
        IVector LSolve(IVector x, bool UseDiagonal);
        IVector USolve(IVector x, bool UseDiagonal);
        IVector LMult(IVector x, bool UseDiagonal);
        IVector UMult(IVector x, bool UseDiagonal);
        ILinearOperator Transpose { get; }
    }
}
