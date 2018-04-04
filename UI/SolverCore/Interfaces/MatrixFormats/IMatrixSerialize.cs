using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverCore
{
    public interface IMatrixSerialize
    {
        string Serialize(IVector b, IVector x0);
        string BinarySerialize(IVector b, IVector x0);
    }
}
