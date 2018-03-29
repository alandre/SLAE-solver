using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolverCore;
using SolverCore.Factorizations;


namespace UI
{
    public enum FactorizersEnum { IncompleteCholesky = 0, IncompleteLU = 1, IncompleteLUsq = 2 }
    public class FactorizerFactory
    {
        //public enum Formats { Coordinational = 0, Dense = 1, Skyline = 2, SparseRow = 3, SparseRowColumn = 4 }
        public static IMatrix Factorize_it(FactorizersEnum type, CoordinationalMatrix M)
        {
            switch (type)
            {
                case FactorizersEnum.IncompleteCholesky:new IncompleteCholesky(M); break;
                case FactorizersEnum.IncompleteLU: new IncompleteLU(M); break;
                case FactorizersEnum.IncompleteLUsq: new IncompleteLUsq(M); break;
                default: return null;
            }
            //сборки ради
            return null;

        }
    }
}
