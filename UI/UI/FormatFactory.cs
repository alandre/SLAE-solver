using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SolverCore;

namespace UI
{
    public class FormatFactory
    {
        public enum Formats { Coordinational = 0, Dense = 1, Skyline = 2, SparseRow = 3, SparseRowColumn = 4 }

        public static Dictionary<string, Formats> FormatsDictionary { get; } = new Dictionary<string, Formats>
        {
            {"Координатный", Formats.Coordinational},
            {"Плотный", Formats.Dense},
            {"Профильный", Formats.Skyline},
            {"Строчный без выделенной диагонали", Formats.SparseRow},
            {"Строчно-стобцовый", Formats.SparseRowColumn}
        };

        public static IMatrix Init(Formats type, MatrixInitialazer initialazer, bool symmetry)
        {
            try
            {
                if (symmetry)
                    switch (type)
                    {
                        case Formats.Coordinational:
                            return new SymmetricCoordinationalMatrix(initialazer.row, initialazer.column, initialazer.gg, initialazer.size);
                        case Formats.Dense:
                            return new SymmetricDenseMatrix(initialazer.denseL);
                        case Formats.Skyline:
                            return new SymmetricSkylineMatrix(initialazer.di, initialazer.ia, initialazer.gg);
                        case Formats.SparseRow:
                            return new SymmetricSparseRowMatrix(initialazer.gg, initialazer.ja, initialazer.ia);
                        case Formats.SparseRowColumn:
                            return new SymmetricSparseRowColumnMatrix(initialazer.di, initialazer.gg, initialazer.ia, initialazer.ia);
                        default:
                            throw new ArgumentOutOfRangeException(nameof(type), type, null);
                    }
                switch (type)
                {
                    case Formats.Coordinational:
                        return new CoordinationalMatrix(initialazer.row, initialazer.column, initialazer.gg, initialazer.size);
                    case Formats.Dense:
                        return new DenseMatrix(initialazer.dense);
                    case Formats.Skyline:
                        return new SkylineMatrix(initialazer.di, initialazer.ia, initialazer.al, initialazer.al);
                    case Formats.SparseRow:
                        return new SparseRowMatrix(initialazer.gg, initialazer.ja, initialazer.ia);
                    case Formats.SparseRowColumn:
                        return new SparseRowColumnMatrix(initialazer.di, initialazer.al, initialazer.au, initialazer.ia, initialazer.ja);
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        }

        public static bool PatternRequired(Formats type)
        {
            switch (type)
            {
                case Formats.Coordinational:
                case Formats.Dense:
                    return false;
                default:
                    return true;
            }
        }

        public static IMatrix Convert(CoordinationalMatrix matrix, Formats type)
        {
            switch (type)
            {
                case Formats.Coordinational:
                    return matrix;
                case Formats.Dense:
                    return new DenseMatrix(matrix);
                case Formats.Skyline:
                    return new SkylineMatrix(matrix);
                case Formats.SparseRow:
                    return new SparseRowMatrix(matrix);
                case Formats.SparseRowColumn:
                    return new SparseRowColumnMatrix(matrix);
                default:
                    return matrix;
            }
        }

        public static IMatrix Convert(SymmetricCoordinationalMatrix matrix, Formats type)
        {
            switch (type)
            {
                case Formats.Coordinational:
                    return matrix;
                case Formats.Dense:
                    return new SymmetricDenseMatrix(matrix);
                case Formats.Skyline:
                    return new SymmetricSkylineMatrix(matrix);
                case Formats.SparseRow:
                    return new SymmetricSparseRowMatrix(matrix);
                case Formats.SparseRowColumn:
                    return new SymmetricSparseRowColumnMatrix(matrix);
                default:
                    return matrix;
            }
        }
    }
}
