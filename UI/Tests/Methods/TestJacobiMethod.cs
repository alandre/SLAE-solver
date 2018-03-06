using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolverCore;
using SolverCore.Solvers;
using SolverCore.Methods;

using Xunit;





namespace Methods
{
    internal class FakeLog : ILogger
    {
        public void read()
        {
            return;
        }

        public void write()
        {
            return;
        }
    }

    public class TestJacobiMethod
    {
       
        IMethod Method;
        ILogger Logger;
        private double[,] _matrix;
        LoggingSolver loggingSolver;

        public TestJacobiMethod()
        {
            Method = new JacobiMethod();
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
        
        // ...

    }
}
