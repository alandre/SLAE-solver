namespace SolverCore
{
    public class TransposeMatrix<T> : ILinearOperator
        where T : ITransposeLinearOperator, ILinearOperator
    {
        public T Matrix { get; set; }

        public int Size => Matrix.Size;

        public IVector Diagonal => Matrix.Diagonal;

        public ILinearOperator Transpose => Matrix;

        public IVector LMult(IVector vector, bool isUseDiagonal, DiagonalElement diagonalElement) => Matrix.LMultTranspose(vector, isUseDiagonal, diagonalElement);

        public IVector LSolve(IVector vector, bool isUseDiagonal) => Matrix.LSolveTranspose(vector, isUseDiagonal);

        public IVector Multiply(IVector vector) => Matrix.MultiplyTranspose(vector);

        public IVector UMult(IVector vector, bool isUseDiagonal, DiagonalElement diagonalElement) => Matrix.UMultTranspose(vector, isUseDiagonal, diagonalElement);

        public IVector USolve(IVector vector, bool isUseDiagonal) => Matrix.USolveTranspose(vector, isUseDiagonal);
    }
}