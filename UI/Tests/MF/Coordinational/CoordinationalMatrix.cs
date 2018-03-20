using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;
using SolverCore;
using UI;

namespace MF.Coordinational
{
    public class TestCoordinationalMatrix
    {

        private int[] rows;
        private int[] columns;

        private double[] values;
        private int size;

        private IVector vector;

        private CoordinationalMatrix coordinationalMatrix;

        // TODO сделать короче названия методов

        public TestCoordinationalMatrix()
        {
            size = 3;
            values = new double[] { 1, 4, 5, 6, 2, 8, 3 };
            columns = new int[] { 0, 1, 2, 0, 1, 0, 2 };
            rows = new int[] { 0, 0, 0, 1, 1, 2, 2 };

            vector = new Vector(new double[] { 1, 1, 1 });

            coordinationalMatrix = new CoordinationalMatrix(rows, columns, values, size);
        }

        [Theory]
        [InlineData(FormatFactory.Formats.Coordinational)]
        [InlineData(FormatFactory.Formats.Dense)]
        [InlineData(FormatFactory.Formats.Skyline)]
        [InlineData(FormatFactory.Formats.SparseRow)]
        [InlineData(FormatFactory.Formats.SparseRowColumn)]
        public void CoordinationalMatrix_TestConstructor(FormatFactory.Formats type)
        {

            var exploredMatrix = FormatFactory.Convert(coordinationalMatrix, type);
            var backCoordMatrix = exploredMatrix.ConvertToCoordinationalMatrix();
            Assert.True(new HashSet<(double, int, int)>(coordinationalMatrix).SetEquals(backCoordMatrix));
            //Assert.True((coordinationalMatrix).Equals(backCoordMatrix));





            //var formatFactory = new FormatFactory();
            //
            //foreach (var type in formatFactory.formats)
            //{
            //    var exploredMatrix = FormatFactory.Convert(coordinationalMatrix, type.Key);
            //    var backCoordMatrix = exploredMatrix.ConvertToCoordinationalMatrix();
            //    Assert.True(new HashSet<(double, int, int)>(coordinationalMatrix).SetEquals(backCoordMatrix));
            //}
        }

       


        [Fact]
        public void CoordinationalMatrix_TestLMult()
        {
            var resultTrueDiag = coordinationalMatrix.LMult(vector, true);
            Vector resultActualTrueDiag = new Vector(new double[] { 1, 8, 11 });

            var resultFalseDiag = coordinationalMatrix.LMult(vector, false);
            Vector resultActualFalseDiag = new Vector(new double[] { 1, 7, 9 });

            for (int i = 0; i < resultTrueDiag.Size; i++)
            {
                Assert.Equal(resultTrueDiag[i], resultActualTrueDiag[i], 8);
                Assert.Equal(resultFalseDiag[i], resultActualFalseDiag[i], 8);
            }
        }

        [Fact]
        public void CoordinationalMatrix_TestUMult()
        {
            var resultTrueDiag = coordinationalMatrix.UMult(vector, true);
            Vector resultActualTrueDiag = new Vector(new double[] { 10, 2, 3 });

            var resultFalseDiag = coordinationalMatrix.UMult(vector, false);
            Vector resultActualFalseDiag = new Vector(new double[] { 10, 1, 1 });

            for (int i = 0; i < resultTrueDiag.Size; i++)
            {
                Assert.Equal(resultTrueDiag[i], resultActualTrueDiag[i], 8);
                Assert.Equal(resultFalseDiag[i], resultActualFalseDiag[i], 8);
            }
        }

        [Fact]
        public void CoordinationalMatrix_TestLSolve()
        {
            IVector resultActual = new Vector(new double[] { 1, 1, 1 });
            IVector vector = coordinationalMatrix.LMult(resultActual, true);

            var result = coordinationalMatrix.LSolve(vector, true);

            for (int i = 0; i < result.Size; i++)
                Assert.Equal(result[i], resultActual[i], 8);
        }

