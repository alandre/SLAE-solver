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
    public partial class Krylov : Form
    {
        MainForm mainForm;
        public Krylov()
        {
            InitializeComponent();
           
        }

        private void forwardToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            mainForm.SetKrylov((int)iterBox.Value);
            mainForm.Show();
            Hide();
        }

        private void Krylov_Load(object sender, EventArgs e)
        {
            mainForm = (MainForm)(Owner);
        }

        private void cancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Owner.Show();
            this.Hide();
        }

        private void Krylov_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
            Owner.Show();
        }
    }
}
