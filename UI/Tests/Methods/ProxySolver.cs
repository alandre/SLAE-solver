using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolverCore;
using SolverCore.Solvers;
using SolverCore.Methods;
using System.Collections;

using Tests.Extensions;

namespace Methods
{
    public class ProxySolver :  ISolver
    {
        LoggingSolver loggingSolver;
        public ProxySolver(IMethod method, ILogger logger)
        {
            loggingSolver = new LoggingSolver(method, logger);
        }

        public IVector Solve(ILinearOperator A, IVector x0, IVector b, int maxIter = 100000, double eps = 1E-14, bool malloc = false)
        {
            return loggingSolver.Solve(A, x0, b, maxIter, eps, malloc);
        }
    }

    public class ProxyMethod : IMethod
    {
        IMethod method;
        public List<int> listofCount = new List<int>();

        public ProxyMethod(IMethod method)
        {
            this.method = method;
        }

        public IVector InitMethod(ILinearOperator A, IVector x0, IVector b, bool malloc = false)
        {
            Counters.Mult.ResetCount();
            var result = method.InitMethod(A, x0, b, false);
            listofCount.Add(Counters.Mult.count);
            return result;
        }

        public void MakeStep(out int iter, out double residual)
        {
            Counters.Mult.ResetCount();
            method.MakeStep(out iter, out residual);
            listofCount.Add(Counters.Mult.count);
        }
    }

    // TODO фабрику, мб, какую-нибудь, для типов матриц
    // TODO отслеживание количество действий в решателях с помощью Proxy паттерна
    // TODO засекать время 

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
            return linear.LMult(x, UseDiagonal, diagonalElement);
        }

        public IVector LSolve(IVector x, bool UseDiagonal)
        {
            return linear.LSolve(x, UseDiagonal);
        }

        public IVector Multiply(IVector vector)
        {
            Counters.Mult.Inc();
            return linear.Multiply(vector);
        }

        public IVector UMult(IVector x, bool UseDiagonal, DiagonalElement diagonalElement = DiagonalElement.One)
        {
            return linear.UMult(x, UseDiagonal, diagonalElement);
        }

        public IVector USolve(IVector x, bool UseDiagonal)
        {
            return linear.USolve(x, UseDiagonal);
        }
    }

}


/*
 * using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolverCore;
using SolverCore.Solvers;
using SolverCore.Methods;
using System.Collections;

using Tests.Extensions;

namespace Methods
{
    public class ProxySolver :  ISolver
    {
        LoggingSolver loggingSolver;
        public ProxySolver(IMethod method, ILogger logger)
        {
            loggingSolver = new LoggingSolver(method, logger);
        }

        public IVector Solve(ILinearOperator A, IVector x0, IVector b, int maxIter = 100000, double eps = 1E-14, bool malloc = false)
        {
            return loggingSolver.Solve(A, x0, b, maxIter, eps, malloc);
        }
    }

    public class ProxyMethod : IMethod
    {
        IMethod method;
        public List<int> listofCount = new List<int>();

        public ProxyMethod(IMethod method)
        {
            this.method = method;
        }

        public IVector InitMethod(ILinearOperator A, IVector x0, IVector b, bool malloc = false)
        {
            var result = method.InitMethod(A, x0, b, false);
            return result;
        }

        public void MakeStep(out int iter, out double residual)
        {
            Counters.ResetHadamar();
            method.MakeStep(out iter, out residual);
            listofCount.Add(Counters.CountHadamar);
        }
    }

    // TODO фабрику, мб, какую-нибудь, для типов матриц
    // TODO отслеживание количество действий в решателях с помощью Proxy паттерна
    // TODO засекать время 


    // надо ли захерачивать IVector?
    public class ProxyVector :  Vector, IVector
    {
        IVector realVector;
        #region странная фигня
        public ProxyVector(double[] vector) : base(vector)
        {
            realVector = this as Vector;
        }

        public ProxyVector(int size) : base(size)
        {
            realVector = this as Vector;
        }

        public override bool Equals(object obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();
        #endregion

        public new double DotProduct(IVector vector)
        {
            Counters.IncDot();
            return base.DotProduct(vector);
        }
        public new IVector HadamardProduct(IVector vector)
        {
            Counters.IncHadamar();
            return base.HadamardProduct(vector);

        }
        public new IVector Add(IVector vector, double multiplier = 1)
        {
            var tmp = (base.Add(vector, multiplier)) as Vector;
            return new ProxyVector(tmp.ToArray());
        }

        public new IVector Clone()
        {
            var tmp = (base.Clone()) as Vector; // TODO мб, без as?
            return new ProxyVector(tmp.ToArray());
        }


    }
}
*/
