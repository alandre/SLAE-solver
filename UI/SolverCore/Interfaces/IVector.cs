using System.Collections.Generic;

namespace SolverCore
{
    public interface IVector : IEnumerable<double>
    {
        int Size { get; }
        double this[int index] { get; set; }
        double Norm { get; }
        double Dot(IVector vector);
        IVector Add(IVector vector, double multiplier = 1);
        IVector Clone();
        void SetConst(double value = 0);
    }
}
