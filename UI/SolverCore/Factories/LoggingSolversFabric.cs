using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolverCore.Solvers;
using SolverCore.Methods;

namespace SolverCore
{
   // public enum MethodsEnum { CG, GaussianSeidel, Jacobi, LOS, BCGStab, GMRes }
    public enum MethodsEnum { CG,GaussianSeidel, Jacobi, LOS, BCGStab}

    public class LoggingSolversFabric
    {
        static List<String> Types = null;
        public LoggingSolversFabric()
        {
            if (Types == null)
            {
                Types = new List<string>();
                foreach (var value in Enum.GetValues(typeof(MethodsEnum)))
                {
                    Types.Add(value.ToString());
                }
            }

        }

        /// <summary>
        /// Создаёт решатель для метода с указанным логером
        /// </summary>
        /// <param name="type">Метод</param>
        /// <param name="Logger">Логер</param>
        /// <param name="KrylovSubspaceDimension">Размерность подпространства Крылова</param>
        /// <returns></returns>
        public static ISolver Spawn(MethodsEnum type, ILogger Logger, int KrylovSubspaceDimension = 4)
        {
            IMethod method = null;
            switch (type)
            {
                case MethodsEnum.CG: method = new CGM(); break;
                case MethodsEnum.GaussianSeidel: method = new GaussianSeidelMethod(); break;
                case MethodsEnum.Jacobi: method = new JacobiMethod(); break;
                case MethodsEnum.LOS: method = new LOS(); break;
                case MethodsEnum.BCGStab: method = new BCGStab(); break;

                default: return null;
            }
            
            return new LoggingSolver(method, Logger);
        }

        /// <summary>
        /// Создаёт решатель для метода с указанным логером
        /// </summary>
        /// <param name="type">Метод</param>
        /// <param name="Logger">Логер</param>
        /// <param name="KrylovSubspaceDimension">Размерность подпространства Крылова</param>
        /// <returns></returns>
        public static ISolver Spawn(String type, ILogger Logger, int KrylovSubspaceDimension = 4)
        {
            try
            {
                MethodsEnum method = (MethodsEnum)Enum.Parse(typeof(MethodsEnum), type);
                if (Enum.IsDefined(typeof(MethodsEnum), method))
                {
                    return Spawn(method, Logger, KrylovSubspaceDimension);
                }
                else return null;
            }
            catch (ArgumentException)
            {
                return null;
            }
        }

        public List<String> MethodTypes
        {
            get => Types;
        }
    }
}
