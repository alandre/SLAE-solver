using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;
using SolverCore;

namespace MF.SparseRow
{
    public class TestSparseRowMatrix
    {

        [Fact]
        public void SparseRowMatrix_TestLMult()
        {
            var a = new double[] { 1, 8, 2, 7, 2, 3, 7, 4 };
            var ia = new int[] { 0, 2, 4, 6, 8 };
            var ja = new int[] { 0, 2, 1, 3, 0, 2, 1, 3 };
            SparseRowMatrix sparseRowMatrix = new SparseRowMatrix(a,ja,ia);

            IVector vector = new Vector(new double[] { 2, 1, 1, 1 });

            var resultTrueDiag = sparseRowMatrix.LMult(vector, true);
            Vector resultActualTrueDiag = new Vector(new double[] { 2, 2, 7, 11 });

            var resultFalseDiag = sparseRowMatrix.LMult(vector, false);
            Vector resultActualFalseDiag = new Vector(new double[] { 2, 1, 5, 8 });

            for (int i = 0; i < resultTrueDiag.Size; i++)
            {
                Assert.Equal(resultTrueDiag[i], resultActualTrueDiag[i], 8);
                Assert.Equal(resultFalseDiag[i], resultActualFalseDiag[i], 8);
            }
        }

       [Fact]
       public void SparseRowMatrix_TestUMult()
       {
            var a = new double[] { 1, 8, 2, 7, 2, 3, 7, 4 };
            var ia = new int[] { 0, 2, 4, 6, 8 };
            var ja = new int[] { 0, 2, 1, 3, 0, 2, 1, 3 };
            SparseRowMatrix sparseRowMatrix = new SparseRowMatrix(a, ja, ia);

            IVector vector = new Vector(new double[] { 2, 1, 1, 1 });

            var resultTrueDiag = sparseRowMatrix.UMult(vector, true);
            Vector resultActualTrueDiag = new Vector(new double[] { 10, 9, 3, 4 });

            var resultFalseDiag = sparseRowMatrix.UMult(vector, false);
            Vector resultActualFalseDiag = new Vector(new double[] { 10, 8, 1, 1 });

            for (int i = 0; i < resultTrueDiag.Size; i++)
            {
                Assert.Equal(resultTrueDiag[i], resultActualTrueDiag[i], 8);
                Assert.Equal(resultFalseDiag[i], resultActualFalseDiag[i], 8);
            }
       }
      
       [Fact]
       public void SparseRowMatrix_TestLSolve()
       {
            var a = new double[] { 1, 8, 2, 7, 2, 3, 7, 4 };
            var ia = new int[] { 0, 2, 4, 6, 8 };
            var ja = new int[] { 0, 2, 1, 3, 0, 2, 1, 3 };
            SparseRowMatrix sparseRowMatrix = new SparseRowMatrix(a, ja, ia);

           IVector resultActual = new Vector(new double[] { 1, 2, 3, 4 });
           IVector vector = sparseRowMatrix.LMult(resultActual, true);
      
           var result = sparseRowMatrix.LSolve(vector, true);
      
           for (int i = 0; i < result.Size; i++)
               Assert.Equal(result[i], resultActual[i], 8);
       }

        [Fact]
        public void SparseRowMatrix_TestUSolve()
        {
            var a = new double[] { 1, 8, 2, 7, 2, 3, 7, 4 };
            var ia = new int[] { 0, 2, 4, 6, 8 };
            var ja = new int[] { 0, 2, 1, 3, 0, 2, 1, 3 };
            SparseRowMatrix sparseRowMatrix = new SparseRowMatrix(a, ja, ia);

            IVector resultActual = new Vector(new double[] { 1, 2, 3, 4 });
            IVector vector = sparseRowMatrix.UMult(resultActual, true);

            var result = sparseRowMatrix.USolve(vector, true);

            for (int i = 0; i < result.Size; i++)
                Assert.Equal(result[i], resultActual[i], 8);
        }
    }
}
