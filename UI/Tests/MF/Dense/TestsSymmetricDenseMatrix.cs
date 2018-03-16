using System;
using Xunit;
using SolverCore;
using System.Collections.Generic;

namespace MF.SymmetricDense
{
    public class TestsDenseSymmetricMatrix
    {
        private double[][] _matrix;
        private SymmetricDenseMatrix denseSymmetricMatrix;

        public TestsDenseSymmetricMatrix()
        {
            _matrix = new double[][] { new double[]{ 1 },
                                       new double[] { 2, 5 },
                                       new double[] { 2, 5, 4 } };
                denseSymmetricMatrix = new SymmetricDenseMatrix(_matrix);
        }


        [Fact]
        public void DenseSymmetricMatrix_TestConstructorExeptions()
        {
            _matrix = new double[][] { 
                                       new double[] { 2, 5 },
                                       new double[] { 2, 5, 4 } };
            

            Assert.Throws<ArgumentNullException>(() => { SymmetricDenseMatrix denseSymmetricMatrix = new SymmetricDenseMatrix((double[][])null); });      
            Assert.Throws<ArgumentException>(() => { SymmetricDenseMatrix denseMatrix2 = new SymmetricDenseMatrix(-1); });
            Assert.Throws<RankException>(() => { SymmetricDenseMatrix denseMatrix1 = new SymmetricDenseMatrix(_matrix); });
        }

        [Fact]
        public void DenseSymmetricMatrix_TestIndexator()
        {
            Assert.Throws<IndexOutOfRangeException>(() => denseSymmetricMatrix[4, 2]);
            
        }

        [Fact]
        public void DenseSymmetricMatrix_Foreach()
        {

            var matrix = new double[3, 3] { { 1, 2, 2 },
                                            { 2, 5, 5 }, 
                                            { 2, 5, 4 } };
            DenseMatrix denseMatrix = new DenseMatrix(matrix);

            Assert.True(new HashSet<(double,int,int)>(denseSymmetricMatrix).SetEquals(denseMatrix));
        }

        [Fact]
        public void DenseSymmetricMatrix_TestLMultExeptions()
        {
            Vector exampleVector = new Vector(new double[2] { 1.0, 2.0 });

            Assert.Throws<ArgumentNullException>(() => denseSymmetricMatrix.LMult(null, false, DiagonalElement.One));
            Assert.Throws<RankException>(() => denseSymmetricMatrix.LMult(exampleVector, false, DiagonalElement.One));
        }

        [Fact]
        public void DenseSymmetricMatrix_TestLMult()
        {
            Vector vector = new Vector(new double[] { 1, 3, 8 });

            var resultTrueDiag = denseSymmetricMatrix.LMult(vector, true);
            Vector resultActualTrueDiag = new Vector(new double[] { 1, 17, 49 });

            var resultFalseDiag = denseSymmetricMatrix.LMult(vector, false);
            Vector resultActualFalseDiag = new Vector(new double[] { 1, 5, 25 });

            for (int i = 0; i < resultTrueDiag.Size; i++)
            {
                Assert.Equal(resultTrueDiag[i], resultActualTrueDiag[i], 8);
                Assert.Equal(resultFalseDiag[i], resultActualFalseDiag[i], 8);
            }
        }

        [Fact]
        public void DenseSymmetricMatrix_TestUMultExeptions()
        {
            Vector exampleVector = new Vector(new double[2] { 1.0, 2.0 });

            Assert.Throws<ArgumentNullException>(() => denseSymmetricMatrix.UMult(null, false, DiagonalElement.One));
            Assert.Throws<RankException>(() => denseSymmetricMatrix.UMult(exampleVector, false, DiagonalElement.One));
        }


        [Fact]
        public void DenseSymmetricMatrix_TestUMult()
        {
            Vector vector = new Vector(new double[] { 1, 3, 8 });

            var resultFalseDiag = denseSymmetricMatrix.UMult(vector, false);
            Vector resultActualFalseDiag = new Vector(new double[] { 23, 43, 8 });

            var resultTrueDiag = denseSymmetricMatrix.UMult(vector, true);
            Vector resultActualTrueDiag = new Vector(new double[] { 23, 55, 32 });

            for (int i = 0; i < resultFalseDiag.Size; i++)
            {
                Assert.Equal(resultFalseDiag[i], resultActualFalseDiag[i], 8);
                Assert.Equal(resultTrueDiag[i], resultActualTrueDiag[i], 8);
            }
        }

        [Fact]
        public void DenseSymmetricMatrix_TestLSolveExeptions()
        {
            Vector exampleVector = new Vector(new double[2] { 1.0, 2.0 });

            Assert.Throws<ArgumentNullException>(() => denseSymmetricMatrix.LSolve(null, false));
            Assert.Throws<RankException>(() => denseSymmetricMatrix.LSolve(exampleVector, false));
        }


        [Fact]
        public void DenseSymmetricMatrix_TestLSolve()
        {
            //_matrix = new double[3, 3] { { 1, 2, 3 }, { 2, -1, 1 }, { 7, -20, 93 } };

            _matrix = new double[][] { new double[]{ 1 },
                                       new double[] { 0, 5 },
                                       new double[] { 0, 0, 32 } };

            SymmetricDenseMatrix denseSymmetricMatrix = new SymmetricDenseMatrix(_matrix);

            Vector vector = new Vector(new double[] { 1, 4, 4 });

            var result = denseSymmetricMatrix.LSolve(vector, true);
            Vector resultActual = new Vector(new double[] { 1, 0.8, 0.125 });

            for (int i = 0; i < result.Size; i++)
                Assert.Equal(result[i], resultActual[i], 8);
        }

        [Fact]
        public void DenseSymmetricMatrix_TestUSolveExeptions()
        {
            _matrix = new double[][] { new double[]{ 1 },
                                       new double[] { 2, 5 },
                                       new double[] { 2, 5, 4 } };
            SymmetricDenseMatrix denseSymmetricMatrix = new SymmetricDenseMatrix(_matrix);

            Vector exampleVector = new Vector(new double[2] { 1.0, 2.0 });

            Assert.Throws<ArgumentNullException>(() => denseSymmetricMatrix.USolve(null, false));
            Assert.Throws<RankException>(() => denseSymmetricMatrix.USolve(exampleVector, false));
        }

        [Fact]
        public void DenseSymmetricMatrix_TestUSolve()
        {
            _matrix = new double[][] { new double[]{ 1 },
                                       new double[] { 2, 5 },
                                       new double[] { 2, 5, 4 } };
            SymmetricDenseMatrix denseSymmetricMatrix = new SymmetricDenseMatrix(_matrix);

            Vector vector = new Vector(new double[] { 1, 0, -0.04301075 });

            var result = denseSymmetricMatrix.USolve(vector, false);
            Vector resultActual = new Vector(new double[] { 0.655914, 0.21505375, -0.04301075 });
 

            for (int i = 0; i < result.Size; i++)
                Assert.Equal(result[i], resultActual[i], 8);
        }

        [Fact]
        public void DenseSymmetricMatrix_TestMultyplyExceptions()
        {
            Vector vector = new Vector(new double[] { 1, 0 });

            Assert.Throws<ArgumentNullException>(() => denseSymmetricMatrix.Multiply(null));
            Assert.Throws<RankException>(() => denseSymmetricMatrix.Multiply(vector));
        }

    }
}

        