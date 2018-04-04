using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolverCore.Factorizations;


namespace SolverCore
{
    public enum FactorizersEnum { WithoutFactorization, IncompleteCholesky, IncompleteLU, IncompleteLUsq, DiagonalFactorization, SimpleFactorization }
    public class FactorizersFactory
    {
        public static Dictionary<string, FactorizersEnum> FactorizersSimDictionary { get; } = new Dictionary<string, FactorizersEnum>
        {
            {"Без факторизации",FactorizersEnum.WithoutFactorization},
            {"Неполный Холецкий", FactorizersEnum.IncompleteCholesky},
            {"Неполный LU", FactorizersEnum.IncompleteLU},
            {"Неполный LUsq", FactorizersEnum.IncompleteLUsq},
            {"Диагональная", FactorizersEnum.DiagonalFactorization},
            {"Простая", FactorizersEnum.SimpleFactorization}



        };
        public static Dictionary<string, FactorizersEnum> FactorizersDictionary { get; } = new Dictionary<string, FactorizersEnum>
        {
            {"Без факторизации",FactorizersEnum.WithoutFactorization},
            //{"Неполный Холецкий", FactorizersEnum.IncompleteCholesky},
            {"Неполный LU", FactorizersEnum.IncompleteLU},
            {"Неполный LUsq", FactorizersEnum.IncompleteLUsq},
            {"Диагональная", FactorizersEnum.DiagonalFactorization},
            {"Простая", FactorizersEnum.SimpleFactorization}



        };
        public static IFactorization SpawnFactorization(FactorizersEnum factorizer, CoordinationalMatrix M)
        {
            IFactorization factorization;
            switch (factorizer)

            {
                case FactorizersEnum.IncompleteCholesky:
                    return factorization = new IncompleteCholesky(M);
                case FactorizersEnum.IncompleteLU:
                    return factorization = new IncompleteLU(M);
                case FactorizersEnum.IncompleteLUsq:
                    return factorization = new IncompleteLUsq(M); 
                case FactorizersEnum.DiagonalFactorization:
                    return factorization = new DioganalFactorization(M); 
                case FactorizersEnum.SimpleFactorization:
                    return factorization = new SimpleFactorization(M); 
                case FactorizersEnum.WithoutFactorization:
                    return factorization = null; 
                default: return null;
            }
        }

    }
}
