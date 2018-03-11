using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SolverCore;

namespace UI
{
    public enum FormatsEnum { Coordinational, Dense, Skyline, SparseRow, SparseRowColumn }
    class FormatFactory
    {
        public static IMatrix Init(int type, MatrixInitialazer tmp, bool symmetry)
        {
            try
            {
                if (symmetry)
                    switch (type)
                    {
                        case 0:
                            {
                                // SymmetricCoordinationalMatrix matrix = new SymmetricCoordinationalMatrix();
                            }
                            break;
                        case 1:
                            {
                                SymmetricDenseMatrix matrix = new SymmetricDenseMatrix(tmp.denseL);
                            }
                            break;
                        case 2:
                            {
                                SymmetricSkylineMatrix matrix = new SymmetricSkylineMatrix(tmp.di, tmp.ig, tmp.gg);
                            }
                            break;
                        case 3:
                            {
                                SymmetricSparseRowMatrix matrix = new SymmetricSparseRowMatrix(tmp.gg, tmp.jg, tmp.ig);
                            }
                            break;
                        case 4:
                            {
                                SymmetricSparseRowColumnMatrix matrix = new SymmetricSparseRowColumnMatrix(tmp.di, tmp.gg, tmp.ig, tmp.ig);
                            }
                            break;
                        default:
                            break;
                    }
                switch (type)
                {
                    case 0:
                        {
                            var list = tmp.column.Select((c, i) => (c, tmp.row[i], tmp.gg[i])).ToList();
                            CoordinationalMatrix matrix = new CoordinationalMatrix(list, tmp.size);
                        }
                        break;
                    case 1:
                        {
                            DenseMatrix matrix = new DenseMatrix(tmp.dense);
                            IMatrix m = matrix;
                        }
                        break;
                    case 2:
                        {
                            SkylineMatrix matrix = new SkylineMatrix(tmp.di, tmp.ig, tmp.gl, tmp.gl);
                        }
                        break;
                    case 3:
                        {
                            SparseRowMatrix matrix = new SparseRowMatrix(tmp.gg, tmp.jg, tmp.ig);
                        }
                        break;
                    case 4:
                        {
                            SparseRowColumnMatrix matrix = new SparseRowColumnMatrix(tmp.di, tmp.gl, tmp.gu, tmp.ig, tmp.jg);
                        }
                        break;
                    default:
                        break;

                }
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        }
    }
}
