using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverCore.Methods
{
    public class GaussianSeidelMethod : IMethod
    {
        IVector x, x0, b;
        ILinearOperator A;
        double norm_b;
        double lastResidual;
        int currentIter;
        bool init;
        IVector x_temp;
        IVector Ux;

        public GaussianSeidelMethod()
        {
            init = false;
        }
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
            currentIter = 0;
            norm_b = b.Norm;
            try
            {
                lastResidual = A.Multiply(x0).Add(b, -1).Norm / norm_b;
            }
            catch (DivideByZeroException e)
            {
                return null;
            }
            init = true;
            x_temp = new Vector(x.Size);
            Ux = A.UMult(x0, false, 0);
            return x;
        }

        public void MakeStep(out int iter, out double residual)
        {
            if (!init)
            {
                throw new InvalidOperationException("Решатель не инициализирован, выполнение операции невозможно");
            }

            currentIter++;

            //x_k=(L+D)^(-1)*(b-Ux)
            var x_k = A.LSolve(b.Add(Ux, -1), true);

            double w = 1.0;

            while (w >= 0.1)
            {
                ////????????????????
                for (int i = 0; i < x.Size; i++)
                {
                    x_temp[i] = w * x_k[i] + (1 - w) * x[i];
                }
                ////????????????????

                Ux = Ux = A.UMult(x_temp, false, 0);

                //tempResidual = ||b - (Ux+Lx+Dx)|| / ||b||
                double tempResidual = Ux.Add(A.LMult(x_temp,false,0)).Add(A.Diagonal.HadamardProduct(x_temp)).Add(b, -1).Norm / norm_b;

                if (tempResidual < lastResidual)
                {
                    lastResidual = tempResidual;
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

            residual = lastResidual;
            iter = currentIter;
        }
    }
}
