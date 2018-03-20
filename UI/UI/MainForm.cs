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
using SolverCore.Solvers;
using SolverCore.Methods;
using UI.Properties;

namespace UI
{
    public partial class MainForm : Form
    {
        private MatrixInitialazer Input = new MatrixInitialazer();

        bool inputChecked = false;
        bool methodChecked = false;
        bool manualInputNotNull = false;
        bool fileInputNotNull = false;

        struct SLAE
        {
            public IMatrix matrix;
            public IVector b;
            public IVector x0;

            public SLAE(IMatrix _matrix, IVector _b, IVector _x0)
            {
                matrix = _matrix;
                b = _b;
                x0 = _x0;
            }
        }

        SLAE currentSLAE;
        SLAE manualInputedSLAE;
        SLAE fileInputedSLAE;

        static List<String> Types = null;

        private string path;
        ConstructorForm constructorForm;

        public MainForm()
        {
            InitializeComponent();
            var tmp = new FormatFactory();
            var keyList = new List<string>(tmp.formats.Keys);
            Types = new List<string>();
            for (int i = 0; i < keyList.Count; i++)
            {
                formatBox.Items.Add(keyList[i]);
            }
            formatBox.Text = formatBox.Items[0].ToString();

            var location = System.Reflection.Assembly.GetExecutingAssembly().Location;   //get path with .exe file
            path = Path.GetDirectoryName(location);
            textBox1.Text = path;
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
                    fileInputedSLAE.matrix = FormatFactory.Init(value, Input, Input.symmetry);
                    var a = FormatFactory.PatternRequired(formatBox.SelectedItem.ToString());

                    fileInputBtn.Text = file.FileName;
                    fileInputNotNull = true;
                    CheckedChanged(inputCheckedImg, inputChecked = true);
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

        private void fileInputRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            fileInputPanel.Enabled = fileInputRadioBtn.Checked;
            manualInpitRadioBtn.Checked = !fileInputRadioBtn.Checked;
            inputChecked = fileInputRadioBtn.Checked && fileInputNotNull;

            CheckedChanged(inputCheckedImg, inputChecked);
        }

        private void manualInpitRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            manualInputBtn.Enabled = manualInpitRadioBtn.Checked;
            fileInputRadioBtn.Checked = !manualInpitRadioBtn.Checked;
            inputChecked = manualInpitRadioBtn.Checked && manualInputNotNull;

            CheckedChanged(inputCheckedImg, inputChecked);
        }

        public void SetSLAE(IMatrix _mat, IVector _b, IVector _x0)
        {
            manualInputedSLAE = new SLAE(_mat, _b, _x0);

            manualInputNotNull = true;
            CheckedChanged(inputCheckedImg, inputChecked = true);
        }

        private void CheckedChanged(PictureBox pictureBox, bool check)
        {
            pictureBox.Image = check ? Resources.CheckMark : Resources.UnabledCheckMark;
            startBtn.Enabled = inputChecked && methodChecked;
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

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //if (checkedListBox1.CheckedItems.Count > 0)
            //    methodCheckedImg.Image = Resources.CheckMark;
            //else
            //    methodCheckedImg.Image = Resources.UnabledCheckMark;
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //методы на форму должны добавляться из фабрики решателей
            string a = (string)checkedListBox1.SelectedItem;
            if (Types.Contains(a)) Types.Remove(a);
            else Types.Add(a);
            if (checkedListBox1.CheckedItems.Count > 0)
            {
                methodCheckedImg.Image = Resources.CheckMark;
                methodChecked = true;
            }
            else
            {
                methodCheckedImg.Image = Resources.UnabledCheckMark;
                methodChecked = false;
            }

            startBtn.Enabled = inputChecked && methodChecked;
        }

        private void Start_Click(object sender, EventArgs e)
        {
            SolveAsync();

            var uniqueDirectoryName = string.Format(@"\{0}", Guid.NewGuid());
            string full_directory_name = path + uniqueDirectoryName;
            Directory.CreateDirectory(@full_directory_name);

            //TODO: different file names (depending on the choosen methods)
            System.IO.File.Create(full_directory_name + "\\result.txt");
            MessageBox.Show("Результат был записан");

        }
        private async void SolveAsync()
        {
            /*
            foreach (string Method in Types)
            {
                ILogger Logger = new FakeLog();
                LoggingSolver loggingSolver = Spawn(Method, Logger);
                IVector result = await RunAsync(loggingSolver, matrix, x0, b);
            }
            */
            //Необходима фабрика решателей, жду слияния главной ветки и ветки решателей
            //временная мера для запуска программы
            for (int i = 0; i < checkedListBox1.CheckedItems.Count; i++)
            {
                LoggingSolver loggingSolver;
                IMethod Method = new JacobiMethod();
                ILogger Logger = new FakeLog();
                loggingSolver = new LoggingSolver(Method, Logger);
                IVector result = await RunAsync(loggingSolver, currentSLAE.matrix, currentSLAE.x0, currentSLAE.b);
            }

        }
        private Task<IVector> RunAsync(LoggingSolver loggingSolver, IMatrix matrix, IVector x0, IVector b)
        {
            return Task.Run(() =>
            {
                return loggingSolver.Solve((ILinearOperator)matrix, x0, b);
            });
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            if (FBD.ShowDialog() == DialogResult.OK)
            {
                path = FBD.SelectedPath;
                textBox1.Text = path;
            }
        }
    }

    //временная мера
    internal class FakeLog : ILogger
    {
        public void read()
        {
            return;
        }

        public void write()
        {
            return;
        }

        public void Write(int Iter, double Residual)
        {
            throw new NotImplementedException();
        }
    }
}
