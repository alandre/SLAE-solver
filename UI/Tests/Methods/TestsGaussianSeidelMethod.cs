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
    public class TestsGaussianSeidelMethod
    {
        private readonly ITestOutputHelper _testOutputHelper;
        IMethod Method;
        ILogger Logger;
        private double[,] _matrix;
        LoggingSolver loggingSolver;

        public TestsGaussianSeidelMethod(ITestOutputHelper testOutputHelper)
        {
            Method = new GaussianSeidelMethod();
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

            Assert.NotEmpty(result);

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
        public void AlgorithmCountMult()
        {
            var proxyMethod = new ProxyMethod(new GaussianSeidelMethod());
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

        [Theory]
        [InlineData(FactorizersEnum.IncompleteLU)]
        [InlineData(FactorizersEnum.IncompleteLUsq)]
        [InlineData(FactorizersEnum.SimpleFactorization)]
        public void FactorizeMatrix(FactorizersEnum factorizers)
        {
            //_matrix = new double[3, 3] { { 3, 1, 1 }, // несимметричная
            //                             { 0, 5, 1 },
            //                             { 2, 0, 3 } };
            IVector resultActual = new Vector(new double[] { 1, 1, 1, 1, 1 });

            DenseMatrix denseMatrix = DenseMatrixGen.LowCondMatrix(5);// new DenseMatrix(_matrix);
            Vector x0 = new Vector(new double[] { 0, 0, 0, 0, 0 });
            IVector b = denseMatrix.Multiply(resultActual);

            var result = loggingSolver.Solve(denseMatrix, x0, b, Factorizer: FactorizersFactory.SpawnFactorization(factorizers, denseMatrix.ConvertToCoordinationalMatrix()));

            for (int i = 0; i < 5; i++)
                _testOutputHelper.WriteLine(
                    denseMatrix[i, 0].ToString() + " " +
                    denseMatrix[i, 1].ToString() + " " +
                    denseMatrix[i, 2].ToString() + " " +
                    denseMatrix[i, 3].ToString() + " " +
                    denseMatrix[i, 4].ToString());


            _testOutputHelper.WriteLine("");

            foreach (var elem in result)
                _testOutputHelper.WriteLine(elem.ToString());

            for (int i = 0; i < resultActual.Size; i++)
                Assert.Equal(result[i], resultActual[i], 8);
        }

        [Theory]
        [InlineData(FactorizersEnum.IncompleteLU)]
        [InlineData(FactorizersEnum.IncompleteLUsq)]
        [InlineData(FactorizersEnum.IncompleteCholesky)]
        [InlineData(FactorizersEnum.SimpleFactorization)]
        public void FactorizeSymmetricMatrix(FactorizersEnum factorizers)
        {
            //_matrix = new double[3, 3] { { 3, 0, 2 }, // симметричная
            //                             { 0, 5, 0 },
            //                             { 2, 0, 3 } };
            IVector resultActual = new Vector(new double[] { 1, 1, 1, 1, 1 });

            DenseMatrix denseMatrix = DenseMatrixGen.SymmetricMatrix(5);//   new DenseMatrix(_matrix);
            Vector x0 = new Vector(new double[] { 0, 0, 0, 0, 0 });
            IVector b = denseMatrix.Multiply(resultActual);

            var result = loggingSolver.Solve(denseMatrix, x0, b, Factorizer: FactorizersFactory.SpawnFactorization(factorizers, denseMatrix.ConvertToCoordinationalMatrix()));

            for (int i = 0; i < 5; i++)
                _testOutputHelper.WriteLine(
                    denseMatrix[i, 0].ToString() + " " +
                    denseMatrix[i, 1].ToString() + " " +
                    denseMatrix[i, 2].ToString() + " " +
                    denseMatrix[i, 3].ToString() + " " +
                    denseMatrix[i, 4].ToString());


            _testOutputHelper.WriteLine("");

            foreach (var elem in result)
                _testOutputHelper.WriteLine(elem.ToString());

            for (int i = 0; i < resultActual.Size; i++)
                Assert.Equal(result[i], resultActual[i], 8);
        }

        [Theory]
        [InlineData(FactorizersEnum.IncompleteLU)]
        [InlineData(FactorizersEnum.IncompleteLUsq)]
        [InlineData(FactorizersEnum.IncompleteCholesky)]
        [InlineData(FactorizersEnum.DiagonalFactorization)]
        [InlineData(FactorizersEnum.SimpleFactorization)]
        public void FactorizeDiagMatrix(FactorizersEnum factorizers)
        {
            //_matrix = new double[3, 3] { { 3, 0, 2 }, // симметричная
            //                             { 0, 5, 0 },
            //                             { 2, 0, 3 } };
            IVector resultActual = new Vector(new double[] { 1, 1, 1, 1, 1 });

            DenseMatrix denseMatrix = DenseMatrixGen.DiagonalMatrix(5);//   new DenseMatrix(_matrix);
            Vector x0 = new Vector(new double[] { 0, 0, 0, 0, 0 });
            IVector b = denseMatrix.Multiply(resultActual);

            var result = loggingSolver.Solve(denseMatrix, x0, b, Factorizer: FactorizersFactory.SpawnFactorization(factorizers, denseMatrix.ConvertToCoordinationalMatrix()));

            for (int i = 0; i < 5; i++)
                _testOutputHelper.WriteLine(
                    denseMatrix[i, 0].ToString() + " " +
                    denseMatrix[i, 1].ToString() + " " +
                    denseMatrix[i, 2].ToString() + " " +
                    denseMatrix[i, 3].ToString() + " " +
                    denseMatrix[i, 4].ToString());


            _testOutputHelper.WriteLine("");

            foreach (var elem in result)
                _testOutputHelper.WriteLine(elem.ToString());

            for (int i = 0; i < resultActual.Size; i++)
                Assert.Equal(result[i], resultActual[i], 8);
        }

    }
}
