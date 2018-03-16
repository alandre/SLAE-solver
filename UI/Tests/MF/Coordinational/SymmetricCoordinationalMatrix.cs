using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;
using SolverCore;

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


        public TestSymmetricCoordinationalMatrix()
        {
            size = 3;
            values = new double[] { 1, 4, 2, 5, 3 };
            columns = new int[] { 0, 0, 1, 0, 2 };
            rows = new int[] { 0, 1, 1, 2, 2 };

            vector = new Vector(new double[] { 1, 1, 1 });

            symmetricCoordinationalMatrix = new SymmetricCoordinationalMatrix(rows, columns, values, size);
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
    }
}
