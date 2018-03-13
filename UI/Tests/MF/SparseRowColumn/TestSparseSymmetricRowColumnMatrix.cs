using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;
using SolverCore;

namespace MF.SymmetricSparseRowColumn
{
    public class TestSparseSymmetricRowColumnMatrix
    {
        private int[] ia;
        private int[] ja;

        private double[] di;
        private double[] aa;
      

        private IVector vector;

        private SymmetricSparseRowColumnMatrix sparseSymmetricRowColumnMatrix;


        public TestSparseSymmetricRowColumnMatrix()
        {
            di = new double[] { 1, 2, 3 };
          
            aa = new double[] { 3, 2, 1 };
            ja = new int[] { 1, 1, 2 };
            ia = new int[] { 1, 1, 2, 4 };

            vector = new Vector(new double[] { 1, 1, 1 });

            sparseSymmetricRowColumnMatrix = new SymmetricSparseRowColumnMatrix(di, aa, ia, ja);
        }

        [Fact]
        public void SparseRowColumnMatrix_TestLMult()
        {
            var resultTrueDiag = sparseSymmetricRowColumnMatrix.LMult(vector, true);
            Vector resultActualTrueDiag = new Vector(new double[] { 1, 5, 6 });

            var resultFalseDiag = sparseSymmetricRowColumnMatrix.LMult(vector, false);
            Vector resultActualFalseDiag = new Vector(new double[] { 1, 4, 4 });

            for (int i = 0; i < resultTrueDiag.Size; i++)
            {
                Assert.Equal(resultTrueDiag[i], resultActualTrueDiag[i], 8);
                Assert.Equal(resultFalseDiag[i], resultActualFalseDiag[i], 8);
            }
        }

        [Fact]
        public void SparseRowColumnMatrix_TestUMult()
        {
            var resultTrueDiag = sparseSymmetricRowColumnMatrix.UMult(vector, true);
            Vector resultActualTrueDiag = new Vector(new double[] { 6, 3, 3 });

            var resultFalseDiag = sparseSymmetricRowColumnMatrix.UMult(vector, false);
            Vector resultActualFalseDiag = new Vector(new double[] { 6, 2, 1 });

            for (int i = 0; i < resultTrueDiag.Size; i++)
            {
                Assert.Equal(resultTrueDiag[i], resultActualTrueDiag[i], 8);
                Assert.Equal(resultFalseDiag[i], resultActualFalseDiag[i], 8);
            }
        }

        [Fact]
        public void SparseRowColumnMatrix_TestLSolve()
        {
            IVector resultActual = new Vector(new double[] { 1, 1, 1 });
            IVector vector = sparseSymmetricRowColumnMatrix.LMult(resultActual, true);

            var result = sparseSymmetricRowColumnMatrix.LSolve(vector, true);

            for (int i = 0; i < result.Size; i++)
                Assert.Equal(result[i], resultActual[i], 8);
        }

        [Fact]
        public void SparseRowColumnMatrix_TestUSolve()
        {
            IVector resultActual = new Vector(new double[] { 1, 1, 1 });
            IVector vector = sparseSymmetricRowColumnMatrix.UMult(resultActual, true);

            var result = sparseSymmetricRowColumnMatrix.USolve(vector, true);

            for (int i = 0; i < result.Size; i++)
                Assert.Equal(result[i], resultActual[i], 8);
        }

        [Fact]
        public void SparseRowColumnMatrix_TestMultiply()
        {
            var result = sparseSymmetricRowColumnMatrix.Multiply(vector);
            Vector resultActual = new Vector(new double[] { 6, 6, 6 });

            for (int i = 0; i < resultActual.Size; i++)
                Assert.Equal(result[i], resultActual[i], 8);
        }

        
    }
}
