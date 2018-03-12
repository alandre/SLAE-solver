using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class PatternForm : Form
    {
        Dictionary<(int row, int column), double> matrix = new Dictionary<(int row, int column), double>();
        int width, heigth;

        public PatternForm()
        {
            InitializeComponent();
        }

        public PatternForm(DataGridView mat, int w, int h)
        {
            InitializeComponent();

            CopyDataGridView(mat, A);

            width = w;
            heigth = h;

        }

        private void CopyDataGridView(DataGridView org, DataGridView copy)
        {
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

        private void A_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            //
        }

        private void A_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = ((DataGridView)sender).Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (cell.Value.ToString() == "0")
            {
                if (cell.Style.BackColor == Color.SteelBlue)
                {
                    matrix.Remove((cell.RowIndex, cell.ColumnIndex));
                    cell.Style.BackColor = Color.White;
                }
                else
                {
                    matrix.Add((cell.RowIndex, cell.ColumnIndex), 0.0);
                    cell.Style.BackColor = Color.SteelBlue;
                }
            }
        }

        private void PatternForm_Load(object sender, EventArgs e)
        {
            Width = Width > 115 + width ? Width : 115 + width;
            Height += heigth;
            MaximumSize = Size;
            MinimumSize = Size;
            A.ReadOnly = true;
            BuildMatrix();
            infoTextBox.Width = Width - 40;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void A_SelectionChanged(object sender, EventArgs e)
        {
            A.ClearSelection();
        }

        private void далееToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void BuildMatrix()
        {
            foreach (DataGridViewRow row in A.Rows)
                foreach (DataGridViewCell cell in row.Cells)
                    if (cell.Value.ToString() != "0")
                    {
                        matrix.Add((cell.RowIndex, cell.ColumnIndex), Double.Parse(cell.Value.ToString()));
                        cell.Style.BackColor = Color.SteelBlue;
                    }
        }
    }
}
