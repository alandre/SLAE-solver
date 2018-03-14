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

        ConstructorForm SLAESource;
        MainForm mainForm;

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
            IVector b, x0;
            CoordinationalMatrix tmp;
            SLAESource.GetSLAE(out tmp, out b, out x0);
            IMatrix mat = FormatFactory.Convert(MatrixVisualRepresentation.GridViewToCoordinational(A), format);
            mainForm.SetSLAE(mat, b, x0);
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

        private void backwardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Owner.Show();
            Hide();
        }

        public void Update(string type)
        {
            Location = Owner.Location;
            format = type;

            DataGridView mat = new DataGridView();

            SLAESource.GetGridParams(ref mat, ref width, ref heigth);
            MatrixVisualRepresentation.CopyDataGridView(mat, ref A);

            Size size = new Size(Width > 115 + width ? Width : 115 + width, 190 + heigth);
            MaximumSize = size;
            MinimumSize = size;
            Size = size;
            A.ReadOnly = true;
            MatrixVisualRepresentation.GenerateInitialPattern(ref A);
            MatrixVisualRepresentation.PaintPattern(ref A, Color.SteelBlue);
        }
    }
}
