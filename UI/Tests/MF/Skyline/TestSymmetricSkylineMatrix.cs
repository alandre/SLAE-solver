using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;
using SolverCore;
using Xunit.Abstractions;

namespace MF.SymmetricSkyline
{
    public class TestSymmetricSkylineMatrix
    {
        private double[] di; // диагональ
        private double[] al; // массив элементов профиля нижнего треугольника
        private int[] ia; // целочисленный массив с указателями начала строк профиля 
        private SymmetricSkylineMatrix symmetricSkylineMatrix;
        private readonly ITestOutputHelper _testOutputHelper;



        public TestSymmetricSkylineMatrix(ITestOutputHelper testOutputHelper)
        {

            di = new double[] { 1, 2, 3 };
            al = new double[] { 1, 2, 3 };
            ia = new int[] { 1, 1, 2, 4 };
            symmetricSkylineMatrix = new SymmetricSkylineMatrix(di, ia, al);
            _testOutputHelper = testOutputHelper;
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

        [Fact]
        public void SymmetricSkylineMatrix_Foreach()
        {
            //di = new double[] { 1, 2, 3 };
            //al = new double[] { 1, 2, 3 };
            //ia = new int[] { 1, 1, 2, 4 };
            //symmetricSkylineMatrix = new SymmetricSkylineMatrix(di, ia, al);


            List<(double, int, int)> elemList =
               new List<(double, int, int)>()
               {
                    (1,0,0),
                    (1,0,1),
                    (2,0,2),
                    (1,1,0),
                    (2,1,1),
                    (3,1,2),
                    (2,2,0),
                    (3,2,1),
                    (3,2,2),
               };


            Assert.True(new HashSet<(double, int, int)>(symmetricSkylineMatrix).SetEquals(elemList));

            foreach (var elem in symmetricSkylineMatrix)
                _testOutputHelper.WriteLine(elem.ToString());
        }


     

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

     

        [Fact]
        public void SymmetricSkylineMatrix_TestUSolve()
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

       

        [Fact]
        public void SymmetricSkylineMatrix_Fill()
        {
            FillFunc fillFunc = (row, col) => { return (row + 1) + (col + 1); };

            
            symmetricSkylineMatrix.Fill(fillFunc);

            //di = new double[] { 1, 2, 3 };
            //al = new double[] { 1, 2, 3 };
            //ia = new int[] { 1, 1, 2, 4 };
            di = new double[] { 2, 4, 6 };
            al = new double[] { 3, 4, 5 };
            ia = new int[] { 1, 1, 2, 4 };
            SymmetricSkylineMatrix skyline = new SymmetricSkylineMatrix(di,ia,al);
            Assert.True(new HashSet<(double, int, int)>(symmetricSkylineMatrix).SetEquals(skyline));

        }

    }
}
