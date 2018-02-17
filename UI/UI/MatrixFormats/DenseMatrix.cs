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


    }
}
