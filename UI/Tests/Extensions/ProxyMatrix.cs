using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolverCore;
using SolverCore.Solvers;
using SolverCore.Methods;
using System.Collections;


namespace Extensions
{

    public class ProxyMatrix : ILinearOperator
    {
        ILinearOperator linear;
        public ProxyMatrix(ILinearOperator linearOperator)
        {
            linear = linearOperator;
        }

        public int Size => linear.Size;

        public IVector Diagonal => linear.Diagonal;

        public ILinearOperator Transpose => linear.Transpose;

        public IVector LMult(IVector x, bool UseDiagonal, DiagonalElement diagonalElement = DiagonalElement.One)
        {
            Counters.Inc("LMult");
            return linear.LMult(x, UseDiagonal, diagonalElement);
        }

        public IVector LSolve(IVector x, bool UseDiagonal)
        {
            return linear.LSolve(x, UseDiagonal);
        }

        public IVector Multiply(IVector vector)
        {
            Counters.Inc("Mult");
            return linear.Multiply(vector);
        }

        public IVector UMult(IVector x, bool UseDiagonal, DiagonalElement diagonalElement = DiagonalElement.One)
        {
            Counters.Inc("UMult");
            return linear.UMult(x, UseDiagonal, diagonalElement);
        }

        public IVector USolve(IVector x, bool UseDiagonal)
        {
            return linear.USolve(x, UseDiagonal);
        }
    }

}



