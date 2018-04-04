
using System;

namespace SolverCore.Methods
{
    public class BCGStab : IMethod
    {
        IFactorization Factorizer;
        IVector xk, b, z, r0, r, LAUz, LAUp, r_prev, p, Az, Ap;
        ILinearOperator A;
        double norm_b, dotproduct_rr, dotproduct_rkr0, dotproduct_rprevr0;
        int currentIter;
        bool init = false;

        public IVector x
        {
            get
            {
                if (Factorizer == null)
                {
                    return xk;
                }
                else
                {
                    return Factorizer.USolve(xk);
                }

            }
            private set { }
        }

        public bool InitMethod(ILinearOperator A, IVector x0, IVector b, bool malloc = false, IFactorization Factorizer = null)
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
            this.Factorizer = Factorizer;
            norm_b = b.Norm;
            currentIter = 0;

            if (Factorizer != null)
            {
                r_prev = Factorizer.LSolve(b.Add(A.Multiply(xk), -1));
                r0 = r_prev.Clone();
                z = Factorizer.USolve(r_prev.Clone());
            }
            else
            {
                r_prev = b.Add(A.Multiply(xk), -1);
                r0 = r_prev.Clone();
                z = r_prev.Clone();
            }


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

            currentIter++;
            iter = currentIter;

            if (Factorizer != null)
            {
                if (currentIter % 5 == 0)
                {
                    r_prev = Factorizer.LSolve(b.Add(A.Multiply(xk), -1));
                    r0 = r_prev.Clone();
                    z = Factorizer.USolve(r_prev.Clone());
                    dotproduct_rr = r_prev.DotProduct(r_prev);
                    dotproduct_rprevr0 = dotproduct_rr;
                }

                LAUz = Factorizer.LSolve(A.Multiply(Factorizer.USolve(z)));
                var alpha = dotproduct_rprevr0 / (LAUz.DotProduct(r0));

                p = r_prev.Add(LAUz, -alpha);

                LAUp = Factorizer.LSolve(A.Multiply(Factorizer.USolve(p)));
                var gamma = (LAUp.DotProduct(p)) / (LAUp.DotProduct(LAUp));

                xk = xk.Add(z, alpha).Add(p, gamma);
                r = p.Add(LAUp, -gamma);

                dotproduct_rkr0 = r.DotProduct(r0);
                var beta = alpha * dotproduct_rkr0 / (gamma * dotproduct_rprevr0);
                z = r.Add(z, beta).Add(LAUz, -beta * gamma);

                dotproduct_rprevr0 = dotproduct_rkr0;
                r_prev = r;


                if (Double.IsNaN(beta) || Double.IsInfinity(beta))
                {
                    residual = -1;
                    return;
                }
            }
            else
            {
                if (currentIter % 5 == 0)
                {
                    r_prev = b.Add(A.Multiply(xk), -1);
                    r0 = r_prev.Clone();
                    z = r_prev.Clone();
                    dotproduct_rr = r_prev.DotProduct(r_prev);
                    dotproduct_rprevr0 = dotproduct_rr;
                }

                Az = A.Multiply(z);
                var alpha = dotproduct_rprevr0 / (Az.DotProduct(r0));

                p = r_prev.Add(Az, -alpha);

                Ap = A.Multiply(p);
                var gamma = (Ap.DotProduct(p)) / (Ap.DotProduct(Ap));

                xk = xk.Add(z, alpha).Add(p, gamma);
                r = p.Add(Ap, -gamma);

                dotproduct_rkr0 = r.DotProduct(r0);
                var beta = alpha * dotproduct_rkr0 / (gamma * dotproduct_rprevr0);
                z = r.Add(z, beta).Add(Az, -beta * gamma);

                dotproduct_rprevr0 = dotproduct_rkr0;
                r_prev = r;

                if (Double.IsNaN(beta) || Double.IsInfinity(beta))
                {
                    residual = -1;
                    return;
                }
            }

            dotproduct_rr = r.DotProduct(r);
            residual = Math.Sqrt(dotproduct_rr) / norm_b;
        }
    }
}