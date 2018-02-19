using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverCore.Methods
{
    class GaussianSeidelMethod : IMethod
    {
        IVector x, x0, b;
        ILinearOperator A;
        int maxIter;
        double eps;
        double norm_b;
        double lastDiscrepancy;
        int current_itter;
        bool init;
        IVector x_temp;
        GaussianSeidelMethod()
        {
            init = false;
        }
        public IVector InitMethod(ILinearOperator A, IVector x0, IVector b, int maxIter, double eps, bool malloc = false)
        {
            if (malloc == true)
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
            this.maxIter = maxIter;
            this.eps = eps;
            current_itter = 0;
            norm_b = b.Norm;
            try
            {
                lastDiscrepancy = A.Multiply(x0).Add(b, -1).Norm / norm_b;
            }
            catch (DivideByZeroException e)
            {
                return null;
            }
            init = true;
            x_temp = new Vector(x.Size);
            return x;
        }

        public bool MakeStep(out int iter, out double discrepancy)
        {
            if (init != true)
            {
                throw new InvalidOperationException("Решатель не инициализирован, выполнение операции невозможно");
            }

            if (Math.Abs(lastDiscrepancy) < eps)
            {
                discrepancy = lastDiscrepancy;
                iter = current_itter;
                return true;
            }
            current_itter++;

            var D = A.Diagonal;

            ////????????????????
            for (int i = 0; i < D.Size; i++)
            {
                D[i] = 1.0 / D[i];
            }
            ////????????????????

            var x_k = A.LSolve(b.Add(A.UMult(x, false, 0), -1), true);

            double w = 1.0;

            while (w >= 0.1)
            {
                ////????????????????
                for (int i = 0; i < x.Size; i++)
                {
                    x_temp[i] = w * x_k[i] + (1 - w) * x[i];
                }
                ////????????????????

                double temp_discrepancy = A.Multiply(x_temp).Add(b, -1).Norm / norm_b;

                if (temp_discrepancy < lastDiscrepancy)
                {
                    lastDiscrepancy = temp_discrepancy;
                    break;
                }
                w -= 0.1;
            }

            ////????????????????
            for (int i = 0; i < x.Size; i++)
            {
                x[i] = x_temp[i];
            }
            ////???????????????

            discrepancy = lastDiscrepancy;
            iter = current_itter;
            return false;
        }
    }
}
