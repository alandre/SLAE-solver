using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SolverCore;

namespace UI
{
    public enum Formats { Coordinational = 0, Dense = 1, Skyline = 2, SparseRow = 3, SparseRowColumn = 4}

    class FormatFactory
    {
        public static IMatrix Init(Formats type, MatrixInitialazer initialazer, bool symmetry)
        {
            try
            {
                if (symmetry)
                    switch (type)
                    {
                        case Formats.Coordinational:
                            {
                                // SymmetricCoordinationalMatrix matrix = new SymmetricCoordinationalMatrix();
                            }
                            break;
                        case Formats.Dense:
                                return new SymmetricDenseMatrix(initialazer.denseL);
                        case Formats.Skyline:
                                return new SymmetricSkylineMatrix(initialazer.di, initialazer.ig, initialazer.gg);
                        case Formats.SparseRow:
                                return new SymmetricSparseRowMatrix(initialazer.gg, initialazer.jg, initialazer.ig);
                        case Formats.SparseRowColumn:
                                return new SymmetricSparseRowColumnMatrix(initialazer.di, initialazer.gg, initialazer.ig, initialazer.ig);
                        default:
                            break;
                    }
                else
                    switch (type)
                {
                    case Formats.Coordinational:
                        {
                            var list = initialazer.column.Select((c, i) => (c, initialazer.row[i], initialazer.gg[i])).ToList();
                            return new CoordinationalMatrix(list, initialazer.size);
                        }
                    case Formats.Dense:
                            return new DenseMatrix(initialazer.dense);
                    case Formats.Skyline:
                            return new SkylineMatrix(initialazer.di, initialazer.ig, initialazer.gl, initialazer.gl);
                    case Formats.SparseRow:
                            return new SparseRowMatrix(initialazer.gg, initialazer.jg, initialazer.ig);
                    case Formats.SparseRowColumn:
                            return new SparseRowColumnMatrix(initialazer.di, initialazer.gl, initialazer.gu, initialazer.ig, initialazer.jg);
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
