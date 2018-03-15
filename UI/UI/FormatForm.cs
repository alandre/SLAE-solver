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
    public partial class FormatForm : Form
    {
        DataGridView A;
        int width, height;

        ConstructorForm constructorForm;
        PatternForm patternForm;
        MainForm mainForm;

        public FormatForm()
        {
            InitializeComponent();
            var tmp = new FormatFactory();
            var keyList = new List<string>(tmp.formats.Keys);
            for (int i = 0; i < keyList.Count; i++)
            {
                formatBox.Items.Add(keyList[i]);
            }
            formatBox.Text = formatBox.Items[0].ToString();
        }

        public FormatForm(DataGridView mat, int w, int h)
        {
            InitializeComponent();
            
            A = mat;
            width = w;
            height = h;
        }

        private void formatBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            forwardToolStripMenuItem.Enabled = formatBox.SelectedIndex > -1;
        }

        private void forwardItem_Click(object sender, EventArgs e)
        {
            if (FormatFactory.PatternRequired(formatBox.Text))
            {
                if (patternForm == null || patternForm.IsDisposed)
                    patternForm = new PatternForm();

                patternForm.Owner = this;
                patternForm.Show();
                patternForm.Update(formatBox.Text);
                Hide();
            }
            else
            {
                IMatrix A;
                IVector x0, b;
                constructorForm = (ConstructorForm)Owner;
                constructorForm.GetSLAE(out A, formatBox.Text, out b, out x0);

                mainForm.SetSLAE(A, b, x0);
                mainForm.Show();
                Hide();
            }
        }

        private void backwardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Owner.Show();
            Hide();
        }

        private void FormatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
            mainForm.Show();
        }

        private void FormatForm_Shown(object sender, EventArgs e)
        {
            Location = Owner.Location;
        }

        private void FormatForm_Load(object sender, EventArgs e)
        {
            mainForm = (MainForm)Owner.Owner;
        }
    }
}
