using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;
using SolverCore;
using Xunit.Abstractions;

namespace MF.SparseRow
{
    public class TestSparseRowMatrix
    {
        private double[] _a;
        private int[] _ia;
        private int[] _ja;
        private IVector vector;

        public static SparseRowMatrix sparseRowMatrix;
        private readonly ITestOutputHelper _testOutputHelper;
        public TestSparseRowMatrix(ITestOutputHelper testOutputHelper)
        {
            _a = new double[] { 1, 8, 2, 7, 2, 3, 6, 4 };
            _ia = new int[] { 0, 2, 4, 6, 8 };
            _ja = new int[] { 0, 2, 1, 3, 0, 2, 1, 3 };
            vector = new Vector(new double[] { 2, 1, 1, 1 });

            sparseRowMatrix = new SparseRowMatrix(_a, _ja, _ia);
            _testOutputHelper = testOutputHelper;
        }


        [Fact]
        public void Foreach()
        {


            List<(double, int, int)> elemList =
                new List<(double, int, int)>()
                {
                    (1,0,0),
                    (8,0,2),
                    (2,1,1),
                    (7,1,3),
                    (2,2,0),
                    (3,2,2),
                    (6,3,1),
                    (4,3,3),
                    
                };


            Assert.True(new HashSet<(double, int, int)>(sparseRowMatrix).SetEquals(elemList));

            foreach (var elem in sparseRowMatrix)
                _testOutputHelper.WriteLine(elem.ToString());
        }

        [Fact]
        public void LMult()
        {
            var resultTrueDiag = sparseRowMatrix.LMult(vector, true);
            Vector resultActualTrueDiag = new Vector(new double[] { 2, 2, 7, 10 });

            var resultFalseDiag = sparseRowMatrix.LMult(vector, false);
            Vector resultActualFalseDiag = new Vector(new double[] { 2, 1, 5, 7 });

            for (int i = 0; i < resultTrueDiag.Size; i++)
            {
                Assert.Equal(resultTrueDiag[i], resultActualTrueDiag[i], 8);
                Assert.Equal(resultFalseDiag[i], resultActualFalseDiag[i], 8);
            }
        }

       [Fact]
       public void UMult()
       {
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
       public void LSolve()
       {
           IVector resultActual = new Vector(new double[] { 1, 2, 3, 4 });
           IVector vector = sparseRowMatrix.LMult(resultActual, true);
      
           var result = sparseRowMatrix.LSolve(vector, true);
      
           for (int i = 0; i < result.Size; i++)
               Assert.Equal(result[i], resultActual[i], 8);
       }

        [Fact]
        public void USolve()
        {
            IVector resultActual = new Vector(new double[] { 1, 2, 3, 4 });
            IVector vector = sparseRowMatrix.UMult(resultActual, true);

            var result = sparseRowMatrix.USolve(vector, true);

            for (int i = 0; i < result.Size; i++)
                Assert.Equal(result[i], resultActual[i], 8);
        }

        [Fact]
        public void Multiply()
        {
            var result = sparseRowMatrix.Multiply(vector);
            Vector resultActual = new Vector(new double[] { 10, 9, 7, 10 });

            for (int i = 0; i < resultActual.Size; i++)
                Assert.Equal(result[i], resultActual[i], 8);
        }

        [Fact]
        public void UMultTranspose()
        {
            var resultTrueDiag = sparseRowMatrix.UMultTranspose(vector, true);
            Vector resultActualTrueDiag = new Vector(new double[] { 2, 2, 19, 11 });

            var resultFalseDiag = sparseRowMatrix.UMultTranspose(vector, false);
            Vector resultActualFalseDiag = new Vector(new double[] { 2, 1, 17, 8 });


            for (int i = 0; i < resultTrueDiag.Size; i++)
            {
                Assert.Equal(resultTrueDiag[i], resultActualTrueDiag[i], 8);
                Assert.Equal(resultFalseDiag[i], resultActualFalseDiag[i], 8);
            }
        }

        [Fact]
        public void LMultTranspose()
        {
            var resultTrueDiag = sparseRowMatrix.LMultTranspose(vector, true);
            Vector resultActualTrueDiag = new Vector(new double[] { 4, 8, 3, 4 });

            var resultFalseDiag = sparseRowMatrix.LMultTranspose(vector, false);
            Vector resultActualFalseDiag = new Vector(new double[] { 4, 7, 1, 1 });

            for (int i = 0; i < resultTrueDiag.Size; i++)
            {
                Assert.Equal(resultTrueDiag[i], resultActualTrueDiag[i], 8);
                Assert.Equal(resultFalseDiag[i], resultActualFalseDiag[i], 8);
            }
        }
        
        [Fact]
        public void LSolveTranspose()
        {
            IVector resultActual = new Vector(new double[] { 1, 2, 3, 4 });
            IVector vector = sparseRowMatrix.LMultTranspose(resultActual, true);

            var result = sparseRowMatrix.LSolveTranspose(vector, true);

            for (int i = 0; i < result.Size; i++)
                Assert.Equal(result[i], resultActual[i], 8);
        }
       
        [Fact]
        public void USolveTranspose()
        {
            IVector resultActual = new Vector(new double[] { 1, 2, 3, 4 });
            IVector vector = sparseRowMatrix.UMultTranspose(resultActual, true);

            var result = sparseRowMatrix.USolveTranspose(vector, true);

            for (int i = 0; i < result.Size; i++)
                Assert.Equal(result[i], resultActual[i], 8);
        }

        [Fact]
        public void Fill()
        {
            FillFunc fillFunc = (row, col) => { return (row + 1) + (col + 1); };
            
            sparseRowMatrix.Fill(fillFunc);

            _a = new double[] { 2, 4, 4, 6, 4, 6, 6, 8 };
            _ia = new int[] { 0, 2, 4, 6, 8 };
            _ja = new int[] { 0, 2, 1, 3, 0, 2, 1, 3 };


            SparseRowMatrix sparseRow = new SparseRowMatrix(_a, _ia, _ja);
            Assert.True(new HashSet<(double, int, int)>(sparseRowMatrix).SetEquals(sparseRow));

        }
    }
}
