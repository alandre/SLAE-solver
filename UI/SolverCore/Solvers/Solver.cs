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
        public IVector Solve(ILinearOperator A, IVector x0, IVector b, int maxiter, double eps, bool malloc = false)
        {
            IVector result;
            int iter;
            double discrepancy;
            bool step_result;
       
            result = Method.InitMethod(A, x0, b, maxiter, eps, malloc);

            if (result == null)
                return null;

            while (true)
            {
                try
                {
                    step_result = Method.MakeStep(out iter, out discrepancy);
                }
                catch (Exception e)
                {
                    return null;
                }

                if (step_result)
                    break;
            }
            return result;
        }
    }
}
