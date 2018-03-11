using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using SolverCore;

namespace UI
{
    public partial class MainForm : Form
    {
        private MatrixInitialazer Input = new MatrixInitialazer();
        private IMatrix matrix;
        
        public MainForm()
        {
            InitializeComponent();
            formatBox.DataSource = Enum.GetValues(typeof(FormatsEnum));
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void fileInput_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog file = new OpenFileDialog();
                file.Filter = "Text file|*.txt";
                if (file.ShowDialog() == DialogResult.OK)
                {
                    StreamReader sr = new StreamReader(file.FileName);
                    string dataInput = sr.ReadToEnd();
                    sr.Close();

                    Input = MatrixInitialazer.Input(dataInput, Input, sim.Checked);
                    epsBox.Enabled = true;
                    timeBox.Enabled = true;
                    matrix = FormatFactory.Init(formatBox.SelectedIndex, Input, Input.symmetry);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Неправильный формат входного файла.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void ChoseOutput_Click(object sender, EventArgs e)
        {

        }

        private void ManualEntry_Click(object sender, EventArgs e)
        {
            ConstructorForm form = new ConstructorForm();
            form.Show();
        }

        private void formatBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void sim_Click(object sender, EventArgs e)
        {
        }

        private void Notsim_Click(object sender, EventArgs e)
        {
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void epsBox_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void epsBox_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
