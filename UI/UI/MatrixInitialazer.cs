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

        public SparseMatrixWithoutDiag SparseMatrixWithoutDiag { get; set; }

        public SparseNatrixWithDiag sparseWithDiag { get; set; }

        public static DenseMatrix Input(string data, DenseMatrix matrix, bool symmetry)
        {
            matrix = JsonConvert.DeserializeObject<DenseMatrix>(data);
            matrix.symmetry = symmetry;
            if (matrix.x0 == null)
                DenseMatrix.SetDefaultx0(matrix);
            return matrix;
        }

        public static CoordinateMatrix Input(string data, CoordinateMatrix matrix, bool symmetry)
        {
            matrix = JsonConvert.DeserializeObject<CoordinateMatrix>(data);
            matrix.symmetry = symmetry;
            if (matrix.x0 == null)
                CoordinateMatrix.SetDefaultx0(matrix);
            return matrix;
        }

        public static SparseMatrixWithoutDiag Input(string data, SparseMatrixWithoutDiag matrix, bool symmetry)
        {
            matrix = JsonConvert.DeserializeObject<SparseMatrixWithoutDiag>(data);
            matrix.symmetry = symmetry;
            if (matrix.x0 == null)
                SparseMatrixWithoutDiag.SetDefaultx0(matrix);
            return matrix;
        }

        public static SparseNatrixWithDiag Input(string data, SparseNatrixWithDiag matrix, bool symmetry)
        {
            matrix = JsonConvert.DeserializeObject<SparseNatrixWithDiag>(data);
            matrix.symmetry = symmetry;
            if (matrix.x0 == null)
                SparseNatrixWithDiag.SetDefaultx0(matrix);
            return matrix;
        }
    }   
}
