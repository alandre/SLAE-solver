using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolverCore;
using SolverCore.Solvers;
using SolverCore.Methods;
using Extensions;

using Xunit;





namespace Methods
{
    // TODO соглашение об именовании

    /*public class TestBCGStabMethod
    {
       
        IMethod Method;
        ILogger Logger;
        private double[,] _matrix;
        LoggingSolver loggingSolver;

        public TestBCGStabMethod()
        {
            Method = new BCGStab();
            Logger = new FakeLog();
            loggingSolver = new LoggingSolver(Method, Logger); 
        }

        [Fact]
        public void TestAlgorithm()
        {
            _matrix = new double[3, 3] { { 3, 1, 1 },
                                         { 0, 5, 1 },
                                         { 2, 0, 3 } };

            IVector resultActual = new Vector(new double[] { 1, 1, 1 });

            DenseMatrix denseMatrix = new DenseMatrix(_matrix);
            Vector x0 = new Vector(new double[] { 0, 0, 0 });
            IVector b = denseMatrix.Multiply(resultActual);

            var result = loggingSolver.Solve(denseMatrix, x0, b);

            for (int i = 0; i < resultActual.Size; i++)
                Assert.Equal(result[i], resultActual[i], 8);

        }

       

        [Fact]
        public void TestNotDiagonallyDominant()
        {
            _matrix = new double[3, 3] { { 3, 1, 1 },
                                         { 0, 5, 1 },
                                         { 80, 0, 3 } };
        
            IVector resultActual = new Vector(new double[] { 1, 1, 1 });
        
            DenseMatrix denseMatrix = new DenseMatrix(_matrix);
            Vector x0 = new Vector(new double[] { 0, 0, 0 });
            IVector b = denseMatrix.Multiply(resultActual);
        
            var result = loggingSolver.Solve(denseMatrix, x0, b);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void TestAlgorithmCountMult()
        {
            var proxyMethod = new ProxyMethod(new BCGStab());
            loggingSolver = new LoggingSolver(proxyMethod, Logger);
            double[,] _matrix = new double[3, 3] { { 3, 1, 1 },
                                                   { 0, 5, 1 },
                                                   { 2, 0, 3 } };

            IVector resultActual = new Vector(new double[] { 1, 1, 1 });

            DenseMatrix denseMatrix = new DenseMatrix(_matrix);
            ProxyMatrix proxyMatrix = new ProxyMatrix(denseMatrix);

            Vector x0 = new Vector(new double[] { 0, 0, 0 });
            IVector b = denseMatrix.Multiply(resultActual);

            var result = loggingSolver.Solve(proxyMatrix, x0, b);
            var MultCount = proxyMethod.MultCount;

            // сравнивать количество вызовов

            for (int i = 0; i < resultActual.Size; i++)
                Assert.Equal(result[i], resultActual[i], 8);

        }
        // ...

    }*/
}
