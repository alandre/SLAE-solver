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
using UI.MatrixFormats;

namespace UI
{
    public partial class MainForm : Form
    {
        private MatrixInitialazer Input = new MatrixInitialazer();
        public MainForm()
        {
            InitializeComponent();
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
                if (file.ShowDialog() == DialogResult.OK)
                {
                    StreamReader sr = new StreamReader(file.FileName);
                    string dataInput = sr.ReadToEnd();
                    sr.Close();

                    //Очевидно напрашивается какой-то паттерн, фабрика, может быть
                    switch (formatBox.SelectedIndex)
                    {
                        case 0:
                            {
                                var matrix = new DenseMatrix { symmetry = sim.Checked };
                                Input.dense = MatrixInitialazer.Input(dataInput, matrix);
                            }
                            break;
                        case 1:
                            {
                                var matrix = new SparseMatrixWithOutDiag { symmetry = sim.Checked };
                                Input.sparseWithOutDiag = MatrixInitialazer.Input(dataInput, matrix);
                            }
                            break;
                        case 2:
                            {
                                var matrix = new SparseNatrixWithDiag { symmetry = sim.Checked };
                                Input.sparseWithDiag = MatrixInitialazer.Input(dataInput, matrix);
                            }
                            break;
                        case 3:
                            {
                                var matrix = new CoordinateMatrix() { symmetry = sim.Checked };
                                Input.coordinate = MatrixInitialazer.Input(dataInput, matrix);
                            }
                            break;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
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
            fileInput.Enabled = true;
            ManualEntry.Enabled = true;
        }

        private void Notsim_Click(object sender, EventArgs e)
        {
            fileInput.Enabled = true;
            ManualEntry.Enabled = true;
        }
    }
}
