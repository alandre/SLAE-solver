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
using System.Collections.Immutable;
using System.Diagnostics;
using UI.Properties;
using System.Text;

namespace UI
{
    struct SLAE
    {
        public IMatrix matrix;
        public IVector b;
        public IVector x0;


        public SLAE(IMatrix matrix, IVector b, IVector x0)
        {
            this.matrix = matrix;
            this.b = b;
            this.x0 = x0;
        }
    }

    public partial class MainForm : Form
    {
        bool inputChecked = false;
        bool methodChecked = false;
        bool manualInputNotNull = false;
        bool fileInputNotNull = false;
        SaveBufferLogger Logger;
        IVector x0_tmp;
        SLAE currentSLAE;
        SLAE manualInputedSLAE;
        SLAE fileInputedSLAE;

        static List<String> Types = null;

        private string path;
        ConstructorForm constructorForm;

        public string FullDirectoryName = "";

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
            var FactList = new List<string>(FactorizerFactory.FactorizersDictionary.Keys);
            Types = new List<string>();
            foreach (var factorizer in FactList)
            {
                factorizerBox.Items.Add(factorizer);
            }
            factorizerBox.Text = factorizerBox.Items[0].ToString();
            methodListBox.DataSource = Enum.GetValues(typeof(MethodsEnum));
            var location = System.Reflection.Assembly.GetExecutingAssembly().Location;   
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
                var file = new OpenFileDialog {Filter = "Text file |*.txt|JSON file |*.json"};
                if (file.ShowDialog() == DialogResult.OK)
                {
                    StreamReader sr = new StreamReader(file.FileName);
                    string dataInput = sr.ReadToEnd();
                    sr.Close();

                    var input = MatrixInitialazer.Input(dataInput, sim.Checked);
                    epsBox.Enabled = true;
                    iterBox.Enabled = true;
                    fileInputedSLAE.matrix = FormatFactory.Init(FormatFactory.FormatsDictionary[formatBox.Text], input, input.symmetry);
                    if (fileInputedSLAE.matrix != null)
                    {
                        fileInputedSLAE.b = new Vector(input.b);
                        if (input.x0 != null)
                            fileInputedSLAE.x0 = new Vector(input.x0);
                        else
                        {
                            var tmpx0 = new double[fileInputedSLAE.matrix.Size];
                            for (int i = 0; i < tmpx0.Length; i++)
                                tmpx0[i] = 0;
                            fileInputedSLAE.x0 = new Vector(tmpx0);
                        }

                        fileInputBtn.Text = file.FileName;
                        fileInputNotNull = true;
                        CheckedChanged(inputCheckedImg, inputChecked = true);
                    }
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
            Process.Start("explorer.exe", FullDirectoryName);
        }

