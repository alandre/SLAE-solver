using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UI.MatrixFormats;

namespace UI
{
    class MatrixInitialazer
    {
        public DenseMatrix dense { get; set; }

        public CoordinateMatrix coordinate { get; set; }

        public SparseMatrixWithOutDiag sparseWithOutDiag { get; set; }

        public SparseNatrixWithDiag sparseWithDiag { get; set; }

        public static DenseMatrix Input(string data, DenseMatrix matrix)
        {
            return JsonConvert.DeserializeObject<DenseMatrix>(data);
        }

        public static CoordinateMatrix Input(string data, CoordinateMatrix matrix)
        {
            return JsonConvert.DeserializeObject<CoordinateMatrix>(data);
        }

        public static SparseMatrixWithOutDiag Input(string data, SparseMatrixWithOutDiag matrix)
        {
            return JsonConvert.DeserializeObject<SparseMatrixWithOutDiag>(data);
        }

        public static SparseNatrixWithDiag Input(string data, SparseNatrixWithDiag matrix)
        {
            return JsonConvert.DeserializeObject<SparseNatrixWithDiag>(data);
        }
    }   
}
