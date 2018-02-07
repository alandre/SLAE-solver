using System.Collections.Generic;

namespace SolverCore
{
    public interface IVector : IEnumerable<double>
    {
        int Size { get; }
        double this[int index] { get; }

        double Multiply(IVector vector);
        IVector Plus(IVector vector);
        IVector Minus(IVector vector);

        double Norm();
    }
}
