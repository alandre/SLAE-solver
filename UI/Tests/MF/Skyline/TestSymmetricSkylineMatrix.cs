using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;
using SolverCore;

namespace MF.SymmetricSkyline
{
    public class TestSymmetricSkylineMatrix
    {
        private double[] di; // диагональ
        private double[] al; // массив элементов профиля нижнего треугольника
        private int[] ia; // целочисленный массив с указателями начала строк профиля 
        private SymmetricSkylineMatrix symmetricSkylineMatrix;

        


        public TestSymmetricSkylineMatrix()
        {

            di = new double[] { 1, 2, 3 };
            al = new double[] { 1, 2, 3 };
            ia = new int[] { 1, 1, 2, 4 };
            symmetricSkylineMatrix = new SymmetricSkylineMatrix(di, ia, al);    

        }





        [Fact]
        public void SymmetricSkylineMatrix_TestLMult()
        {
            Vector vector = new Vector(new double[] { 1, 1, 1 });

            var resultTrueDiag = symmetricSkylineMatrix.LMult(vector, true);
            Vector resultActualTrueDiag = new Vector(new double[] { 1, 3, 8 });

            var resultFalseDiag = symmetricSkylineMatrix.LMult(vector, false);
            Vector resultActualFalseDiag = new Vector(new double[] { 1,2,6 });

            for (int i = 0; i < resultTrueDiag.Size; i++)
            {
                Assert.Equal(resultTrueDiag[i], resultActualTrueDiag[i], 8);
                Assert.Equal(resultFalseDiag[i], resultActualFalseDiag[i], 8);
            }
        }

        //[Fact]
        //public void DenseMatrix_TestUMultExeptions()
        //{
        //    Vector exampleVector = new Vector(new double[2] { 1.0, 2.0 });

        //    Assert.Throws<ArgumentNullException>(() => denseMatrix.UMult(null, false, DiagonalElement.One));
        //    Assert.Throws<RankException>(() => denseMatrix.UMult(exampleVector, false, DiagonalElement.One));
        //}

        [Fact]
        public void SymmetricSkylineMatrix_TestUMult()
        {
            Vector vector = new Vector(new double[] { 1,1,1});

            var resultFalseDiag = symmetricSkylineMatrix.UMult(vector, false);
            Vector resultActualFalseDiag = new Vector(new double[] { 4, 4, 1 });

            var resultTrueDiag = symmetricSkylineMatrix.UMult(vector, true);
            Vector resultActualTrueDiag = new Vector(new double[] { 4, 5, 3 });

            for (int i = 0; i < resultFalseDiag.Size; i++)
            {
                Assert.Equal(resultFalseDiag[i], resultActualFalseDiag[i], 8);
                Assert.Equal(resultTrueDiag[i], resultActualTrueDiag[i], 8);
            }
        }

        //[Fact]
        //public void DenseMatrix_TestLSolveExeptions()
        //{
        //    Vector exampleVector = new Vector(new double[2] { 1.0, 2.0 });

        //    Assert.Throws<ArgumentNullException>(() => denseMatrix.LSolve(null, false));
        //    Assert.Throws<RankException>(() => denseMatrix.LSolve(exampleVector, false));
        //}

        [Fact]
        public void SymmetricSkylineMatrix_TestLSolve()
        {

            di = new double[] { 1, 2, 3 };
            al = new double[] { 1, 2, 3 };
            ia = new int[] { 1, 1, 2, 4 };
            symmetricSkylineMatrix = new SymmetricSkylineMatrix(di, ia, al);


            Vector vector = new Vector(new double[] { 1, 5, 17 });

            var result = symmetricSkylineMatrix.LSolve(vector, true);
            Vector resultActual = new Vector(new double[] { 1, 2, 3 });

            for (int i = 0; i < result.Size; i++)
                Assert.Equal(result[i], resultActual[i], 8);
        }

        //[Fact]
        //public void DenseMatrix_TestUSolveExeptions()
        //{
        //    _matrix = new double[3, 3] { { 1, 3, 5 }, { 2, 5, 4 }, { 7, 1, 8 } };
        //    DenseMatrix denseMatrix = new DenseMatrix(_matrix);

        //    Vector exampleVector = new Vector(new double[2] { 1.0, 2.0 });

        //    Assert.Throws<ArgumentNullException>(() => denseMatrix.USolve(null, false));
        //    Assert.Throws<RankException>(() => denseMatrix.USolve(exampleVector, false));
        //}

        [Fact]
        public void DenseMatrix_TestUSolve()
        {
            di = new double[] { 1, 2, 3 };
            al = new double[] { 1, 2, 3 };
            ia = new int[] { 1, 1, 2, 4 };
            symmetricSkylineMatrix = new SymmetricSkylineMatrix(di, ia, al);

            Vector vector = new Vector(new double[] { 4, 4, 1 });

            var result = symmetricSkylineMatrix.USolve(vector, false);
            Vector resultActual = new Vector(new double[] { 1, 1, 1 });

            for (int i = 0; i < result.Size; i++)
                Assert.Equal(result[i], resultActual[i], 8);
        }

        //[Fact]
        //public void DenseMatrix_TestMultyplyExceptions()
        //{
        //    Vector vector = new Vector(new double[] { 1, 0 });

        //    Assert.Throws<ArgumentNullException>(() => denseMatrix.Multiply(null));
        //    Assert.Throws<RankException>(() => denseMatrix.Multiply(vector));
        //}


    }
}
