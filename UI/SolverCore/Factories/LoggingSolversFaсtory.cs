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
   // public enum MethodsEnum { CG, GaussianSeidel, Jacobi, LOS, BCGStab, GMRes }
    public enum MethodsEnum { CGM, GaussianSeidel, Jacobi, LOS, BCGStab}
    public enum FactorizersEnum { IncompleteCholesky = 0, IncompleteLU = 1, IncompleteLUsq = 2 }
    public class LoggingSolversFabric
    {
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
    public class FactorizerFactory
    {
        //public enum Formats { Coordinational = 0, Dense = 1, Skyline = 2, SparseRow = 3, SparseRowColumn = 4 }
        public static IMatrix Factorize_it(FactorizersEnum type, IMatrix M)
        {
            CoordinationalMatrix M_1;
            M_1=MatrixExtensions.ConvertToCoordinationalMatrix(M);
            IMatrix factorizedMatrix;
            switch (type)
            {
                case FactorizersEnum.IncompleteCholesky: return factorizedMatrix= IncompleteCholesky.IncompleteCholeskyMethod(M_1); 
                case FactorizersEnum.IncompleteLU: return factorizedMatrix = IncompleteLU.IncompleteLUMethod(M_1);
                case FactorizersEnum.IncompleteLUsq: return factorizedMatrix = IncompleteLUsq.IncompleteLUsqMethod(M_1);
                default: return null;
            }
            //сборки ради
            return null;

        }
    }
}
