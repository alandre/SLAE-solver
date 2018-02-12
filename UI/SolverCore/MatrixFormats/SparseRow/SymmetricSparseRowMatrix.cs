using System;
using System.Collections;

namespace SolverCore
{
    public class SymmetricSparseRowMatrix : IMatrix, ILinearOperator
    {
        public double this[int i, int j] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int Size => throw new NotImplementedException();

        public IVector Diagonal => throw new NotImplementedException();

        public ILinearOperator Transpose => throw new NotImplementedException();

        public System.Collections.Generic.IEnumerator<(double value, int row, int col)> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public IVector LMult(IVector x, bool UseDiagonal)
        {
            throw new NotImplementedException();
        }

        public IVector LSolve(IVector x, bool UseDiagonal)
        {
            throw new NotImplementedException();
        }

        public IVector Multiply(IVector x)
        {
            throw new NotImplementedException();
        }

        public IVector UMult(IVector x, bool UseDiagonal)
        {
            throw new NotImplementedException();
        }

        public IVector USolve(IVector x, bool UseDiagonal)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
