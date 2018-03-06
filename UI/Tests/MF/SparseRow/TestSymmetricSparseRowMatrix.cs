using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;
using SolverCore;

namespace MF.SymmetricSparseRow
{
    public class TestSymmetricSparseRowMatrix
    {
        private double[] _a;
        private int[] _ia;
        private int[] _ja;
        private IVector vector;

        private SymmetricSparseRowMatrix sparseRowMatrix;


        public TestSymmetricSparseRowMatrix()
        {
            _a = new double[] { 1, 2, 5, 3, 7, 4 };
            _ia = new int[] { 0, 1, 2, 4, 6 };
            _ja = new int[] { 0, 1, 0, 2, 1, 3 };

            vector = new Vector(new double[] { 2, 1, 1, 1 });

            sparseRowMatrix = new SymmetricSparseRowMatrix(_a, _ja, _ia);
        }

        [Fact]
        public void SparseRowMatrix_TestLMult()
        {
            var resultTrueDiag = sparseRowMatrix.LMult(vector, true);
            Vector resultActualTrueDiag = new Vector(new double[] { 2, 2, 13, 11 });

            var resultFalseDiag = sparseRowMatrix.LMult(vector, false);
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
            var resultTrueDiag = sparseRowMatrix.UMult(vector, true);
            Vector resultActualTrueDiag = new Vector(new double[] { 7, 9, 3, 4 });

            var resultFalseDiag = sparseRowMatrix.UMult(vector, false);
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
            IVector vector = sparseRowMatrix.LMult(resultActual, true);

            var result = sparseRowMatrix.LSolve(vector, true);

            for (int i = 0; i < result.Size; i++)
                Assert.Equal(result[i], resultActual[i], 8);
        }

        [Fact]
        public void SparseRowMatrix_TestUSolve()
        {
            IVector resultActual = new Vector(new double[] { 1, 2, 3, 4 });
            IVector vector = sparseRowMatrix.UMult(resultActual, true);

            var result = sparseRowMatrix.USolve(vector, true);

            for (int i = 0; i < result.Size; i++)
                Assert.Equal(result[i], resultActual[i], 8);
        }

        [Fact]
        public void SymmitricSparseRowMatrix_TesMultiply()
        {
            var result = sparseRowMatrix.Multiply(vector);
            Vector resultActual = new Vector(new double[] { 7, 9, 13, 11 });

            for (int i = 0; i < resultActual.Size; i++)
                Assert.Equal(result[i], resultActual[i], 8);
        }
    }
}
