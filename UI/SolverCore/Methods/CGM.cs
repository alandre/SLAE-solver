﻿using System;

namespace SolverCore.Methods
{
    public class CGM : IMethod
    {
        IVector x0, b, z, r, Az;
        ILinearOperator A, At;
        double norm_b, dotproduct_rr, coefficient;
        int currentIter;
        bool init;
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
            this.x0 = x0;
            this.b = b;
            this.A = A;
            At = A.Transpose;
            norm_b = b.Norm;
            currentIter = 0;
            //r = b.Add(A.Multiply(x0), -1);
            r = At.Multiply(b).Add(At.Multiply(A.Multiply(x0)), -1);
            z = r.Clone();
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

            // Az = A.Multiply(z);
            Az = At.Multiply(A.Multiply(z));
            coefficient = dotproduct_rr / Az.DotProduct(z);
            if (Double.IsInfinity(coefficient) || Double.IsNaN(coefficient))
            {
                residual = -1;
                return;
            }
            x = x.Add(z, coefficient);
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
