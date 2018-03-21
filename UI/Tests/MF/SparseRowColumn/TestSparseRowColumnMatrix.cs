using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;
using SolverCore;
using Xunit.Abstractions;

namespace MF.SparseRowColumn
{
    public class TestSparseRowColumnMatrix
    {
        private int[] ia;
        private int[] ja;

        private double[] di;
        private double[] al;
        private double[] au;

        private IVector vector;

        private SparseRowColumnMatrix sparseRowColumnMatrix;
        private readonly ITestOutputHelper _testOutputHelper;

        public TestSparseRowColumnMatrix(ITestOutputHelper testOutputHelper)
        {
            di = new double[] { 1,2,3 };
            al = new double[] { 1,2,3 };
            au = new double[] { 3,2,1 };
            ja = new int[] { 1,1,2 };
            ia = new int[] { 1, 1, 2, 4};

            vector = new Vector(new double[] { 1, 1, 1 });

            sparseRowColumnMatrix = new SparseRowColumnMatrix(di,al,au,ia,ja);
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void SparseRowColumnMatrix_TestForeach()
        {


            List<(double, int, int)> elemList =
                new List<(double, int, int)>()
                {
                    (1,0,0),
                    (3,0,1),
                    (2,0,2),
                    (1,1,0),
                    (2,1,1),
                    (1,1,2),
                    (2,2,0),
                    (3,2,1),
                    (3,2,2),
                };


            Assert.True(new HashSet<(double, int, int)>(sparseRowColumnMatrix).SetEquals(elemList));

            foreach (var elem in sparseRowColumnMatrix)
                _testOutputHelper.WriteLine(elem.ToString());
        }

        [Fact]
        public void SparseRowColumnMatrix_TestLMult()
        {
            var resultTrueDiag = sparseRowColumnMatrix.LMult(vector, true);
            Vector resultActualTrueDiag = new Vector(new double[] { 1,3,8 });

            var resultFalseDiag = sparseRowColumnMatrix.LMult(vector, false);
            Vector resultActualFalseDiag = new Vector(new double[] { 1,2,6 });

            for (int i = 0; i < resultTrueDiag.Size; i++)
            {
                Assert.Equal(resultTrueDiag[i], resultActualTrueDiag[i], 8);
                Assert.Equal(resultFalseDiag[i], resultActualFalseDiag[i], 8);
            }
        }

        [Fact]
        public void SparseRowColumnMatrix_TestUMult()
        {
            var resultTrueDiag = sparseRowColumnMatrix.UMult(vector, true);
            Vector resultActualTrueDiag = new Vector(new double[] { 6,3,3 });

            var resultFalseDiag = sparseRowColumnMatrix.UMult(vector, false);
            Vector resultActualFalseDiag = new Vector(new double[] { 6,2,1 });

            for (int i = 0; i < resultTrueDiag.Size; i++)
            {
                Assert.Equal(resultTrueDiag[i], resultActualTrueDiag[i], 8);
                Assert.Equal(resultFalseDiag[i], resultActualFalseDiag[i], 8);
            }
        }

        [Fact]
        public void SparseRowColumnMatrix_TestLSolve()
        {
            IVector resultActual = new Vector(new double[] { 1,1,1 });
            IVector vector = sparseRowColumnMatrix.LMult(resultActual, true);

            var result = sparseRowColumnMatrix.LSolve(vector, true);

            for (int i = 0; i < result.Size; i++)
                Assert.Equal(result[i], resultActual[i], 8);
        }

        [Fact]
        public void SparseRowColumnMatrix_TestUSolve()
        {
            IVector resultActual = new Vector(new double[] { 1,1,1 });
            IVector vector = sparseRowColumnMatrix.UMult(resultActual, true);

            var result = sparseRowColumnMatrix.USolve(vector, true);

            for (int i = 0; i < result.Size; i++)
                Assert.Equal(result[i], resultActual[i], 8);
        }

        [Fact]
        public void SparseRowColumnMatrix_TestMultiply()
        {
            var result = sparseRowColumnMatrix.Multiply(vector);
            Vector resultActual = new Vector(new double[] { 6,4,8 });

            for (int i = 0; i < resultActual.Size; i++)
                Assert.Equal(result[i], resultActual[i], 8);
        }

        [Fact]
        public void SparseRowColumnMatrix_TestUMultTranspose()
        {
            var resultTrueDiag = sparseRowColumnMatrix.UMultTranspose(vector, true);
            Vector resultActualTrueDiag = new Vector(new double[] { 1,5,6});

            var resultFalseDiag = sparseRowColumnMatrix.UMultTranspose(vector, false);
            Vector resultActualFalseDiag = new Vector(new double[] { 1,4,4});


            for (int i = 0; i < resultTrueDiag.Size; i++)
            {
                Assert.Equal(resultTrueDiag[i], resultActualTrueDiag[i], 8);
                Assert.Equal(resultFalseDiag[i], resultActualFalseDiag[i], 8);
            }
        }

        [Fact]
        public void SparseRowColumnMatrix_TestLMultTranspose()
        {
            var resultTrueDiag = sparseRowColumnMatrix.LMultTranspose(vector, true);
            Vector resultActualTrueDiag = new Vector(new double[] { 4, 5, 3 });

            var resultFalseDiag = sparseRowColumnMatrix.LMultTranspose(vector, false);
            Vector resultActualFalseDiag = new Vector(new double[] { 4, 4, 1});

            for (int i = 0; i < resultTrueDiag.Size; i++)
            {
                Assert.Equal(resultTrueDiag[i], resultActualTrueDiag[i], 8);
                Assert.Equal(resultFalseDiag[i], resultActualFalseDiag[i], 8);
            }
        }

        [Fact]
        public void SparseRowColumnMatrix_TestLSolveTranspose()
        {
            IVector resultActual = new Vector(new double[] { 1,5,6 });
            IVector vector = sparseRowColumnMatrix.LMultTranspose(resultActual, true);

            var result = sparseRowColumnMatrix.LSolveTranspose(vector, true);

            for (int i = 0; i < result.Size; i++)
                Assert.Equal(result[i], resultActual[i], 8);
        }

        [Fact]
        public void SparseRowColumnMatrix_TestUSolveTranspose()
        {
            IVector resultActual = new Vector(new double[] { 4,5,3 });
            IVector vector = sparseRowColumnMatrix.UMultTranspose(resultActual, true);

            var result = sparseRowColumnMatrix.USolveTranspose(vector, true);

            for (int i = 0; i < result.Size; i++)
                Assert.Equal(result[i], resultActual[i], 8);
        }


        [Fact]
        public void SparseRowColumnMatrix_Fill()
        {
            FillFunc fillFunc = (row, col) => { return (row + 1) + (col + 1); };

            sparseRowColumnMatrix.Fill(fillFunc);
           //di = new double[] { 1, 2, 3 };
           //al = new double[] { 1, 2, 3 };
           //au = new double[] { 3, 2, 1 };
           //ja = new int[] { 1, 1, 2 };
           //ia = new int[] { 1, 1, 2, 4 };

            di = new double[] { 2, 4, 6 };
            al = new double[] { 3, 4, 5 };
            au = new double[] { 3, 4, 5 };
            ja = new int[] { 1, 1, 2 };
            ia = new int[] { 1, 1, 2, 4 };


            SparseRowColumnMatrix sparseRowCollumn = new SparseRowColumnMatrix(di, al, au, ia, ja);
            Assert.True(new HashSet<(double, int, int)>(sparseRowColumnMatrix).SetEquals(sparseRowCollumn));

        }
    }
}
