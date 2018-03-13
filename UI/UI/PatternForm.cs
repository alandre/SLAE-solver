using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SolverCore;

namespace UI
{
    public partial class PatternForm : Form
    {
        Dictionary<(int row, int column), double> matrix;
        int width, heigth;
        bool symmetric = true;

        ConstructorForm SLAESource;
        MainForm mainForm;

        public PatternForm()
        {
            InitializeComponent();
        }

        private void CopyDataGridView(DataGridView org, DataGridView copy)
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

        private void A_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            //
        }

        private void A_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = ((DataGridView)sender).Rows[e.RowIndex].Cells[e.ColumnIndex];
            DataGridViewCell scell = ((DataGridView)sender).Rows[e.ColumnIndex].Cells[e.RowIndex];
            if (cell.Value.ToString() == "0" && scell.Value.ToString() == "0")
            {
                if (cell.Style.BackColor == Color.SteelBlue)
                {
                    matrix.Remove((cell.RowIndex, cell.ColumnIndex));
                    cell.Style.BackColor = Color.White;

                    if (symmetric)
                    {
                        matrix.Remove((cell.ColumnIndex, cell.RowIndex));
                        ((DataGridView)sender).Rows[e.ColumnIndex].Cells[e.RowIndex].Style.BackColor = Color.White;
                    }
                }
                else
                {
                    matrix.Add((cell.RowIndex, cell.ColumnIndex), 0.0);
                    cell.Style.BackColor = Color.SteelBlue;

                    if (symmetric)
                    {
                        matrix.Add((cell.ColumnIndex, cell.RowIndex), 0.0);
                        ((DataGridView)sender).Rows[e.ColumnIndex].Cells[e.RowIndex].Style.BackColor = Color.SteelBlue;
                    }
                }
            }
        }

        private void PatternForm_Load(object sender, EventArgs e)
        {
            SLAESource = (ConstructorForm)(Owner.Owner);
            mainForm = (MainForm)(Owner.Owner.Owner);

            infoTextBox.Width = Width - 40;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void A_SelectionChanged(object sender, EventArgs e)
        {
            A.ClearSelection();
        }

        private void forwardToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            IVector b, x0;
            int n;
            SLAESource.GetSLAEParams(out n, out b, out x0);
            mainForm.SetSLAE(new CoordinationalMatrix(matrix.Select(x => (x.Key.row, x.Key.column, x.Value)), n), b, x0);
            Close();
        }

        private void PatternForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (e.CloseReason == CloseReason.UserClosing)
            //{
            //    e.Cancel = true;
            //    Hide();
            //}
            if (e.CloseReason == CloseReason.UserClosing)
                mainForm.Show();
        }

        private void PatternForm_Shown(object sender, EventArgs e)
        {
        }

        private void backwardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Owner.Show();
            Hide();
        }

        public void Update()
        {
            Location = Owner.Location;

            DataGridView mat = new DataGridView();

            SLAESource.GetGridParams(ref mat, ref width, ref heigth);
            CopyDataGridView(mat, A);

            Size size = new Size(Width > 115 + width ? Width : 115 + width, 190 + heigth);
            MaximumSize = size;
            MinimumSize = size;
            Size = size;
            A.ReadOnly = true;
            BuildMatrix();
        }

        private void BuildMatrix()
        {
            matrix = new Dictionary<(int row, int column), double>();

            foreach (DataGridViewRow row in A.Rows)
                foreach (DataGridViewCell cell in row.Cells)
                    if (cell.Value.ToString() != "0")
                    {
                        matrix.Add((cell.RowIndex, cell.ColumnIndex), Double.Parse(cell.Value.ToString()));
                        cell.Style.BackColor = Color.SteelBlue;

                        if (symmetric && !matrix.ContainsKey((cell.ColumnIndex,cell.RowIndex)))
                        {
                            DataGridViewCell symcell = A.Rows[cell.ColumnIndex].Cells[cell.RowIndex];
                            matrix.Add((symcell.RowIndex, symcell.ColumnIndex), Double.Parse(symcell.Value.ToString()));
                            symcell.Style.BackColor = Color.SteelBlue;
                        }
                    }
        }
    }
}
