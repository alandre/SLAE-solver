using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverCore.Methods
{
    class CGM : IMethod
    {
        IVector x, x0, b, z, r, Az;
        ILinearOperator A;
        double norm_b, dotproduct_rr, coefficient;
        int currentIter;
        bool init;

        CGM()
        {
            init = false;
        }
        //не предобусловленная система
        public IVector InitMethod(ILinearOperator A, IVector x0, IVector b, bool malloc = false)
        {
            if (malloc)
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
            norm_b = b.Norm;
            //???
            try
            {
                coefficient = 1 / norm_b;
            }
            catch (DivideByZeroException)
            {
                return null;
            }
            currentIter = 0;
            r = b.Add(A.Multiply(x0), -1);
            z = r.Clone();
            dotproduct_rr = r.DotProduct(r);
            init = true;
            return x;
        }
        
        public void MakeStep(out int iter, out double residual)
        {
            if (!init)
            {
                throw new InvalidOperationException("Решатель не инициализирован, выполнение операции невозможно");
            }

            currentIter++;
            iter = currentIter;

            Az = A.Multiply(z);

            //???
            try
            {
                coefficient = dotproduct_rr / Az.DotProduct(z);
            }
            catch (DivideByZeroException)
            {
                residual = -1;
                return;
            }

            x = x.Add(z, coefficient);
            r = r.Add(Az, -coefficient);
            coefficient = dotproduct_rr;
            dotproduct_rr = r.DotProduct(r);

            //???
            try
            {
                coefficient = dotproduct_rr / coefficient;
            }
            catch (DivideByZeroException)
            {
                residual = -1;
                return;
            }
            z = r.Add(z, coefficient);

            residual = Math.Sqrt(dotproduct_rr) / norm_b;  
        }
    }
}