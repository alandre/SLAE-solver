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
    public partial class ConstructorForm : Form
    {
        int n = 2; // размерность матрицы
        int width, height; // базовые ширина и высота формы
        int cellWidth = 35, cellHeight; // ширина и высота ячеек
        bool symmetrized = false; // является ли матрица симметричной

        DataGridViewCell highlightedCell;

        FormatForm formatForm;

        public ConstructorForm()
        {
            InitializeComponent();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            int diff;
            bool inc = (diff = (int)numericUpDown1.Value - n) > 0;

            n = (int)numericUpDown1.Value;

            // изменение размеров формы в соответствии с размерностью матрицы

            Size size = new Size(width + cellWidth * (n - 2), height + cellHeight * (n - 2));
            MaximumSize = size;
            MinimumSize = size;
            Size = size;
            //Width = width + cellWidth * (n - 2);
            //Height = height + cellHeight * (n - 2);

            // изменение размера матрицы и векторов
            SizeChanged(diff);

            if (inc && checkBox1.Checked)
                // если установлен режим симметричной матрицы, 
                // добавленные элементы верхнего треугольника необходимо enable
                Symmetrize();
        }

        private void ConstructorForm_Load(object sender, EventArgs e)
        {
            width = this.Width;
            height = this.Height;
            F.RowTemplate.Height = cellHeight = A.RowTemplate.Height;

            toolTip1.SetToolTip(CleanX0_Btn, "Очистить");
            toolTip2.SetToolTip(CleanF_Btn, "Очистить");
            toolTip3.SetToolTip(CleanMatrix_Btn, "Очистить");

            InitializeSLAE();
        }

        private void InitializeSLAE()
        {
            F.Columns.Add("Column1", "");
            F.Columns[0].Width = cellWidth;

            SizeChanged(n);
        }

        private void SizeChanged(int diff)
        {
            A.Height = F.Height = n * cellHeight + 2;
            A.Width = x0.Width = n * cellWidth + 1;
            F.Width = cellWidth + 1;
            x0.Height = cellHeight + 1;

            // если размерность увеличилась 
            if (diff > 0)
            {
                int j0 = n - diff;
                for (int j = j0; j < n; ++j)
                {
                    A.Columns.Add("Column" + (j + 1).ToString(), "");
                    A.Columns[j].Width = cellWidth;
                    x0.Columns.Add("Column" + (j + 1).ToString(), "");
                    x0.Columns[j].Width = cellWidth;
                }

                A.Rows.Add(diff);
                Clean(A, 0, j0);
                Clean(A, j0, 0);

                x0.RowCount = 1;
                Clean(x0, 0, j0);

                F.Rows.Add(diff);
                Clean(F, j0, 0);
            }
            // если размерность уменьшилась
            else
            {
                for (int j = n - diff - 1; j >= n; --j)
                {
                    A.Columns.RemoveAt(j);
                    A.Rows.RemoveAt(j);
                    F.Rows.RemoveAt(j);
                    x0.Columns.RemoveAt(j);
                }
            }
        }

        // обнуление элементов матрицы/вектора
        private void Clean(DataGridView sender, int i0, int j0)
        {
            int ni = sender.RowCount;
            int nj = sender.ColumnCount;

            for (int i = i0; i < ni; i++)
                for (int j = j0; j < nj; j++)
                    sender.Rows[i].Cells[j].Value = 0.0;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                bool conflicts = false;

                for (int i = 0; i < n; i++)
                    for (int j = i + 1; j < n; j++)
                        if (A.Rows[i].Cells[j].Value.ToString() != A.Rows[j].Cells[i].Value.ToString()
                            && A.Rows[i].Cells[j].Value.ToString() != "0")
                            conflicts = true;

                if (conflicts)
                    if(MessageBox.Show("Введенная матрица не является симметричной. " +
                        "Заполнить верхний треугольник матрицы в соответствии с нижним?",
                        "Предупреждение", MessageBoxButtons.YesNo) == DialogResult.No)
                        return;

                Symmetrize();
            }
            else
                Unsymmetrize();
        }

        private void Symmetrize()
        {
            for (int i = 0; i < n; i++)
                for (int j = i + 1; j < n; j++)
                {
                    A.Rows[i].Cells[j].ReadOnly = true;
                    A.Rows[i].Cells[j].Style.BackColor = Color.LightGray;
                    A.Rows[i].Cells[j].Value = A.Rows[j].Cells[i].Value;
                }

            symmetrized = true;
        }
        
        private void CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (symmetrized)
            {
                int j = e.ColumnIndex;
                int i = e.RowIndex;

                A.Rows[j].Cells[i].Value = A.Rows[i].Cells[j].Value;
            }

            forwardToolStripMenuItem1.Enabled = true;
        }

        private void CleanMatrix_Btn_Click(object sender, EventArgs e)
        {
            Clean(A,0,0);
        }

        private void CleanF_Btn_Click(object sender, EventArgs e)
        {
            Clean(F,0,0);
        }

        private void CleanX0_Btn_Click(object sender, EventArgs e)
        {
            Clean(x0,0,0);
        }

        private void cancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            abortEdit(A);
            abortEdit(F);
            abortEdit(x0);

            Owner.Show();
            this.Hide();
        }

        private void abortEdit(DataGridView dataGridView)
        {
            if (dataGridView.IsCurrentCellInEditMode)
            {
                var old = dataGridView.CurrentCell.FormattedValue;
                dataGridView.EndEdit();
                dataGridView.CurrentCell.Value = old;
            }
        }

        private void forwardToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (formatForm == null || formatForm.IsDisposed)
                formatForm = new FormatForm();
            
            formatForm.Owner = this;
            formatForm.Show();
            Hide();
        }

        private void Unsymmetrize()
        {
            symmetrized = false;

            for (int i = 0; i < n; i++)
                for (int j = i + 1; j < n; j++)
                {
                    A.Rows[i].Cells[j].ReadOnly = false;
                    A.Rows[i].Cells[j].Style.BackColor = Color.White;
                    A.Rows[i].Cells[j].Value = (double)0;
                }
        }

        private void ConstructorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
            Owner.Show();
        }

        private void ConstructorForm_Shown(object sender, EventArgs e)
        {
            Location = Owner.Location;
        }

        private void A_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (!Double.TryParse(e.FormattedValue.ToString(), out double result))
            {
                MessageBox.Show("Введите корректное значение.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                A.EditingControl.Text = "0";
            }
        }

        private void F_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (!Double.TryParse(e.FormattedValue.ToString(), out double result))
            {
                MessageBox.Show("Введите корректное значение.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                F.EditingControl.Text = "0";
            }
        }

        private void x0_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (!Double.TryParse(e.FormattedValue.ToString(), out double result))
            {
                MessageBox.Show("Введите корректное значение.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                x0.EditingControl.Text = "0";
            }
        }

        private void A_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            // Если ячейка read-only, принудительный TAB 
            if (((DataGridView)sender).Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (!double.TryParse(e.FormattedValue.ToString(), out double res))
            {
                e.Cancel = true;
                //((DataGridView)sender).Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "Введенное значение не является вещественным числом.";
            }
        }

        private void timerCellHighlight_Tick(object sender, EventArgs e)
        {
            highlightedCell.Style.BackColor = Color.White;
        }

        private void CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            forwardToolStripMenuItem1.Enabled = false;
        }

        public void GetGridParams(ref DataGridView mat, ref int w, ref int h)
        {
            mat = A;
            w = cellWidth * (n - 2);
            h = cellHeight * (n - 2);
        }

        public void GetSLAEParams(out int _n, /*out IMatrix _A,*/ out IVector _b, out IVector _x0)
        {
            _n = n;
            _b = new Vector(n);
            _x0 = new Vector(n);
            
            for (int i = 0; i < n; i++)
            {
                _b[i] = double.Parse(F.Rows[i].Cells[0].Value.ToString());
                _x0[i] = double.Parse(x0.Rows[0].Cells[i].Value.ToString());
            }
        }
    }
}
