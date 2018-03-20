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
        int width, heigth;
        string format;
        int cellWidth = MatrixVisualRepresentation.CellWidth;
        int cellHeight = MatrixVisualRepresentation.CellHeight;

        ConstructorForm SLAESource;
        MainForm mainForm;

        IMatrix matrix;
        IVector x0, b;

        bool patternChanged = false;

        public PatternForm()
        {
            InitializeComponent();
        }

        private void A_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            //
        }

        private void A_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            patternChanged = true;
            MatrixVisualRepresentation.InverseElementPatternStatus(ref A, e.RowIndex, e.ColumnIndex);
            MatrixVisualRepresentation.PaintPattern(ref A, Color.SteelBlue);
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
            bool symmetric = SLAESource.IsSymmetric;
            var tmp = new FormatFactory();
            if (symmetric)
                matrix = FormatFactory.Convert((SymmetricCoordinationalMatrix)MatrixVisualRepresentation.PatternedGridViewToCoordinational(A, symmetric), 
                    FormatFactory.FormatsDictionary[format]);
            else
                matrix = FormatFactory.Convert((CoordinationalMatrix)MatrixVisualRepresentation.PatternedGridViewToCoordinational(A, symmetric), 
                    FormatFactory.FormatsDictionary[format]);

            if (patternChanged)
            {
                CoordinationalMatrix user = (CoordinationalMatrix)MatrixVisualRepresentation.PatternedGridViewToCoordinational(A, symmetric);
                CoordinationalMatrix auto = matrix.ConvertToCoordinationalMatrix();

                foreach (var elem in auto)
                    if (!user.Contains((elem.value, elem.row, elem.col)))
                    {
                        var res = MessageBox.Show("Заданный портрет не соответствует выбранному формату хранения. Портрет будет автоматически преобразован к корректному виду.", "Уведомление", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        if (res == DialogResult.OK)
                        {
                            mainForm.SetSLAE(matrix, b, x0);
                            Close();
                        }
                        return;
                    }
            }
                       
            mainForm.SetSLAE(matrix, b, x0);
            Close();
        }

        private void PatternForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
            mainForm.Show();
        }

        private void PatternForm_Shown(object sender, EventArgs e)
        {
        }

        private void saveToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                IMatrix A;
                IVector x0, b;
                SLAESource.GetSLAE(out A, format, out b, out x0);

                IO.writeSLAE(new SLAE(A, b, x0), saveFileDialog.FileName);
            }
        }

        private void backwardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Owner.Show();
            Hide();
        }

        public void Update(string type)
        {
            SLAESource.GetSLAE(out this.matrix, type, out b, out x0);
            int n = this.matrix.Size;

            Location = Owner.Location;
            format = type;

            DataGridView matrix = new DataGridView();

            matrix = MatrixVisualRepresentation.CoordinationalToGridView(this.matrix.ConvertToCoordinationalMatrix(), SLAESource.IsSymmetric);
            MatrixVisualRepresentation.CopyDataGridView(matrix, ref A);

            width = cellWidth * (n - 2);
            heigth = cellHeight * (n - 2);

            Size size = new Size(Width > 115 + width ? Width : 115 + width, 190 + heigth);
            MaximumSize = size;
            MinimumSize = size;
            Size = size;
            A.ReadOnly = true;
            MatrixVisualRepresentation.PaintPattern(ref A, Color.SteelBlue);
        }
    }
}
