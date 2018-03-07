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
    public partial class ConstructorForm : Form
    {
        int n = 2;
        int width, height;
        int cellWidth = 35, cellHeight;
        bool symmetrized = false;

        public ConstructorForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void sizePanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            bool inc = (n < (int)numericUpDown1.Value);

            n = (int)numericUpDown1.Value;
            this.Width = width + cellWidth * (n - 2);
            this.Height = height + cellHeight * (n - 2);

            InitializeSLAE();

            if (inc && checkBox1.Checked)
                Symmetrize();
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ConstructorForm_Load(object sender, EventArgs e)
        {
            width = this.Width;
            height = this.Height;
            cellHeight = A.RowTemplate.Height;
            F.RowTemplate.Height = cellHeight;

            InitializeSLAE();
        }

        private void InitializeSLAE()
        {
            A.Height = F.Height = n * cellHeight + 2;
            A.Width = x0.Width = n * cellWidth + 1;
            F.Width = cellWidth + 1;
            x0.Height = cellHeight + 1;

            A.Columns.Clear();

            for (int j = 0; j < n; ++j)// наименование столбцов
            {
                A.Columns.Add("Column" + (j + 1).ToString(), "");
                A.Columns[j].Width = cellWidth;
            }

            for (int i = 0; i < n; i++)
            {
                A.Rows.Add();
                for (int j = 0; j < n; j++)
                    A.Rows[i].Cells[j].Value = (double)0;
            }

            F.Columns.Clear();
            F.Columns.Add("Column1", "");
            F.Columns[0].Width = cellWidth;

            for (int i = 0; i < n; i++)
            {
                F.Rows.Add();
                F.Rows[i].Cells[0].Value = (double)0;
            }

            x0.Columns.Clear();

            for (int j = 0; j < n; ++j)// наименование столбцов
            {
                x0.Columns.Add("Column" + (j + 1).ToString(), "");
                x0.Columns[j].Width = cellWidth;
            }

            x0.Rows.Add();
            for (int j = 0; j < n; j++)
                x0.Rows[0].Cells[j].Value = (double)0;
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

        private void A_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
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

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void A_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (symmetrized)
            {
                int j = e.ColumnIndex;
                int i = e.RowIndex;

                A.Rows[j].Cells[i].Value = A.Rows[i].Cells[j].Value;
            }
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

        private void A_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            // Check if cell is read-only. If so the force move to next control.

            if (((DataGridView)sender).Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly)
            {

                SendKeys.Send("{TAB}");

            }
        }
    }
}
