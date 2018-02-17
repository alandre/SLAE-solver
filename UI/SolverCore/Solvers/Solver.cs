using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverCore
{
    /// <summary>
    /// Решатель СЛАУ
    /// </summary>
    public class Solver : ISolver
    {
        IMethod Method;
        /// <summary>
        /// Конструктор решателя для конкретного метода
        /// </summary>
        /// <param name="Method"></param>
        public Solver(IMethod Method)
        {
            this.Method = Method;
        }
        public IVector Solve(IMatrix A, IVector x0, IVector b, int maxiter, double eps, bool malloc = false)
        {
            IVector result;
            int iter;
            double discrepancy;
            result = Method.InitMethod(A, x0, b, maxiter, eps, malloc);

            while (Method.MakeStep(out iter, out discrepancy) != true) ;

            return result;
        }
    }
}
