using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;
using SolverCore;
using Xunit.Abstractions;

namespace MF.Skyline
{
    public class TestSkylineMatrix
    {
        private double[] di; // диагональ
        private double[] al; // массив элементов профиля нижнего треугольника
        private double[] au; // массив элементов профиля верхнего треугольника
        private int[] ia; // целочисленный массив с указателями начала строк профиля 
        private SkylineMatrix skylineMatrix;
        private readonly ITestOutputHelper _testOutputHelper;



        public TestSkylineMatrix(ITestOutputHelper testOutputHelper)
        {

            di = new double[] { 1, 2, 3 };
            al = new double[] { 1, 2, 3 };
            au = new double[] { 3, 2, 1 };
            ia = new int[] { 1, 1, 2, 4 };
            skylineMatrix = new SkylineMatrix(di, ia, al, au);

            _testOutputHelper = testOutputHelper;
        }





        [Fact]
        public void LMult()
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

        

        [Fact]
        public void UMult()
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

        

        [Fact]
        public void LSolve()
        {

            di = new double[] { 1, 2, 3 };
            au = new double[] { 1, 2, 3 };
            al = new double[] { 3, 2, 1 };
            ia = new int[] { 1, 1, 2, 4 };
            skylineMatrix = new SkylineMatrix(di, ia, al, au);


            Vector vector = new Vector(new double[] { 1, 5, 17 });

            var result = skylineMatrix.LSolve(vector, true);
            Vector resultActual = new Vector(new double[] { 1, 1, 4.66666666667 });

            for (int i = 0; i < result.Size; i++)
                Assert.Equal(result[i], resultActual[i], 8);
        }

        

        [Fact]
        public void USolve()
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

        

        [Fact]
        public void Fill()
        {
            FillFunc fillFunc = (row, col) => { return (row + 1) + (col + 1); };

            skylineMatrix.Fill(fillFunc);

            di = new double[] { 2, 4, 6 };
            al = new double[] { 3, 4, 5 };
            au = new double[] { 3, 4, 5 };
            ia = new int[] { 1, 1, 2, 4 };


            SkylineMatrix skyline = new SkylineMatrix(di, ia, al, au);
            Assert.True(new HashSet<(double, int, int)>(skylineMatrix).SetEquals(skyline));

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
                    (2,0,2),
                    (1,1,0),
                    (2,1,1),
                    (1,1,2),
                    (2,2,0),
                    (3,2,1),
                    (3,2,2),
                };

            foreach (var elem in skylineMatrix)
                _testOutputHelper.WriteLine(elem.ToString());

            Assert.True(new HashSet<(double, int, int)>(skylineMatrix).SetEquals(elemList));

        }
    }
}
