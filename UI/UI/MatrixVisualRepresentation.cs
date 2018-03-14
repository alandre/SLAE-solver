using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SolverCore;
using System.Drawing;

namespace UI
{
    static class MatrixVisualRepresentation
    {
        enum CellTag
        {
            ForcedSignficant,
            Significant,
            Nonsignificant
        }

        public static void GenerateInitialPattern(ref DataGridView gridView)
        {
            foreach (DataGridViewRow row in gridView.Rows)
                foreach (DataGridViewCell cell in row.Cells)
                {
                    DataGridViewCell scell = gridView.Rows[cell.ColumnIndex].Cells[cell.RowIndex];
                    if (cell.Value.ToString() != "0" || scell.Value.ToString() != "0")
                    {
                        cell.Tag = CellTag.ForcedSignficant;
                        scell.Tag = CellTag.ForcedSignficant;
                    }
                    else
                        cell.Tag = CellTag.Nonsignificant;
                }
        }

        private static void AddElementToPattern(ref DataGridView gridView, int i, int j)
        {
            gridView.Rows[i].Cells[j].Tag = CellTag.Significant;
            gridView.Rows[j].Cells[i].Tag = CellTag.Significant;
        }

        private static void RemoveElementFromPattern(ref DataGridView gridView, int i, int j)
        {
            gridView.Rows[i].Cells[j].Tag = CellTag.Nonsignificant;
            gridView.Rows[j].Cells[i].Tag = CellTag.Nonsignificant;
        }

        public static void InverseElementPatternStatus(ref DataGridView gridView, int i, int j)
        {
            if ((CellTag)gridView.Rows[i].Cells[j].Tag == CellTag.Nonsignificant)
                AddElementToPattern(ref gridView, i, j);
            else if ((CellTag)gridView.Rows[i].Cells[j].Tag == CellTag.Significant)
                RemoveElementFromPattern(ref gridView, i, j);
        }

        public static CoordinationalMatrix GridViewToCoordinational(DataGridView gridView)
        {
            Dictionary<(int row, int column), double> matrix = new Dictionary<(int row, int column), double>();
            int n = gridView.ColumnCount;

            foreach (DataGridViewRow row in gridView.Rows)
                foreach (DataGridViewCell cell in row.Cells)
                    if (cell.Value.ToString() != "0")
                        matrix.Add((cell.RowIndex, cell.ColumnIndex), Double.Parse(cell.Value.ToString()));

            return new CoordinationalMatrix(matrix.Select(x => (x.Key.row, x.Key.column, x.Value)), n);
        }

        public static CoordinationalMatrix PatternedGridViewToCoordinational(DataGridView gridView)
        {
            Dictionary<(int row, int column), double> matrix = new Dictionary<(int row, int column), double>();
            int n = gridView.ColumnCount;

            foreach (DataGridViewRow row in gridView.Rows)
                foreach (DataGridViewCell cell in row.Cells)
                    if ((CellTag)cell.Tag == CellTag.ForcedSignficant || (CellTag)cell.Tag == CellTag.Significant)
                        matrix.Add((cell.RowIndex, cell.ColumnIndex), Double.Parse(cell.Value.ToString()));

            return new CoordinationalMatrix(matrix.Select(x => (x.Key.row, x.Key.column, x.Value)), n);
        }

        public static void PaintPattern(ref DataGridView gridView, Color color)
        {
            foreach (DataGridViewRow row in gridView.Rows)
                foreach (DataGridViewCell cell in row.Cells)
                    if ((CellTag)cell.Tag == CellTag.ForcedSignficant || (CellTag)cell.Tag == CellTag.Significant)
                        cell.Style.BackColor = color;
            else
                        cell.Style.BackColor = Color.White;
        }

        public static void CopyDataGridView(DataGridView org, ref DataGridView copy)
        {
            copy.Columns.Clear();
            copy.Rows.Clear();

            foreach (DataGridViewColumn col in org.Columns)
                copy.Columns.Add(col.Clone() as DataGridViewColumn);

            foreach (DataGridViewRow row in org.Rows)
            {
                DataGridViewRow nrow = row.Clone() as DataGridViewRow;
                int i = 0;
                foreach (DataGridViewCell cell in row.Cells)
                {
                    nrow.Cells[i].Value = cell.Value;
                    i++;
                }
                copy.Rows.Add(nrow);
            }

            copy.Width = org.Width;
            copy.Height = org.Height;
        }
    }
}
