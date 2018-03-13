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
            forwardItem.Enabled = formatBox.SelectedIndex > -1;
        }

        private void forwardItem_Click(object sender, EventArgs e)
        {
            PatternForm patternForm = new PatternForm(A, width, height);
            patternForm.Show();
        }

        private void FormatForm_Load(object sender, EventArgs e)
        {

        }
    }
}
