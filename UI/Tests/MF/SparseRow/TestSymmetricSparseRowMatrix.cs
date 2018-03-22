using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;
using SolverCore;
using Xunit.Abstractions;
using UI;

namespace MF.SymmetricSparseRow
{
    public class TestSymmetricSparseRowMatrix
    {
        private double[,] _matrix;
        private double[] _a;
        private int[] _ia;
        private int[] _ja;
        private IVector vector;

        private readonly ITestOutputHelper _testOutputHelper;

        private SymmetricSparseRowMatrix symmetricSparseRowMatrix;


        public TestSymmetricSparseRowMatrix(ITestOutputHelper testOutputHelper)
        {
            _a = new double[] { 1, 2, 5, 3, 7, 4 };
            _ia = new int[] { 0, 1, 2, 4, 6 };
            _ja = new int[] { 0, 1, 0, 2, 1, 3 };

            vector = new Vector(new double[] { 2, 1, 1, 1 });

            symmetricSparseRowMatrix = new SymmetricSparseRowMatrix(_a, _ja, _ia);
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void SparseRowMatrix_TestLMult()
        {
            var resultTrueDiag = symmetricSparseRowMatrix.LMult(vector, true);
            Vector resultActualTrueDiag = new Vector(new double[] { 2, 2, 13, 11 });

            var resultFalseDiag = symmetricSparseRowMatrix.LMult(vector, false);
            Vector resultActualFalseDiag = new Vector(new double[] { 2, 1, 11, 8 });

            for (int i = 0; i < resultTrueDiag.Size; i++)
            {
                Assert.Equal(resultTrueDiag[i], resultActualTrueDiag[i], 8);
                Assert.Equal(resultFalseDiag[i], resultActualFalseDiag[i], 8);
            }
        }

        [Fact]
        public void SparseRowMatrix_TestUMult()
        {
            var resultTrueDiag = symmetricSparseRowMatrix.UMult(vector, true);
            Vector resultActualTrueDiag = new Vector(new double[] { 7, 9, 3, 4 });

            var resultFalseDiag = symmetricSparseRowMatrix.UMult(vector, false);
            Vector resultActualFalseDiag = new Vector(new double[] { 7, 8, 1, 1 });

            for (int i = 0; i < resultTrueDiag.Size; i++)
            {
                Assert.Equal(resultTrueDiag[i], resultActualTrueDiag[i], 8);
                Assert.Equal(resultFalseDiag[i], resultActualFalseDiag[i], 8);
            }
        }

        [Fact]
        public void SparseRowMatrix_TestLSolve()
        {
            IVector resultActual = new Vector(new double[] { 1, 2, 3, 4 });
            IVector vector = symmetricSparseRowMatrix.LMult(resultActual, true);

            var result = symmetricSparseRowMatrix.LSolve(vector, true);

            for (int i = 0; i < result.Size; i++)
                Assert.Equal(result[i], resultActual[i], 8);
        }

        [Fact]
        public void SparseRowMatrix_TestUSolve()
        {
            IVector resultActual = new Vector(new double[] { 1, 2, 3, 4 });
            IVector vector = symmetricSparseRowMatrix.UMult(resultActual, true);

            var result = symmetricSparseRowMatrix.USolve(vector, true);

            for (int i = 0; i < result.Size; i++)
                Assert.Equal(result[i], resultActual[i], 8);
        }

        [Fact]
        public void SparseRowMatrix_TestMultiply()
        {
            var result = symmetricSparseRowMatrix.Multiply(vector);
            Vector resultActual = new Vector(new double[] { 7, 9, 13, 11 });

            for (int i = 0; i < resultActual.Size; i++)
                Assert.Equal(result[i], resultActual[i], 8);
        }


        [Fact]
        public void SymmetricSparseRowMatrix_TestForeach()
        {
            //_a = new double[] { 1, 2, 5, 3, 7, 4 };
            //_ia = new int[] { 0, 1, 2, 4, 6 };
            //_ja = new int[] { 0, 1, 0, 2, 1, 3 };
            List<(double, int, int)> elemList =
                new List<(double, int, int)>()
                {
                    (1,0,0),
                    (5,0,2),
                    (2,1,1),
                    (7,1,3),
                    (5,2,0),
                    (3,2,2),
                    (7,3,1),
                    (4,3,3),
                };


            Assert.True(new HashSet<(double, int, int)>(symmetricSparseRowMatrix).SetEquals(elemList));

            foreach (var elem in symmetricSparseRowMatrix)
                _testOutputHelper.WriteLine(elem.ToString());
        }

        [Fact]
        public void SparseRowMatrix_Fill()
        {
            FillFunc fillFunc = (row, col) => { return (row + 1) + (col + 1); };

            // лишняя итерация
            symmetricSparseRowMatrix.Fill(fillFunc);

            _a = new double[] { 2, 4, 4, 6, 6, 8 };
            _ia = new int[] { 0, 1, 2, 4, 6 };
            _ja = new int[] { 0, 1, 0, 2, 1, 3 };

            SymmetricSparseRowMatrix sparseRow = new SymmetricSparseRowMatrix(_a, _ja, _ia);

            Assert.True(new HashSet<(double, int, int)>(symmetricSparseRowMatrix).SetEquals(sparseRow));
        }
    }
   
}



