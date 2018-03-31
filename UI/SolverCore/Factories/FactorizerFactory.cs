using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolverCore.Factorizations;

namespace SolverCore
{
    public class FactorizerFactory
    {
        public enum FactorizersEnum { IncompleteCholesky = 0, IncompleteLU = 1, IncompleteLUsq = 2, WithoutFactorization = 3 }
        public static IMatrix Factorize_it(FactorizersEnum type, IMatrix matrix)
        {
            switch (type)
            {
                case FactorizersEnum.IncompleteCholesky:
                    return IncompleteCholesky.IncompleteCholeskyMethod(matrix.ConvertToCoordinationalMatrix());
                case FactorizersEnum.IncompleteLU:
                    return IncompleteLU.IncompleteLUMethod(matrix.ConvertToCoordinationalMatrix());
                case FactorizersEnum.IncompleteLUsq:
                    return IncompleteLUsq.IncompleteLUsqMethod(matrix.ConvertToCoordinationalMatrix());
                case FactorizersEnum.WithoutFactorization:
                    return matrix;
                default: return null;
            }
        }
    }
}