        [Fact]
        public void CoordinationalMatrix_TestUSolve()
        {
            IVector resultActual = new Vector(new double[] { 1, 1, 1 });
            IVector vector = coordinationalMatrix.UMult(resultActual, true);

            var result = coordinationalMatrix.USolve(vector, true);

            for (int i = 0; i < result.Size; i++)
                Assert.Equal(result[i], resultActual[i], 8);
        }

        [Fact]
        public void CoordinationalMatrix_TestMultiply()
        {
            var result = coordinationalMatrix.Multiply(vector);
            Vector resultActual = new Vector(new double[] { 10, 8, 11 });

            for (int i = 0; i < resultActual.Size; i++)
                Assert.Equal(result[i], resultActual[i], 8);
        }

        [Fact]
        public void CoordinationalMatrix_TestMultiplyTranspose()
        {
            var result = coordinationalMatrix.MultiplyTranspose(vector);
            Vector resultActual = new Vector(new double[] { 15, 6, 8 });

            for (int i = 0; i < resultActual.Size; i++)
                Assert.Equal(result[i], resultActual[i], 8);
        }

        [Fact]
        public void CoordinationalMatrix_TestUMultTranspose()
        {
            var resultTrueDiag = coordinationalMatrix.UMultTranspose(vector, true);
            Vector resultActualTrueDiag = new Vector(new double[] { 1, 6, 8 });

            var resultFalseDiag = coordinationalMatrix.UMultTranspose(vector, false);
            Vector resultActualFalseDiag = new Vector(new double[] { 1, 5, 6 });


            for (int i = 0; i < resultTrueDiag.Size; i++)
            {
                Assert.Equal(resultTrueDiag[i], resultActualTrueDiag[i], 8);
                Assert.Equal(resultFalseDiag[i], resultActualFalseDiag[i], 8);
            }
        }

        [Fact]
        public void CoordinationalMatrix_TestLMultTranspose()
        {
            var resultTrueDiag = coordinationalMatrix.LMultTranspose(vector, true);
            Vector resultActualTrueDiag = new Vector(new double[] { 15, 2, 3 });

            var resultFalseDiag = coordinationalMatrix.LMultTranspose(vector, false);
            Vector resultActualFalseDiag = new Vector(new double[] { 15, 1, 1 });

            for (int i = 0; i < resultTrueDiag.Size; i++)
            {
                Assert.Equal(resultTrueDiag[i], resultActualTrueDiag[i], 8);
                Assert.Equal(resultFalseDiag[i], resultActualFalseDiag[i], 8);
            }
        }

        [Fact]
        public void CoordinationalMatrix_TestLSolveTranspose()
        {
            IVector resultActual = new Vector(new double[] { 1, 6, 8 });
            IVector vector = coordinationalMatrix.LMultTranspose(resultActual, true);

            var result = coordinationalMatrix.LSolveTranspose(vector, true);

            for (int i = 0; i < result.Size; i++)
                Assert.Equal(result[i], resultActual[i], 8);
        }

        [Fact]
        public void CoordinationalMatrix_TestUSolveTranspose()
        {
            IVector resultActual = new Vector(new double[] { 15, 2, 3 });
            IVector vector = coordinationalMatrix.UMultTranspose(resultActual, true);

            var result = coordinationalMatrix.USolveTranspose(vector, true);

            for (int i = 0; i < result.Size; i++)
                Assert.Equal(result[i], resultActual[i], 8);
        }


        [Fact]
        public void CoordinationalMatrix_Fill()
        {
            FillFunc fillFunc = (row, col) => { return (row + 1) + (col + 1); };

            // ругается на коллекцию  "Коллекция была изменена; невозможно выполнить операцию перечисления."
            coordinationalMatrix.Fill(fillFunc);

            size = 3;
            values = new double[] { 2, 3, 4, 3, 4, 4, 6};
            columns = new int[] { 0, 1, 2, 0, 1, 0, 2 };
            rows = new int[] { 0, 0, 0, 1, 1, 2, 2 };

            CoordinationalMatrix coordinat = new CoordinationalMatrix(rows, columns, values, size);
            Assert.True(new HashSet<(double, int, int)>(coordinationalMatrix).SetEquals(coordinat));

        }

    }
}