        private void resultsFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // на вход нужен массив кортежей: (string, savebufferloger, double time)
            // строка - название, логер и понятия не имею какого фомата время, потому дабл
            // никто не может обещать, что функция работает, более вероятно что она не работает
            try
            {
                ResultsForm resultsForm = new ResultsForm(_Methods);
                resultsForm.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("Нет данных для решений");
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string method = methodListBox.SelectedItem.ToString();
            if (Types.Contains(method)) Types.Remove(method);
            else
            {
                Types.Add(method);
            }
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
            x0_tmp = currentSLAE.x0.Clone();
            FactorizerFactory.FactorizersEnum factorizerName = FactorizerFactory.FactorizersDictionary[factorizerBox.Text];
            IMatrix factorizedMatrix = FactorizerFactory.Factorize_it(factorizerName,currentSLAE.matrix);
            var uniqueDirectoryName = "\\Solution " + DateTime.Now.ToString("hh-mm-ss dd.mm.yyyy");
            FullDirectoryName = path + uniqueDirectoryName;
            
            _Methods = new(string name, SaveBufferLogger log, double time)[methodListBox.CheckedItems.Count];

            
            Directory.CreateDirectory(FullDirectoryName);

            MethodProgressBar.Value = 0;
            MethodProgressBar.Maximum = methodListBox.CheckedItems.Count;

            IterProgressBar.Maximum = (int)iterBox.Value;
            int count = 0;
            done_label.Text = Convert.ToString(count);
            need_label.Text = Convert.ToString(methodListBox.CheckedItems.Count);
            done_label.Visible = true;
            need_label.Visible = true;
            label5.Visible = true;
            int i = 0;
            foreach (var methodName in methodListBox.CheckedItems)
            {
                currentSLAE.x0 = x0_tmp.Clone();
                _Methods[i].name = methodName.ToString();
                IterProgressBar.Value = 0;
                Logger = new SaveBufferLogger();
                var loggingSolver = LoggingSolversFabric.Spawn((MethodsEnum)methodName, Logger);
                timer1.Enabled = true;
                timer1.Start();
                Stopwatch sw = new Stopwatch();
                sw.Start();
                IVector result = await RunAsync((LoggingSolver)loggingSolver, factorizedMatrix, currentSLAE.x0, currentSLAE.b);
                sw.Stop();
               
                timer1.Stop();
                timer1.Enabled = false;

                _Methods[i].time = 0;
                
                _Methods[i].log = Logger;

                MethodProgressBar.Increment(1);
                var LogList = Logger.GetList();
                if (!LogList.IsEmpty) residual_label.Text = Convert.ToString(LogList[LogList.Count - 1]);
                
                IterProgressBar.Value = (int)iterBox.Value;
                
                count++;
                done_label.Text = Convert.ToString(count);
                WriteResultToFile(result, methodName.ToString(),sw.ElapsedMilliseconds, FullDirectoryName, LogList);
                i++;
            }

        }

        private void WriteResultToFile(
          IVector result,
          string method,
          long time,
          string pathToDirectory,
          IImmutableList<double> logList)
        {
            int iterationCount = logList.Count;
            double resultResidual;
            if (logList.Count>0)
                resultResidual = logList[logList.Count - 1];
            else resultResidual = -1;

            var directory = $"{pathToDirectory}\\{method}";
            Directory.CreateDirectory(directory);

            var pathToTotalFile = $"{pathToDirectory}\\Сводные данные.txt";
            var pathToResultFile = $"{pathToDirectory}\\Решение.txt";
            var pathToSolveReportFile = $"{directory}\\Информация о решении.txt";
            var pathToVectorFile = $"{directory}\\Вектор решения.txt";

            var totalString = new StringBuilder();
            var resultReportString = new StringBuilder();
            var resultTotalString = new StringBuilder();

            var solve = string.Join(" ", result);

            totalString
                .AppendLine($"{method}")
                .AppendLine($"Время решения в миллисекундах: {time}")
                .AppendLine($"Вектор решения: {solve}")
                .AppendLine($"Число итераций: {iterationCount}")
                .AppendLine($"Невязка: {resultResidual}\r\n");

            resultReportString
               .AppendLine($"Число итераций: {iterationCount}")
               .AppendLine($"Невязка: {resultResidual}");

            resultTotalString
              .AppendLine($"{method}\r")
              .AppendLine($"Итерация\tНевязка");

           
            File.AppendAllText(pathToTotalFile, resultTotalString.ToString());
            int i = 1;
            foreach (double element in logList)
            {
                File.AppendAllText(pathToTotalFile, $"{i}\t\t");
                File.AppendAllText(pathToTotalFile, $"{element}\r\n");
                i++;
            }
            File.AppendAllText(pathToTotalFile, "\r\n");

            File.WriteAllText(pathToSolveReportFile, resultReportString.ToString());
            File.WriteAllText(pathToVectorFile, solve.ToString());
            File.AppendAllText(pathToResultFile, totalString.ToString());
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
            return Task.Run(() => loggingSolver.Solve((ILinearOperator)matrix, x0, b,(int)iterBox.Value, double.Parse(epsBox.Text)));
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog {SelectedPath = path};
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
