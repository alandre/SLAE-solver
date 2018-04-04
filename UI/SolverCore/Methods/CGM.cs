using System;

namespace SolverCore.Methods
{
    public class CGM : IMethod
    {
        IFactorization Factorizer;
        IVector x0, b, z, r, Az, xk;
        ILinearOperator A, At;
        double norm_b, dotproduct_rr, coefficient;
        int currentIter;
        bool init;
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
            this.x0 = x0;
            this.b = b;
            this.A = A;
            this.Factorizer = Factorizer;

            At = A.Transpose;
            norm_b = b.Norm;
            currentIter = 0;

            if (Factorizer != null)
            {
                r = Factorizer.UTransposeSolve(At.Multiply(Factorizer.LTransposeSolve(Factorizer.LSolve(b.Add(A.Multiply(x0),-1)))));
                z = r.Clone();
                xk = Factorizer.UMult(x0);
            }
            else
            {
                r = At.Multiply(b).Add(At.Multiply(A.Multiply(x0)), -1);
                z = r.Clone();
            }
            
            
            dotproduct_rr = r.DotProduct(r);
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
                Az = Factorizer.UTransposeSolve(At.Multiply(Factorizer.LTransposeSolve(Factorizer.LSolve(A.Multiply(Factorizer.USolve(z)))))); ;
            }
            else
            {
                Az = At.Multiply(A.Multiply(z));
            }

            coefficient = dotproduct_rr / Az.DotProduct(z);

            if (Double.IsInfinity(coefficient) || Double.IsNaN(coefficient))
            {
                residual = -1;
                return;
            }

            xk = xk.Add(z, coefficient);
            r = r.Add(Az, -coefficient);
            coefficient = dotproduct_rr;
            dotproduct_rr = r.DotProduct(r);
            coefficient = dotproduct_rr / coefficient;

            if (Double.IsInfinity(coefficient) || Double.IsNaN(coefficient))
            {
                residual = -1;
                return;
            }

            z = r.Add(z, coefficient);
            residual = Math.Sqrt(dotproduct_rr) / norm_b;
        }
    }
}
