using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.MatrixFormats
{
    [Serializable]
    class CoordinateMatrix
    {
        public int[] rows { get; set; }
        public int[] cols { get; set; }
        public double[] gg { get; set; }

        public double[] gl { get; set; }
        public double[] gu { get; set; }

        public double[] x0 { get; set; }
        public double eps { get; set; }

        public bool symmetry { get; set; }

        public static CoordinateMatrix SetDefaultx0(CoordinateMatrix matrix)
        {
            int max = -1;
            int min = int.MaxValue;
            foreach (var elem in matrix.rows)
            {
                if (elem > max) max = elem;
                if (elem < min) min = elem;
            }
            if (min == 0)
            {
                double[] x = new double[max + 1];
                matrix.x0 = x;
            }
            else
            {
                double[] x = new double[max];
                matrix.x0 = x;
            }
            return matrix;
        }
    }


}
