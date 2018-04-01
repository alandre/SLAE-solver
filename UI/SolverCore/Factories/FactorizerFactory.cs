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
        public static Dictionary<string, FactorizersEnum> FactorizersDictionary { get; } = new Dictionary<string, FactorizersEnum>
        {
            {"Без факторизации",FactorizersEnum.WithoutFactorization},
            //{"Неполный Холецкий", FactorizersEnum.IncompleteCholesky},
            {"Неполный LU", FactorizersEnum.IncompleteLU},
            {"Неполный LUsq", FactorizersEnum.IncompleteLUsq},
            //{"Диагональная", FactorizersEnum.DiagonalFactorization},
            //{"Простая", FactorizersEnum.SimpleFactorization}



        };
        public static Dictionary<string, FactorizersEnum> FactorizersSimDictionary { get; } = new Dictionary<string, FactorizersEnum>
        {
            {"Без факторизации",FactorizersEnum.WithoutFactorization},
            {"Неполный Холецкий", FactorizersEnum.IncompleteCholesky},
            {"Неполный LU", FactorizersEnum.IncompleteLU},
            {"Неполный LUsq", FactorizersEnum.IncompleteLUsq},
            //{"Диагональная", FactorizersEnum.DiagonalFactorization},
            //{"Простая", FactorizersEnum.SimpleFactorization}



        };
        public enum FactorizersEnum { WithoutFactorization, IncompleteCholesky, IncompleteLU, IncompleteLUsq }//, DiagonalFactorization, SimpleFactorization }
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
              //  case FactorizersEnum.DiagonalFactorization:
                //    return IncompleteLUsq.IncompleteLUsqMethod(matrix.ConvertToCoordinationalMatrix());
              //  case FactorizersEnum.SimpleFactorization:
                //    return IncompleteLUsq.IncompleteLUsqMethod(matrix.ConvertToCoordinationalMatrix());
                case FactorizersEnum.WithoutFactorization:
                    return matrix;
                default: return null;
            }
        }
    }
}
