using System;

namespace SolverCore.Methods
{
    public class BCGStab : IMethod
    {
        IVector xk, b, z, r0, r, LAUz, LAUp, r_prev;
        ILinearOperator A;
        double norm_b, dotproduct_rr, dotproduct_rkr0, dotproduct_rprevr0;
        int currentIter;
        bool init = false;

        public IVector x { get; private set; }

        public bool InitMethod(ILinearOperator A, IVector x0, IVector b, bool malloc = false)
        {
            if (malloc)
            {
                x = new Vector(x0.Size);
            }
            else
            {
                x = x0;
            }

            xk = x0.Clone();
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
            return init;
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
            var alpha = dotproduct_rprevr0 / (LAUz.DotProduct(r0));

            var p = r_prev.Add(LAUz, -alpha);

            LAUp = A.LSolve(A.Multiply(A.USolve(p, true)), true);
            var gamma = (LAUp.DotProduct(p)) / (LAUp.DotProduct(LAUp));

            xk = xk.Add(z, alpha).Add(p, gamma);
            r = p.Add(LAUp, -gamma);

            dotproduct_rkr0 = r.DotProduct(r0);
            var beta = alpha * dotproduct_rkr0 / (gamma * dotproduct_rprevr0);
            z = r.Add(z, beta).Add(LAUz, -beta * gamma);

            dotproduct_rprevr0 = dotproduct_rkr0;
            r_prev = r;
            dotproduct_rr = r.DotProduct(r);

            if (Double.IsNaN(beta) || Double.IsInfinity(beta))
            {
                residual = -1;
                return;
            }

            x = A.USolve(xk, true);
            residual = Math.Sqrt(dotproduct_rr) / norm_b;
        }
    }
}
