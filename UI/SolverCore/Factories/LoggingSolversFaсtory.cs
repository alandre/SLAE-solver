using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolverCore.Solvers;
using SolverCore.Methods;
using SolverCore.Factorizations;


namespace SolverCore
{
    public enum MethodsEnum { CGM, GaussianSeidel, Jacobi, BCGStab, LOS }
    public enum FactorizersEnum { WithoutFactorization, IncompleteCholesky, IncompleteLU, IncompleteLUsq , DiagonalFactorization, SimpleFactorization }

    public class LoggingSolversFabric
    {

        public static Dictionary<string, MethodsEnum> MethodsDictionary { get; } = new Dictionary<string, MethodsEnum>
        {
            {"CGM",MethodsEnum.CGM},
            {"GaussianSeidel", MethodsEnum.GaussianSeidel},
            {"Jacobi", MethodsEnum.Jacobi},
            {"LOS", MethodsEnum.LOS},
            {"BCGStab", MethodsEnum.BCGStab}


        };
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
        /// <summary>
        /// Создаёт решатель для метода с указанным логером
        /// </summary>
        /// <param name="type">Метод</param>
        /// <param name="logger">Логер</param>
        /// <param name="factorizer">Разложение</param>
        /// <param name="krylovSubspaceDimension">Размерность подпространства Крылова</param>
        /// <returns></returns>
        public static ISolver Spawn(MethodsEnum type, ILogger logger, CoordinationalMatrix M, FactorizersEnum factorizer=FactorizersEnum.WithoutFactorization, int krylovSubspaceDimension = 4)
        {
            IMethod method;
            IFactorization factorization;
             switch (factorizer)
            
            {
                case FactorizersEnum.IncompleteCholesky:
                    factorization = new IncompleteCholesky(M); break;
                case FactorizersEnum.IncompleteLU:
                    factorization = new IncompleteLU(M); break;
                case FactorizersEnum.IncompleteLUsq:
                    factorization = new IncompleteLUsq(M); break;
                case FactorizersEnum.DiagonalFactorization:
                    factorization = new DioganalFactorization(M); break;
                case FactorizersEnum.SimpleFactorization:
                    factorization = new SimpleFactorization(M); break;
               
                case FactorizersEnum.WithoutFactorization:
                    factorization = null; break;
                default: return null;
            }
            switch (type)
            {
                case MethodsEnum.CGM: method = new CGM(); break;
                case MethodsEnum.GaussianSeidel: method = new GaussianSeidelMethod(); break;
                case MethodsEnum.Jacobi: method = new JacobiMethod(); break;
                case MethodsEnum.LOS: method = new LOS(); break;
                case MethodsEnum.BCGStab: method = new BCGStab(); break;
                default: return null;
            }
           
        
            
            return new LoggingSolver(method, logger);
        }

    }
}
