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
using Xunit.Abstractions;



namespace Methods
{

    public class TestBCGStabMethod
    {
        private readonly ITestOutputHelper _testOutputHelper;

        private double[,] _matrix;
        IMethod Method;
        ILogger Logger;
        LoggingSolver loggingSolver;

        public TestBCGStabMethod(ITestOutputHelper testOutputHelper)
        {
            Method = new BCGStab();
            Logger = new FakeLog();
            loggingSolver = new LoggingSolver(Method, Logger); 
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Algorithm()
        {
            _matrix = new double[3, 3] { { 3, 1, 1 },
                                         { 0, 5, 1 },
                                         { 2, 0, 3 } };

            IVector resultActual = new Vector(new double[] { 1, 1, 1 });

            DenseMatrix denseMatrix = new DenseMatrix(_matrix);
            Vector x0 = new Vector(new double[] { 0, 0, 0 });
            IVector b = denseMatrix.Multiply(resultActual);

            var result = loggingSolver.Solve(denseMatrix, x0, b);

            foreach (var elem in result)
                _testOutputHelper.WriteLine(elem.ToString());

            for (int i = 0; i < resultActual.Size; i++)
                Assert.Equal(result[i], resultActual[i], 8);

        }

        [Fact]
        public void AlgorithmDiag()
        {
            _matrix = new double[3, 3] { { 2, 0, 0 },
                                         { 0, 2, 0 },
                                         { 1, 0, 2} };

            IVector resultActual = new Vector(new double[] { 1, 1, 1 });

            DenseMatrix denseMatrix = DenseMatrixGen.DiagonalMatrix(3);// new DenseMatrix(_matrix);
            Vector x0 = new Vector(new double[] { 0, 0, 0 });
            IVector b = denseMatrix.Multiply(resultActual);

            var result = loggingSolver.Solve(denseMatrix, x0, b);

            foreach (var elem in result)
                _testOutputHelper.WriteLine(elem.ToString());

            for (int i = 0; i < resultActual.Size; i++)
                Assert.Equal(result[i], resultActual[i], 8);

        }



        [Fact]
        public void NotDiagonallyDominant()
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
        public void AlgorithmCountMult()
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

            _testOutputHelper.WriteLine(MultCount[0].ToString());
            _testOutputHelper.WriteLine(MultCount[1].ToString());

            for (int i = 0; i < resultActual.Size; i++)
                Assert.Equal(result[i], resultActual[i], 8);

        }

        [Fact]
        public void PositiveDefiniteMatrix()
        {
            _matrix = new double[5, 5] { { 3, 1, 0, 7, 5 },
                                         { 0, 6, 3, 6, 0 },
                                         { 9, 0, 7, 0, 0 },
                                         { 8, 3, 4, 2, 0 },
                                         { 1, 2, 0, 1, 5 }};

            IVector resultActual = new Vector(new double[] { 1, 1, 1, 1, 1 });

            DenseMatrix denseMatrix = new DenseMatrix(_matrix);
            Vector x0 = new Vector(new double[] { 0, 0, 0, 0, 0 });
            IVector b = denseMatrix.Multiply(resultActual);

            var result = loggingSolver.Solve(denseMatrix, x0, b);

            foreach (var elem in result)
                _testOutputHelper.WriteLine(elem.ToString());

            Assert.NotEmpty(result);
        }


        [Fact]
        public void NegativeDefiniteMatrix()
        {
            _matrix = new double[5, 5] { { -3, -1, 0, -7, -5 },
                                         { 0, -6, -3, -6, 0 },
                                         { -9, 0, 7, 0, 0 },
                                         { -8, -3, -4, -2, 0 },
                                         {1, 2, 0, 1, -3 }};

            IVector resultActual = new Vector(new double[] { 1, 1, 1, 1, 1});

            DenseMatrix denseMatrix = new DenseMatrix(_matrix);
            Vector x0 = new Vector(new double[] { 0, 0, 0, 0, 0 });
            IVector b = denseMatrix.Multiply(resultActual);

            var result = loggingSolver.Solve(denseMatrix, x0, b);

            foreach (var elem in result)
                _testOutputHelper.WriteLine(elem.ToString());

            Assert.NotEmpty(result);
        }

        [Fact]
        public void SingularMatrix()
        {
            IVector resultActual = new Vector(new double[] { 1, 1, 1 });

            DenseMatrix denseMatrix = DenseMatrixGen.SingularMatrix(3);
            Vector x0 = new Vector(new double[] { 0, 0, 0 });
            IVector b = denseMatrix.Multiply(resultActual);

            var result = loggingSolver.Solve(denseMatrix, x0, b);

            foreach (var elem in result)
                _testOutputHelper.WriteLine(elem.ToString());

            Assert.NotEmpty(result);
        }
    }
}
