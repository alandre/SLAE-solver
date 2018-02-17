using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverCore
{
    class JacobiMethod : IMethod
    {
        IVector x, x0, b;
        ILinearOperator A;
        int max_iter;
        double eps;
        double norm_b;
        double last_discrepancy;
        int current_itter;
        bool init;

        JacobiMethod()
        {
            init = false;
        }

        public IVector InitMethod(ILinearOperator A, IVector x0, IVector b, int max_iter, double eps, bool malloc = false)
        {
            if(malloc == true)
            {
                x = new Vector(x0.Size);   
            }
            else
            {
                x = x0;
            }
 
            this.x0 = x0;
            this.b = b;
            this.A = A;
            this.max_iter = max_iter;
            this.eps = eps;
            current_itter = 0;
            norm_b = b.Norm;
            try
            {
                last_discrepancy = A.Multiply(x0).Add(b, -1).Norm / norm_b;
            }
            catch (DivideByZeroException e)
            {
                return null;
            }
            init = true;
            return x;
        }

        public bool MakeStep(out int iter, out double discrepancy)
        {
            if (init != true)
            {
                throw new InvalidOperationException("Решатель не инициализирован, выполнение операции невозможно");
            }

            if (Math.Abs(last_discrepancy) < eps)
            {
                discrepancy = last_discrepancy;
                iter = current_itter;
                return true;
            }
            current_itter++;

            var D = A.Diagonal;
            for (int i = 0; i < D.Size; i++)
            {
                D[i] = 1.0 / D[i];
            }
            var x_k = D.HadamardProduct(b.Add(A.LMult(x, false).Add(A.UMult(x, false)),-1));

            double w = 1.0;
            var x_temp = new Vector(x.Size);
            while (w >= 0.1)
            {
                for(int i = 0; i < x.Size; i++)
                {
                    x_temp[i] = w * x_k[i] + (1 - w) * x[i];
                }

                double temp_discrepancy = A.Multiply(x_temp).Add(b, -1).Norm / norm_b;

                if (temp_discrepancy < last_discrepancy)
                {
                    last_discrepancy = temp_discrepancy;
                    break;
                }
                w *= 0.9;
            }

            x = x_temp;

            last_discrepancy = A.Multiply(x).Add(b, -1).Norm / norm_b;
            discrepancy = last_discrepancy;
            iter = current_itter;
            return false;
        }
    }
}
