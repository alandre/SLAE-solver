using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverCore
{
    enum MethodsEnum {CGMethod, GaussianSeidelMethod, JacobiMethod, LOSMethod, }
    public class SolversFabric
    {
        ISolver Spawn(enum ILogger Logger)
        {

            return new SolverCore.Solvers.LoggingSolver()
        }
    }
}
