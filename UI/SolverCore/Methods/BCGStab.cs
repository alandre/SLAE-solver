using System;

namespace SolverCore.Methods
{
    class BCGStab : IMethod
    {
        IVector x, xk, b, z, r0, r, LAUz, LAUp, r_prev;
        ILinearOperator A;
        double norm_b, dotproduct_rr, dotproduct_rkr0, dotproduct_rprevr0;
        int currentIter;
        bool init;

        public BCGStab()
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

            xk = x0;
            this.b = b;
            this.A = A;
            norm_b = b.Norm;
            currentIter = 0;
            r_prev = A.LSolve(b.Add(A.Multiply(xk), -1), true);
            r0 = r_prev.Clone();
            z = A.USolve(r_prev.Clone(), true);
            dotproduct_rr = r_prev.DotProduct(r_prev);
            dotproduct_rprevr0 = dotproduct_rr;
            init = true;
            return x;
        }

        public void MakeStep(out int iter, out double residual)
        {
            if (!init)
            {
                throw new InvalidOperationException("Решатель не инициализирован, выполнение операции невозможно");
            }

            if (currentIter % 20 == 0)
            {
                r_prev = A.LSolve(b.Add(A.Multiply(xk), -1), true);
                r0 = r_prev.Clone();
                z = A.USolve(r_prev.Clone(), true);
                dotproduct_rr = r_prev.DotProduct(r_prev);
                dotproduct_rprevr0 = dotproduct_rr;
            }

            currentIter++;
            iter = currentIter;

            LAUz = A.LSolve(A.Multiply(A.USolve(z, true)), true);
            var alfa = dotproduct_rprevr0 / (LAUz.DotProduct(r0));

            var p = r_prev.Add(LAUz, -alfa);

            LAUp = A.LSolve(A.Multiply(A.USolve(p, true)), true);
            var gamma = (LAUp.DotProduct(p)) / (LAUp.DotProduct(LAUp));

            xk = xk.Add(z, alfa).Add(p, gamma);
            r = p.Add(LAUp, -gamma);

            dotproduct_rkr0 = r.DotProduct(r0);
            var betta = alfa * dotproduct_rkr0 / (gamma * dotproduct_rprevr0);
            z = r.Add(z, betta).Add(LAUz, -betta * gamma);

            dotproduct_rprevr0 = dotproduct_rkr0;
            r_prev = r;
            dotproduct_rr = r.DotProduct(r);

            if (Double.IsNaN(alfa) || Double.IsNaN(betta) || Double.IsNaN(gamma))
            {
                residual = -1;
                return;
            }

            x = A.USolve(xk, true);
            residual = Math.Sqrt(dotproduct_rr) / norm_b;
        }
    }
}
