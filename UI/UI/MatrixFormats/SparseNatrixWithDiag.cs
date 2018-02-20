using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.MatrixFormats
{
    [Serializable]
    class SparseNatrixWithDiag
    {
        public int[] ai { get; set; }
        public int[] ja { get; set; }
        public double[] di { get; set; }
        public double[] gg { get; set; }

        public double[] gl { get; set; }
        public double[] gu { get; set; }

        public double[] x0 { get; set; }
        public double eps { get; set; }

        public bool symmetry { get; set; }

        public static SparseNatrixWithDiag SetDefaultx0(SparseNatrixWithDiag matrix)
        {
            double[] x = new double[matrix.di.Length];
            return matrix;
        }
    }
}
