using System;
using Xunit;
using SolverCore;
using System.Collections.Generic;
using Xunit.Abstractions;

namespace MF.Dense
{
    public class TestsDenseMatrix
    {
        private double[,] _matrix;
        private DenseMatrix denseMatrix;
        private readonly ITestOutputHelper _testOutputHelper;

        public TestsDenseMatrix(ITestOutputHelper testOutputHelper)
        {
            _matrix = new double[3, 3] { { 1, 3, 5 }, { 2, 5, 4 }, { 7, 1, 8 } };
            denseMatrix = new DenseMatrix(_matrix);

            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Foreach()
        {
            //di = new double[] { 1, 2, 3 };
            //al = new double[] { 1, 2, 3 };
            //au = new double[] { 3, 2, 1 };
            //ia = new int[] { 1, 1, 2, 4 };
            // 1 3 2
            // 1 2 1 
            // 2 3 3

            List<(double, int, int)> elemList =
                new List<(double, int, int)>()
                {
                    (1,0,0),
                    (3,0,1),
                    (5,0,2),
                    (2,1,0),
                    (5,1,1),
                    (4,1,2),
                    (7,2,0),
                    (1,2,1),
                    (8,2,2),
                };


            Assert.True(new HashSet<(double, int, int)>(denseMatrix).SetEquals(elemList));

            foreach (var elem in denseMatrix)
                _testOutputHelper.WriteLine(elem.ToString());
        }

        [Fact]
        public void ConstructorExeptions()
        {
            _matrix = new double[2, 3] { { 1, 3, 5 }, { 2, 5, 4 } };
            double[,] nullArray = null;

            Assert.Throws<ArgumentNullException>(() => { DenseMatrix denseMatrix = new DenseMatrix(nullArray); });
            Assert.Throws<ArgumentException>(() => { DenseMatrix denseMatrix = new DenseMatrix(_matrix); });
            Assert.Throws<ArgumentException>(() => { DenseMatrix denseMatrix2 = new DenseMatrix(-1); }); 
        }

        [Fact]
        public void Indexator()
        {
            Assert.Throws<IndexOutOfRangeException>(() =>  denseMatrix[4,2]);
            Assert.Equal(1.0,denseMatrix[2,1]);
        }

        [Fact]
        public void DenseMatrix_Foreach()
        {
            var res = denseMatrix.Multiply(new Vector(new double[] { 1, 1, 1 }));
            Vector resVector = new Vector(new double[] { 9, 11, 16 });

            int i = 0;
            foreach (var el in res)
            {
                Assert.Equal(el, resVector[i]);
                i++;
            }
        }

        [Fact]
        public void LMultExeptions()
        {
            Vector exampleVector = new Vector(new double[2] { 1.0, 2.0 });

            Assert.Throws<ArgumentNullException>(() => denseMatrix.LMult(null,false, DiagonalElement.One));
            Assert.Throws<RankException>(() => denseMatrix.LMult(exampleVector, false, DiagonalElement.One));
        }
     
        [Fact]
        public void LMult()
        {
            Vector vector = new Vector(new double[] { 1, 3, 8 });

            var resultTrueDiag = denseMatrix.LMult(vector, true);
            Vector resultActualTrueDiag = new Vector(new double[] { 1, 17, 74 });

            var resultFalseDiag = denseMatrix.LMult(vector, false);
            Vector resultActualFalseDiag = new Vector(new double[] { 1, 5, 18 });

            for (int i = 0; i < resultTrueDiag.Size; i++)
            {
                Assert.Equal(resultTrueDiag[i], resultActualTrueDiag[i], 8);
                Assert.Equal(resultFalseDiag[i], resultActualFalseDiag[i], 8);
            }
        }

        [Fact]
        public void UMultExeptions()
        {
            Vector exampleVector = new Vector(new double[2] { 1.0, 2.0 });

            Assert.Throws<ArgumentNullException>(() => denseMatrix.UMult(null, false, DiagonalElement.One));
            Assert.Throws<RankException>(() => denseMatrix.UMult(exampleVector, false, DiagonalElement.One));
        }

        [Fact]
        public void UMult()
        {
            Vector vector = new Vector(new double[] { 1, 3, 8 });

            var resultFalseDiag = denseMatrix.UMult(vector, false);
            Vector resultActualFalseDiag = new Vector(new double[] { 50, 35, 8 });

            var resultTrueDiag = denseMatrix.UMult(vector, true);
            Vector resultActualTrueDiag = new Vector(new double[] { 50, 47, 64 });

            for (int i = 0; i < resultFalseDiag.Size; i++)
            {
                Assert.Equal(resultFalseDiag[i], resultActualFalseDiag[i], 8);
                Assert.Equal(resultTrueDiag[i], resultActualTrueDiag[i], 8);
            }
        }

        [Fact]
        public void LSolveExeptions()
        {
            Vector exampleVector = new Vector(new double[2] { 1.0, 2.0 });

            Assert.Throws<ArgumentNullException>(() => denseMatrix.LSolve(null, false));
            Assert.Throws<RankException>(() => denseMatrix.LSolve(exampleVector, false));
        }

        [Fact]
        public void LSolve()
        {
            _matrix = new double[3, 3] { { 1, 2, 3 }, { 2, -1, 1 }, { 7, -20, 93 } };
            DenseMatrix denseMatrix = new DenseMatrix(_matrix);

            Vector vector = new Vector(new double[] { 1, 2, 3 });

            var result = denseMatrix.LSolve(vector, true);
            Vector resultActual = new Vector(new double[] { 1, 0, -0.04301075 });

            for (int i = 0; i < result.Size; i++)
                Assert.Equal(result[i], resultActual[i], 8);
        }

        [Fact]
        public void USolveExeptions()
        {
            _matrix = new double[3, 3] { { 1, 3, 5 }, { 2, 5, 4 }, { 7, 1, 8 } };
            DenseMatrix denseMatrix = new DenseMatrix(_matrix);

            Vector exampleVector = new Vector(new double[2] { 1.0, 2.0 });

            Assert.Throws<ArgumentNullException>(() => denseMatrix.USolve(null, false));
            Assert.Throws<RankException>(() => denseMatrix.USolve(exampleVector, false));
        }

        [Fact]
        public void USolve()
        {
            _matrix = new double[3, 3] { { 1, 3, 5 }, { 1, 1, 6 }, { 4, 2, 1 } };
            DenseMatrix denseMatrix = new DenseMatrix(_matrix);

            Vector vector = new Vector(new double[] { 1, 0, -0.04301075 });

            var result = denseMatrix.USolve(vector, false);
            Vector resultActual = new Vector(new double[] { 0.44086025, 0.2580645, -0.04301075 });
            int[] precisionVector = new int[] { 2, 2, 3 };

            for (int i = 0; i < result.Size; i++)
                Assert.Equal(result[i], resultActual[i], 8);
        }

        [Fact]
        public void MultyplyExceptions()
        {
            Vector vector = new Vector(new double[] { 1, 0});

            Assert.Throws<ArgumentNullException>(() => denseMatrix.Multiply(null));
            Assert.Throws<RankException>(() => denseMatrix.Multiply(vector));
        }

        [Fact]
        public void Fill()
        {
            FillFunc fillFunc = (row, col) => { return (row + 1) + (col + 1); }; 

            denseMatrix.Fill(fillFunc);
            _matrix = new double[3, 3] 
            {
                { 2, 3, 4 },
                { 3, 4, 5 },
                { 4, 5, 6 }
            };
            DenseMatrix dense = new DenseMatrix(_matrix);
            Assert.True(new HashSet<(double, int, int)>(denseMatrix).SetEquals(dense));

        }
    }
}
