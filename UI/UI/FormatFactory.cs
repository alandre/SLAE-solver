using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SolverCore;

namespace UI
{
   // public enum Formats { Coordinational = 0, Dense = 1, Skyline = 2, SparseRow = 3, SparseRowColumn = 4}

    public class FormatFactory
    {
        public FormatFactory()
        {
            formats.Add("Координатный", "Coordinational");
            formats.Add("Плотный", "Dense");
            formats.Add("Профильный", "Skyline");
            formats.Add("Строчно-столбцовый без выделенной диагонали", "SparseRow");
            formats.Add("Строчно-стобцовый", "SparseRowColumn");
        }
        public Dictionary<string, string> formats = new Dictionary<string, string>();
        
        public static IMatrix Init(string type, MatrixInitialazer initialazer, bool symmetry)
        {
            
            try
            {
                if (symmetry)
                    switch (type)
                    {
                        case "Coordinational":
                            {
                                // SymmetricCoordinationalMatrix matrix = new SymmetricCoordinationalMatrix();
                            }
                            break;
                        case "Dense":
                                return new SymmetricDenseMatrix(initialazer.denseL);
                        case "Skyline":
                                return new SymmetricSkylineMatrix(initialazer.di, initialazer.ig, initialazer.gg);
                        case "SparseRow":
                                return new SymmetricSparseRowMatrix(initialazer.gg, initialazer.jg, initialazer.ig);
                        case "SparseRowColumn":
                                return new SymmetricSparseRowColumnMatrix(initialazer.di, initialazer.gg, initialazer.ig, initialazer.ig);
                        default:
                            break;
                    }
                else
                    switch (type)
                {
                    case "Coordinational":
                        {
                            var list = initialazer.column.Select((c, i) => (c, initialazer.row[i], initialazer.gg[i])).ToList();
                            return new CoordinationalMatrix(list, initialazer.size);
                        }
                    case "Dense":
                            return new DenseMatrix(initialazer.dense);
                    case "Skyline":
                            return new SkylineMatrix(initialazer.di, initialazer.ig, initialazer.gl, initialazer.gl);
                    case "SparseRow":
                            return new SparseRowMatrix(initialazer.gg, initialazer.jg, initialazer.ig);
                    case "SparseRowColumn":
                            return new SparseRowColumnMatrix(initialazer.di, initialazer.gl, initialazer.gu, initialazer.ig, initialazer.jg);
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        }

        public static bool patternRequired(string type)
        {
            switch (type)
            {
                case "Координатный":
                    return false;
                case "Плотный":
                    return false;
                default:
                    break;
            }
            return true;
        }
    }
}
