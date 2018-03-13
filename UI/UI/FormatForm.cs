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
    public partial class FormatForm : Form
    {
        DataGridView A;
        int width, height;

        PatternForm patternForm;
        MainForm mainForm;

        public FormatForm()
        {
            InitializeComponent();
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
            if (patternForm == null || patternForm.IsDisposed)
                patternForm = new PatternForm();
            
            patternForm.Owner = this;
            patternForm.Show();
            patternForm.Update();
            Hide();
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
