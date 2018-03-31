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
        /// <summary>
        /// Создаёт решатель для метода с указанным логером
        /// </summary>
        /// <param name="type">Метод</param>
        /// <param name="logger">Логер</param>
        /// <param name="factorizer">Разложение</param>
        /// <param name="krylovSubspaceDimension">Размерность подпространства Крылова</param>
        /// <returns></returns>
        public static ISolver Spawn(MethodsEnum type, ILogger logger, IFactorization factorizer = null, int krylovSubspaceDimension = 4)
        {
            IMethod method;
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
