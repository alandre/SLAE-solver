using System.Collections.Generic;

using Xunit;
using SolverCore;
using UI;
using Xunit.Abstractions;

namespace MF.SymmetricCoordinational
{
    public class TestSymmetricCoordinationalMatrix
    {

        private int[] rows;
        private int[] columns;

        private double[] values;
        private int size;

        private IVector vector;

        private SymmetricCoordinationalMatrix symmetricCoordinationalMatrix;
        private readonly ITestOutputHelper _testOutputHelper;

        public TestSymmetricCoordinationalMatrix(ITestOutputHelper testOutputHelper)
        {
            size = 3;
            values = new double[] { 1, 4, 2, 5, 3 };
            columns = new int[] { 0, 0, 1, 0, 2 };
            rows = new int[] { 0, 1, 1, 2, 2 };

            vector = new Vector(new double[] { 1, 1, 1 });

            symmetricCoordinationalMatrix = new SymmetricCoordinationalMatrix(rows, columns, values, size);
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void SkylineMatrix_TestForeach()
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
                    (4,0,1),
                    (5,0,2),
                    (4,1,0),
                    (2,1,1),
                    //(0,1,2),
                    (5,2,0),
                   // (0,2,1),
                    (3,2,2),
                };


            Assert.True(new HashSet<(double, int, int)>(symmetricCoordinationalMatrix).SetEquals(elemList));

            foreach (var elem in symmetricCoordinationalMatrix)
                _testOutputHelper.WriteLine(elem.ToString());
        }

        //[Fact]
        [Theory]
        [InlineData(FormatFactory.Formats.Coordinational)]
        [InlineData(FormatFactory.Formats.Dense)]
        [InlineData(FormatFactory.Formats.Skyline)]
        [InlineData(FormatFactory.Formats.SparseRow)]
        [InlineData(FormatFactory.Formats.SparseRowColumn)]
        public void SymmetricCoordinationalMatrix_TestConstructor(FormatFactory.Formats type)
        {

            var exploredMatrix = FormatFactory.Convert(symmetricCoordinationalMatrix, type);
            var backCoordMatrix = exploredMatrix.ConvertToCoordinationalMatrix();
            Assert.True(new HashSet<(double, int, int)>(symmetricCoordinationalMatrix).SetEquals(backCoordMatrix));

           // var formatFactory = new FormatFactory();
           // 
           // foreach (var type in formatFactory.formats)
           // {
           //     var exploredMatrix = FormatFactory.Convert(symmetricCoordinationalMatrix, type.Key);
           //     var backCoordMatrix = exploredMatrix.ConvertToCoordinationalMatrix();
           //     Assert.True(new HashSet<(double, int, int)>(symmetricCoordinationalMatrix).SetEquals(backCoordMatrix));
           // }
        }

        [Fact]
        public void SymmetricCoordinational_TestLMult()
        {
            var resultTrueDiag = symmetricCoordinationalMatrix.LMult(vector, true);
            Vector resultActualTrueDiag = new Vector(new double[] { 1, 6, 8 });

            var resultFalseDiag = symmetricCoordinationalMatrix.LMult(vector, false);
            Vector resultActualFalseDiag = new Vector(new double[] { 1, 5, 6 });

            for (int i = 0; i < resultTrueDiag.Size; i++)
            {
                Assert.Equal(resultTrueDiag[i], resultActualTrueDiag[i], 8);
                Assert.Equal(resultFalseDiag[i], resultActualFalseDiag[i], 8);
            }
        }

        [Fact]
        public void SymmetricCoordinationalMatrix_TestUMult()
        {
            var resultTrueDiag = symmetricCoordinationalMatrix.UMult(vector, true);
            Vector resultActualTrueDiag = new Vector(new double[] { 10, 2, 3 });

            var resultFalseDiag = symmetricCoordinationalMatrix.UMult(vector, false);
            Vector resultActualFalseDiag = new Vector(new double[] { 10, 1, 1 });

            for (int i = 0; i < resultTrueDiag.Size; i++)
            {
                Assert.Equal(resultTrueDiag[i], resultActualTrueDiag[i], 8);
                Assert.Equal(resultFalseDiag[i], resultActualFalseDiag[i], 8);
            }
        }

        [Fact]
        public void SymmetricCoordinationalMatrix_TestLSolve()
        {
            IVector resultActual = new Vector(new double[] { 1, 1, 1 });
            IVector vector = symmetricCoordinationalMatrix.LMult(resultActual, true);

            var result = symmetricCoordinationalMatrix.LSolve(vector, true);

            for (int i = 0; i < result.Size; i++)
                Assert.Equal(result[i], resultActual[i], 8);
        }

        [Fact]
        public void SymmetricCoordinationalMatrix_TestUSolve()
        {
            IVector resultActual = new Vector(new double[] { 1, 1, 1 });
            IVector vector = symmetricCoordinationalMatrix.UMult(resultActual, true);

            var result = symmetricCoordinationalMatrix.USolve(vector, true);

            for (int i = 0; i < result.Size; i++)
                Assert.Equal(result[i], resultActual[i], 8);
        }

        [Fact]
        public void SymmetricCoordinationalMatrix_TestMultiply()
        {
            var result = symmetricCoordinationalMatrix.Multiply(vector);
            Vector resultActual = new Vector(new double[] { 10, 6, 8 });

            for (int i = 0; i < resultActual.Size; i++)
                Assert.Equal(result[i], resultActual[i], 8);
        }


        [Fact]
        public void SymmetricCoordinationalMatrix_Foreach()
        {
            size = 3;
            var values = new double[] { 1, 4, 5, 4, 2, 5, 3 };
            columns = new int[] { 0, 1, 2, 0, 1, 0, 2 };
            rows = new int[] { 0, 0, 0, 1, 1, 2, 2 };

            CoordinationalMatrix coordinationalMatrix = new CoordinationalMatrix(rows, columns, values, size);

            Assert.True(new HashSet<(double, int, int)>(symmetricCoordinationalMatrix).SetEquals(coordinationalMatrix));
        }

        [Fact]
        public void CoordinationalMatrix_Fill()
        {
            FillFunc fillFunc = (row, col) => { return (row + 1) + (col + 1); };

            // ругается на коллекцию  "Коллекция была изменена; невозможно выполнить операцию перечисления."
            symmetricCoordinationalMatrix.Fill(fillFunc);

            size = 3;
            values = new double[] { 2, 3, 4, 4, 6 };
            columns = new int[] { 0, 0, 1, 0, 2 };
            rows = new int[] { 0, 1, 1, 2, 2 };

            SymmetricCoordinationalMatrix coordinat = new SymmetricCoordinationalMatrix(rows, columns, values, size);
            Assert.True(new HashSet<(double, int, int)>(symmetricCoordinationalMatrix).SetEquals(coordinat));

        }
    }
}
