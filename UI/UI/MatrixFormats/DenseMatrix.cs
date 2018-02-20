using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.MatrixFormats
{
    [Serializable]
    class DenseMatrix
    {
        public double[] denseL { get; set; }
        public double[] denseU { get; set; }
        public double[,] dense { get; set; }
        public double[] di { get; set; }

        public bool symmetry { get; set; }

        public double[] x0 { get; set; }
        public double eps { get; set; }

        public DenseMatrix()
        {
            this.eps = 1e-10;
        }

        public static DenseMatrix SetDefaultx0(DenseMatrix matrix)
        {

            if (matrix.symmetry)
            {
                double[] x = new double[matrix.dense.GetLength(0)];
                matrix.x0 = x;
            }
            else
            {
                double[] x = new double[matrix.di.Length];
                matrix.x0 = x;
            }
            return matrix;
        }

    }
}
