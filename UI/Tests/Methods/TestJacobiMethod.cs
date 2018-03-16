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
using System.Diagnostics;

namespace Methods
{
    // TODO соглашение об именовании

    public class TestJacobiMethod
    {
        private readonly ITestOutputHelper _testOutputHelper;
        IMethod Method;
        ILogger Logger;
        private double[,] _matrix;
        LoggingSolver loggingSolver;

        public TestJacobiMethod(ITestOutputHelper testOutputHelper)
        {
            Method = new JacobiMethod();
            Logger = new FakeLog();
            loggingSolver = new LoggingSolver(Method, Logger);
            _testOutputHelper = testOutputHelper;
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
            var proxyMethod = new ProxyMethod(new JacobiMethod());
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

        // очень долго, не в Unit тестах
      // [Fact]
      // public void SpeedTest()
      // {
      //     // несколько итераций
      //
      //     DenseMatrix[] denseMatrixes = new DenseMatrix[3];
      //     IVector[] resultActuals = new IVector[3];
      //     IVector[] x0 = new IVector[3];
      //     IVector[] b = new IVector[3];
      //     for (int i = 0; i < denseMatrixes.Length; i++)
      //     {
      //         int size = (int)Math.Pow(10, (i + 1));
      //         denseMatrixes[i] = DenseMatrixGen.LowCondMatrix(size);
      //         resultActuals[i] = new Vector(Enumerable.Range(0, size).Select(x => 1.0).ToArray());
      //         x0[i] = new Vector(Enumerable.Range(0, size).Select(x => 0.0).ToArray());
      //         b[i] = denseMatrixes[i].Multiply(resultActuals[i]);
      //     }
      //
      //
      //
      //     double[] measurements = new double[denseMatrixes.Length];
      //     Stopwatch stopWatch = new Stopwatch();
      //     for (int i = 0; i < denseMatrixes.Length; i++)
      //     {
      //         stopWatch.Start();
      //         var result = loggingSolver.Solve(denseMatrixes[i], x0[i], b[i]);
      //         stopWatch.Stop();
      //         TimeSpan ts = stopWatch.Elapsed;
      //         string elapsedTime = ts.Minutes.ToString() + " " + 
      //                              ts.Seconds.ToString() + " " +
      //                              (ts.Milliseconds / 10).ToString();
      //         _testOutputHelper.WriteLine(elapsedTime);
      //     }
      // }

        // ...

    }
}
