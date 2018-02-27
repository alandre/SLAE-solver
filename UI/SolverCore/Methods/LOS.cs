﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverCore.Methods
{
    class LOS : IMethod
    {
        IVector x, x0, b, p, z, r, Ar;
        ILinearOperator A;
        double norm_b,dotproduct_pp, coefficient;
        int currentIter;
        bool init;

        LOS()
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
            p = A.Multiply(r);
            dotproduct_pp = p.DotProduct(p);
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
            //???
            try
            {
                coefficient = p.DotProduct(r) / dotproduct_pp;
            }
            catch (DivideByZeroException)
            {
                residual = -1;
                return;
            }
            x = x.Add(z, coefficient);
            r = r.Add(p, -coefficient);
            Ar = A.Multiply(r);
            coefficient = -p.DotProduct(Ar) / dotproduct_pp;
            z = r.Add(z, coefficient);
            p = Ar.Add(p, coefficient);      
            dotproduct_pp = p.DotProduct(p);

            residual = r.Norm/norm_b;
            
        }
    }
}