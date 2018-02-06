namespace SolverCore
{
    public interface IMatrix
    {
        int Size { get; }
        IMatrix ConvertTo(string matrixFormat);
        IVector Multiply(IVector vector);
    }
}
