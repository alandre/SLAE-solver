using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UI
{
    class MatrixInitialazer
    {
        public int[] column { get; set; }
        public int[] row { get; set; }

        public double[,] dense { get; set; }
        public double[][] denseL { get; set; }

        public int[] ig { get; set; }
        public int[] jg { get; set; }

        public double[] gg { get; set; }
        public double[] gl { get; set; }
        public double[] gu { get; set; }

        public double[] di { get; set; }

        public double[] x0 { get; set; }

        public bool symmetry { get; set; }

        public int size { set; get; }

        public static MatrixInitialazer Input(string data, MatrixInitialazer matrix, bool symmetry)
        {
            matrix = JsonConvert.DeserializeObject<MatrixInitialazer>(data);
            matrix.symmetry = symmetry;
            return matrix;
        }
    }   
}
