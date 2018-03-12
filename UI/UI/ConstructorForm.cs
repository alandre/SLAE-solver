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
        int n = 2; // размерность матрицы
        int width, height; // базовые ширина и высота формы
        int cellWidth = 35, cellHeight; // ширина и высота ячеек
        bool symmetrized = false; // является ли матрица симметричной

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

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {

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
                for (int j = n; j < n - diff; ++j)
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

        private void далееToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormatForm formatForm = new FormatForm(A, cellWidth * (n - 2), cellHeight * (n - 2));
            formatForm.Show();
            //PatternForm patternForm = new PatternForm(A, cellWidth * (n - 2), cellHeight * (n - 2));
            //patternForm.Show();
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
            // Если ячейка read-only, принудительный TAB 
            if (((DataGridView)sender).Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly)
            {
                SendKeys.Send("{TAB}");
            }
        }
    }
}
