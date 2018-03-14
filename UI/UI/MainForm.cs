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
using UI.Properties;

namespace UI
{
    public partial class MainForm : Form
    {
        private MatrixInitialazer Input = new MatrixInitialazer();

        bool inputChecked = false;
        bool methodChecked = false;
        bool outputChecked = false;

        private IMatrix matrix;
        private IVector b;
        private IVector x0;
        
        ConstructorForm constructorForm;

        public MainForm()
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

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void fileInput_Click(object sender, EventArgs e)
        {
            try
            {
                var file = new OpenFileDialog();
                file.Filter = "Text file|*.txt";
                if (file.ShowDialog() == DialogResult.OK)
                {
                    StreamReader sr = new StreamReader(file.FileName);
                    string dataInput = sr.ReadToEnd();
                    sr.Close();

                    Input = MatrixInitialazer.Input(dataInput, Input, sim.Checked);
                    epsBox.Enabled = true;
                    iterBox.Enabled = true;
                    var tmp = new FormatFactory();
                    var value = tmp.formats[formatBox.SelectedItem.ToString()];
                    matrix = FormatFactory.Init(value, Input, Input.symmetry);
                    var a = FormatFactory.PatternRequired(formatBox.SelectedItem.ToString());
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Неправильный формат входного файла.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void ManualEntry_Click(object sender, EventArgs e)
        {
            if (constructorForm == null || constructorForm.IsDisposed)
                constructorForm = new ConstructorForm();

            constructorForm.Owner = this;
            constructorForm.Show();
            Hide();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            fileInputPanel.Enabled = fileInputRadioBtn.Checked;
            manualInpitRadioBtn.Checked = !fileInputRadioBtn.Checked;
        }

        private void manualInpitRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            manualInputBtn.Enabled = manualInpitRadioBtn.Checked;
            fileInputRadioBtn.Checked = !manualInpitRadioBtn.Checked;
        }

        public void SetSLAE(IMatrix _mat, IVector _b, IVector _x0)
        {
            matrix = _mat;
            b = _b;
            x0 = _x0;

            inputCheckedImg.Image = Resources.CheckMark;
        }

        private void epsBox_Validating(object sender, CancelEventArgs e)
        {
            if (!double.TryParse(epsBox.Text, out double res))
            {
                ((TextBox)sender).Undo();
                ((TextBox)sender).BackColor = Color.Red;
                timerHightlight.Start();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (epsBox.BackColor.G < 255)
                epsBox.BackColor = Color.FromArgb(255, (255 + epsBox.BackColor.G) / 2 + 1, (255 + epsBox.BackColor.B) / 2 + 1);
            else
                timerHightlight.Stop();
        }

        private void toolStripMenuOpenOutput_Click(object sender, EventArgs e)
        {

        }

        private void resultsFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResultsForm resultsForm = new ResultsForm();
            resultsForm.Show();
        }
    }
}
