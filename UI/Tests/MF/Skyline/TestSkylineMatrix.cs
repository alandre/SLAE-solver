using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;
using SolverCore;

namespace MF.Skyline
{
    public class TestSkylineMatrix
    {
        private double[] di; // диагональ
        private double[] al; // массив элементов профиля нижнего треугольника
        private double[] au; // массив элементов профиля верхнего треугольника
        private int[] ia; // целочисленный массив с указателями начала строк профиля 
        private SkylineMatrix skylineMatrix;




        public TestSkylineMatrix()
        {

            di = new double[] { 1, 2, 3 };
            al = new double[] { 1, 2, 3 };
            au = new double[] { 3, 2, 1 };
            ia = new int[] { 1, 1, 2, 4 };
            skylineMatrix = new SkylineMatrix(di, ia,al,au);    

        }





        [Fact]
        public void SkylineMatrix_TestLMult()
        {
            Vector vector = new Vector(new double[] { 1, 1, 1 });

            var resultTrueDiag = skylineMatrix.LMult(vector, true);
            Vector resultActualTrueDiag = new Vector(new double[] { 1, 3, 8 });

            var resultFalseDiag = skylineMatrix.LMult(vector, false);
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
        public void SkylineMatrix_TestUMult()
        {
            Vector vector = new Vector(new double[] { 1,1,1});

            var resultFalseDiag = skylineMatrix.UMult(vector, false);
            Vector resultActualFalseDiag = new Vector(new double[] { 6, 2, 1 });

            var resultTrueDiag = skylineMatrix.UMult(vector, true);
            Vector resultActualTrueDiag = new Vector(new double[] { 6, 3, 3 });

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
        public void SkylineMatrix_TestLSolve()
        {

            di = new double[] { 1, 2, 3 };
            al = new double[] { 1, 2, 3 };
            al = new double[] { 3, 2, 1 };
            ia = new int[] { 1, 1, 2, 4 };
            skylineMatrix = new SkylineMatrix(di, ia, al, au);


            Vector vector = new Vector(new double[] { 1, 5, 17 });

            var result = skylineMatrix.LSolve(vector, true);
            Vector resultActual = new Vector(new double[] { 1, 1, 4.66666666667 });

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
        public void SkylineMatrix_TestUSolve()
        {
            di = new double[] { 1, 2, 3 };
            al = new double[] { 1, 2, 3 };
            au = new double[] { 3, 2, 1 };
            ia = new int[] { 1, 1, 2, 4 };
            skylineMatrix = new SkylineMatrix(di, ia, al, au);

            Vector vector = new Vector(new double[] { 4, 4, 1 });

            var result = skylineMatrix.USolve(vector, false);
            Vector resultActual = new Vector(new double[] { -7, 3, 1 });

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
