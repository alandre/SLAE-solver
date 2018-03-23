using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using SolverCore;
using SolverCore.Loggers;
using SolverCore.Solvers;
using SolverCore.Methods;
using System.Collections.Immutable;
using System.ServiceModel.Channels;
using UI.Properties;
using System.Text;

namespace UI
{
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

    public partial class MainForm : Form
    {
        private MatrixInitialazer Input = new MatrixInitialazer();
        Timer timer = new Timer();
        bool inputChecked = false;
        bool methodChecked = false;
        bool manualInputNotNull = false;
        bool fileInputNotNull = false;
        SaveBufferLogger Logger;
        IVector x0_tmp;
        int maxIter;
        double eps;
        SLAE currentSLAE;
        SLAE manualInputedSLAE;
        SLAE fileInputedSLAE;

        static List<String> Types = null;

        private string path;
        ConstructorForm constructorForm;

        (string name, SolverCore.Loggers.SaveBufferLogger log, double time)[] _Methods;


        public MainForm()
        {
            InitializeComponent();
            var keyList = new List<string>(FormatFactory.FormatsDictionary.Keys);
            Types = new List<string>();
            foreach (var format in keyList)
            {
                formatBox.Items.Add(format);
            }
            formatBox.Text = formatBox.Items[0].ToString();

            methodListBox.DataSource = Enum.GetValues(typeof(MethodsEnum));

            var location = System.Reflection.Assembly.GetExecutingAssembly().Location;   //get path with .exe file
            path = Path.GetDirectoryName(location);
            outPathBox.Text = path;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void fileInput_Click(object sender, EventArgs e)
        {
            try
            {
                var file = new OpenFileDialog {Filter = "Text file|*.txt"};
                if (file.ShowDialog() == DialogResult.OK)
                {
                    StreamReader sr = new StreamReader(file.FileName);
                    string dataInput = sr.ReadToEnd();
                    sr.Close();

                    Input = MatrixInitialazer.Input(dataInput, sim.Checked);
                    epsBox.Enabled = true;
                    iterBox.Enabled = true;
                    fileInputedSLAE.matrix = FormatFactory.Init(FormatFactory.FormatsDictionary[formatBox.Text], Input, Input.symmetry);
                    fileInputedSLAE.b = new Vector(Input.b);
                    if (Input.x0 != null)
                        fileInputedSLAE.x0 = new Vector(Input.x0);
                    else
                    {
                        double[] tmpx0 = new double[fileInputedSLAE.matrix.Size];
                        for (int i = 0; i < tmpx0.Length; i++)
                            tmpx0[i] = 0;
                        fileInputedSLAE.x0 = new Vector(tmpx0);
                    }

                    var a = FormatFactory.PatternRequired(FormatFactory.FormatsDictionary[formatBox.SelectedItem.ToString()]);

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
            x0_tmp = _x0;
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
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = path;
            openFileDialog1.ShowDialog();
        }

        private void resultsFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //на вход нужен массив кортежей: (string, savebufferloger, double time)
            //строка - название, логер и понятия не имею какого фомата время, потому дабл
            //никто не может обещать, что функция работает, более вероятно что она не работает
            ResultsForm resultsForm = new ResultsForm(_Methods);
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
            string method = methodListBox.SelectedItem.ToString();
            if (Types.Contains(method)) Types.Remove(method);
            else Types.Add(method);
            if (methodListBox.CheckedItems.Count > 0)
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
            currentSLAE = manualInpitRadioBtn.Checked ? manualInputedSLAE : fileInputedSLAE;
            SolveAsync();
        }
        private async void SolveAsync()
        {
            var uniqueDirectoryName = string.Format(@"\{0}", DateTime.Now.ToString("hh-mm-ss dd.mm.yyyy"));
            //var uniqueDirectoryName = string.Format(@"\{0}", Guid.NewGuid());
            string fullDirectoryName = path + uniqueDirectoryName;
            
            _Methods = new(string name, SaveBufferLogger log, double time)[methodListBox.CheckedItems.Count];

            maxIter = Convert.ToInt16(iterBox.Value);
            eps = Convert.ToDouble(epsBox.Text);
            
            Directory.CreateDirectory(fullDirectoryName);

            MethodProgressBar.Value = 0;
            MethodProgressBar.Maximum = methodListBox.CheckedItems.Count;

            //временная мера 
            IterProgressBar.Maximum = maxIter;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 1; //выбрать наилучшую 
            timer.Enabled = true;
            int count = 0;
            done_label.Text = Convert.ToString(count);
            need_label.Text = Convert.ToString(methodListBox.CheckedItems.Count);
            done_label.Visible = true;
            need_label.Visible = true;
            label5.Visible = true;
            int i = 0;
            foreach (var methodName in methodListBox.CheckedItems)
            {
                currentSLAE.x0 = x0_tmp;
                _Methods[i].name = methodName.ToString();
                IterProgressBar.Value = 0;
                MethodsEnum method =(MethodsEnum)methodName;
                //IMethod method = new JacobiMethod();
                Logger = new SaveBufferLogger();
               
                var loggingSolver = LoggingSolversFabric.Spawn(method, Logger);

                timer.Start();
                IVector result = await RunAsync((LoggingSolver)loggingSolver, currentSLAE.matrix, currentSLAE.x0, currentSLAE.b);
                timer.Stop();
                _Methods[i].time = 0;

                var LogList = Logger.GetList();
                _Methods[i].log = (SaveBufferLogger)Logger;

                MethodProgressBar.Increment(1);
                var LogList = Logger.GetList();
                residual_label.Text = Convert.ToString(LogList[LogList.Count-1]);
                IterProgressBar.Value = maxIter;
                //var newEntry1 = new KeyValuePair<int, double>(Count, r);
                //TODO: замеры времени для Task
                count++;
                done_label.Text = Convert.ToString(count);
                WriteResultToFile(result, methodName.ToString(), LogList.Count, LogList[LogList.Count - 1], fullDirectoryName);

                i++;
            }

        }

        private void WriteResultToFile(
          IVector result,
          string method,
          int iterationCount,
          double residual,
          string pathToDirectory)
        {
            var directory = $"{pathToDirectory}\\{method}";
            Directory.CreateDirectory(directory);

            var pathToTotalFile = $"{path}\\Сводные данные.txt";
            var pathToSolveReportFile = $"{directory}\\Протокол решения.txt";
            var pathToVectorFile = $"{directory}\\Вектор решения.txt";

            var totalString = new StringBuilder();
            var resultReportString = new StringBuilder(); 

            var solve = string.Join(" ", result);

            totalString
                .AppendLine($"{method}")
                .AppendLine($"Вектор решения: {solve}")
                .AppendLine($"Число итераций: {iterationCount}")
                .AppendLine($"Невязка: {residual}\r\n");

            resultReportString
               .AppendLine($"Число итераций: {iterationCount}")
               .AppendLine($"Невязка: {residual}");


            File.WriteAllText(pathToSolveReportFile, resultReportString.ToString());
            File.WriteAllText(pathToVectorFile, solve.ToString());
            File.AppendAllText(pathToTotalFile, totalString.ToString());
        }


        void timer_Tick(object sender, EventArgs e)
        {
            residual_label.Visible = true;
            var (iter, residual) = Logger.GetCurrentState();
            residual_label.Text= Convert.ToString(residual);
            IterProgressBar.Value = iter;
        }

        private Task<IVector> RunAsync(LoggingSolver loggingSolver, IMatrix matrix, IVector x0, IVector b)
        {
            return Task.Run(() =>
            {
                return loggingSolver.Solve((ILinearOperator)matrix, x0, b,maxIter,eps);
           });
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            FBD.SelectedPath = path;
            if (FBD.ShowDialog() == DialogResult.OK)
            {
                path = FBD.SelectedPath;
                outPathBox.Text = path;
            }
        }

        private void fileInputBtn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}
