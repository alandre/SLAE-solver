using System;

namespace SolverCore.Methods
{
    public class LOS : IMethod
    {
        IFactorization Factorizer;
        IVector x0, b, p, z, r, Ar;
        ILinearOperator A;
        double norm_b, dotproduct_pp, coefficient;
        int currentIter;
        bool init;
        public IVector x { get; private set; }

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
            this.x0 = x0;
            this.b = b;
            this.A = A;
            norm_b = b.Norm;
            currentIter = 0;
            //r = b.Add(A.Multiply(x0), -1);
            //z = r.Clone();
            //p = A.Multiply(r);
            r = A.LSolve(b.Add(A.Multiply(x0), -1), true);
            z = A.USolve(r, false);
            p = A.LSolve(A.Multiply(z), true);
            dotproduct_pp = p.DotProduct(p);
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

            coefficient = p.DotProduct(r) / dotproduct_pp;
            if (Double.IsInfinity(coefficient) || Double.IsNaN(coefficient))
            {
                residual = -1;
                return;
            }
            x = x.Add(z, coefficient);
            r = r.Add(p, -coefficient);
            // Ar = A.Multiply(r);
            Ar = A.LSolve(A.Multiply(A.USolve(r, false)), true);
            coefficient = -p.DotProduct(Ar) / dotproduct_pp;
            if (Double.IsInfinity(coefficient) || Double.IsNaN(coefficient))
            {
                residual = -1;
                return;
            }
            // z = r.Add(z, coefficient);
            z = A.USolve(r,false).Add(z, coefficient);
            p = Ar.Add(p, coefficient);
            dotproduct_pp = p.DotProduct(p);

            residual = r.Norm / norm_b;
        }
    }
}
