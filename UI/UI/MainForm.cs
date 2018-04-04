using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
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
        CancellationTokenSource cts;
        bool needFactorization = false;
        bool inputChecked = false;
        bool methodChecked = false;
        bool manualInputNotNull = false;
        bool fileInputNotNull = false;
        SaveBufferLogger Logger;
        IVector x0_tmp;
        SLAE currentSLAE;
        SLAE manualInputedSLAE;
        SLAE fileInputedSLAE;
        int Krylov;
        static List<String> Types = null;
        private string path;
        ConstructorForm constructorForm;
        Krylov KrylovForm;

        public string FullDirectoryName = "";

        List<(string name, SaveBufferLogger log, double time)> _Methods;


        public MainForm()
        {
            cts = new CancellationTokenSource();
            InitializeComponent();
            var keyList = new List<string>(FormatFactory.FormatsDictionary.Keys);
            foreach (var format in keyList)
            {
                formatBox.Items.Add(format);
            }
            formatBox.Text = formatBox.Items[0].ToString();
           
            var Type = new List<MethodsEnum>(LoggingSolversFabric.MethodsDictionary.Values);
            foreach (var Method in Type)
            {
                methodListBox.Items.Add(Method);
            }
            Types = new List<string>();

            var FactList = new List<string>(FactorizersFactory.FactorizersDictionary.Keys);
            foreach (var factorizer in FactList)
            {
                factorizerBox.Items.Add(factorizer);
            }
            factorizerBox.Text = factorizerBox.Items[0].ToString();
            //methodListBox.DataSource = Enum.GetValues(typeof(MethodsEnum));
            var location = System.Reflection.Assembly.GetExecutingAssembly().Location;   
            path = Path.GetDirectoryName(location);
            outPathBox.Text = path;
        }

        private void fileInput_Click(object sender, EventArgs e)
        {
            try
            {
                var file = new OpenFileDialog {Filter = "Text file |*.txt|JSON file |*.json|Binary file | *.dat"};
                if (file.ShowDialog() == DialogResult.OK)
                {
                    StreamReader sr = new StreamReader(file.FileName);
                    string dataInput = sr.ReadToEnd();

                    sr.Close();
                    MatrixInitialazer input;
                    if (Path.GetExtension(file.FileName) != ".dat")
                        input = MatrixInitialazer.Input(dataInput, sim.Checked);
                    else
                        input = MatrixInitialazer.InputB(dataInput, sim.Checked);
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
            sim_CheckedChanged(sender, e);
            fileInputPanel.Enabled = fileInputRadioBtn.Checked;
            manualInpitRadioBtn.Checked = !fileInputRadioBtn.Checked;
            inputChecked = fileInputRadioBtn.Checked && fileInputNotNull;

            CheckedChanged(inputCheckedImg, inputChecked);
        }

        private void manualInpitRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (!(constructorForm == null || constructorForm.IsDisposed)) Sym(constructorForm.IsSymmetric);

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
        public void Sym(bool symmetry)
        {
            if (symmetry)
            {
                factorizerBox.Items.Clear();
                var FactList = new List<string>(FactorizersFactory.FactorizersSimDictionary.Keys);
                foreach (var factorizer in FactList)
                {
                    factorizerBox.Items.Add(factorizer);
                }
                factorizerBox.Text = factorizerBox.Items[0].ToString();
            }
            else
            {
                factorizerBox.Items.Clear();
                var FactList = new List<string>(FactorizersFactory.FactorizersDictionary.Keys);
                foreach (var factorizer in FactList)
                {
                    factorizerBox.Items.Add(factorizer);
                }
                factorizerBox.Text = factorizerBox.Items[0].ToString();
            }
          
        }
        public void SetKrylov(int _Krylov)
        {
            Krylov = _Krylov;
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

            ResultsForm resultsForm = new ResultsForm(_Methods);
            resultsForm.Show();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string method = methodListBox.SelectedItem.ToString();
          
            if (Types.Contains(method)) Types.Remove(method);
            else
            {
                Types.Add(method);
                switch (method)

                {
                    case "GMRes":
                        {
                            if (KrylovForm == null || KrylovForm.IsDisposed)
                                KrylovForm = new Krylov();

                            KrylovForm.Owner = this;
                            KrylovForm.Show();
                            Hide();
                            break;
                        }
                    default: break;
                }
                

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
            inputData.Enabled = outputData.Enabled = methodsData.Enabled =  startBtn.Enabled = false;
            needFactorization = false;
            resultsFormToolStripMenuItem.Enabled = toolStripMenuOpenOutput.Enabled = false;
            menuStrip2.Enabled = false;
            currentSLAE = manualInpitRadioBtn.Checked ? manualInputedSLAE : fileInputedSLAE;
            foreach (MethodsEnum methodName in methodListBox.CheckedItems)
            {
                switch (methodName)
                {
                    case MethodsEnum.BCGStab:
                        needFactorization = true;
                        break;
                    case MethodsEnum.CGM:
                        needFactorization = true;
                        break;
                    case MethodsEnum.LOS:
                        needFactorization = true;
                        break;
                    default: break;
                }
               
            }
          
            if (!needFactorization && (FactorizersFactory.FactorizersSimDictionary[factorizerBox.Text]!=FactorizersEnum.WithoutFactorization))
                MessageBox.Show("Факторизация для выбранных методов не требуется");
            SolveAsync();
            menuStrip2.Enabled = true;
            
        }

        private async void SolveAsync()
        {
         
            x0_tmp = currentSLAE.x0.Clone();
            string factorizerRusName = factorizerBox.Text;
            FactorizersEnum factorizerName = FactorizersFactory.FactorizersSimDictionary[factorizerBox.Text];
            var uniqueDirectoryName = "\\Solution " + DateTime.Now.ToString("hh-mm-ss dd.MM.yyyy");
            FullDirectoryName = path + uniqueDirectoryName;
            
            _Methods = new List<(string name, SaveBufferLogger log, double time)>();

            
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
            foreach (MethodsEnum methodName in methodListBox.CheckedItems)
            {
                bool needFactorization_method = false;
                switch (methodName)
                {
                    case MethodsEnum.BCGStab:
                        needFactorization_method = true;
                        break;
                    case MethodsEnum.CGM:
                        needFactorization_method = true;
                        break;
                    case MethodsEnum.LOS:
                        needFactorization_method = true;
                        break;
                }
                IFactorization factorizer = FactorizersFactory.SpawnFactorization(factorizerName, currentSLAE.matrix.ConvertToCoordinationalMatrix());
                currentSLAE.x0 = x0_tmp.Clone();
                IterProgressBar.Value = 0;
                Logger = new SaveBufferLogger();
                IVector result;
                ISolver loggingSolver;
                if (needFactorization_method)
                {
                     loggingSolver = LoggingSolversFabric.Spawn(methodName, Logger);
                }
                else loggingSolver = LoggingSolversFabric.Spawn(methodName, Logger);

                timer1.Enabled = true;
                
                Stopwatch sw = new Stopwatch();
                sw.Start();
                timer1.Start();
                result = await RunAsync((LoggingSolver)loggingSolver, currentSLAE.matrix, currentSLAE.x0, currentSLAE.b, factorizer);
                sw.Stop();
                timer1.Stop();
                timer1.Enabled = false;

                var LogList = Logger.GetList();

                var lastLog = Logger.GetCurrentState();

                if (LogList.IsEmpty || lastLog.residual == -1 && LogList.Count <= 1)
                {
                    MessageBox.Show("Метод " + methodName.ToString() + " разошелся на 1 итерации.", "Внимание!", MessageBoxButtons.OK);
                    MethodProgressBar.Maximum--;
                }
                else
                {
                    if (lastLog.residual == -1)
                    {
                        MessageBox.Show("Метод " + methodName.ToString() + " разошелся на " + LogList.Count.ToString() + " итерации. См. протокол решения в выходных данных.", "Внимание!", MessageBoxButtons.OK);
                        Logger.RemoveLast();
                    }
                    if (!LogList.IsEmpty) residual_label.Text = Convert.ToString(LogList[LogList.Count - 1]);

                    _Methods.Add((methodName.ToString(), Logger, sw.ElapsedMilliseconds));

                    IterProgressBar.Value = (int)iterBox.Value;

                    count++;
                    done_label.Text = Convert.ToString(count);
                    WriteResultToFile(result, methodName.ToString(), sw.ElapsedMilliseconds, FullDirectoryName, LogList, factorizerRusName);
                    i++;
                    
                    MethodProgressBar.Increment(1);
                }
            }
            startBtn.Enabled = true;
            methodsData.Enabled = true;
            outputData.Enabled = true;
            inputData.Enabled = true;
            resultsFormToolStripMenuItem.Enabled = toolStripMenuOpenOutput.Enabled = count > 0;

            if (count == 0)
                Directory.Delete(FullDirectoryName);
        }

        private void WriteResultToFile(
          IVector result,
          string method,
          long time,
          string pathToDirectory,
          IImmutableList<double> logList,
          string factorization)
        {
            if (method == "Jacobi" || method == "GaussianSeidel")
                factorization = "Без факторизации";
            int iterationCount = logList.Count;
            double resultResidual;
            if (logList.Count>0)
                resultResidual = logList[logList.Count - 1];
            else resultResidual = -1;

            var directory = $"{pathToDirectory}\\{method}";
            Directory.CreateDirectory(directory);

            var pathToTotalFile = $"{pathToDirectory}\\Сводные данные.txt";
            var pathToSolveReportFile = $"{directory}\\Протокол решения.txt";
            var pathToVectorFile = $"{directory}\\Вектор решения.txt";

            var totalString = new StringBuilder();
            var resultReportString = new StringBuilder();
            var resultTotalString = new StringBuilder();
            string solve;
            if (result != null) {  solve = string.Join(" ", result); }
            else {  solve = string.Join(" ","нет решения"); }

            totalString
                .AppendLine($"{method}")
                .AppendLine($"{factorization}")
                .AppendLine($"Время решения в миллисекундах: {time}")
                .AppendLine($"Вектор решения: {solve}")
                .AppendLine($"Число итераций: {iterationCount}")
                .AppendLine($"Невязка: {resultResidual}\r\n");

            resultReportString
               .AppendLine($"Число итераций: {iterationCount}")
               .AppendLine($"Невязка: {resultResidual}");

            resultTotalString
              .AppendLine($"Итерация\tНевязка");

           
            File.AppendAllText(pathToTotalFile, totalString.ToString());
            File.WriteAllText(pathToSolveReportFile, resultTotalString.ToString());
            File.WriteAllText(pathToVectorFile, solve.ToString());
            int i = 1;
            foreach (double element in logList)
            {
                File.AppendAllText(pathToSolveReportFile, $"{i}\t\t");
                File.AppendAllText(pathToSolveReportFile, $"{element}\r\n");
                i++;
            }
            File.AppendAllText(pathToSolveReportFile, "\r\n");
        }


        void timer_Tick(object sender, EventArgs e)
        {
            residual_label.Visible = true;
            var (iter, residual) = Logger.GetCurrentState();
            residual_label.Text= Convert.ToString(residual);
            IterProgressBar.Value = iter;
        }

        private Task<IVector> RunAsync(LoggingSolver loggingSolver, IMatrix matrix, IVector x0, IVector b, IFactorization factorizer)
        {
            return Task.Run(() => loggingSolver.Solve((ILinearOperator)matrix, x0, b, (int)iterBox.Value, double.Parse(epsBox.Text), factorizer));
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

        private void sim_CheckedChanged(object sender, EventArgs e)
        {
            if (sim.Checked)
            {
                factorizerBox.Items.Clear();
                var FactList = new List<string>(FactorizersFactory.FactorizersSimDictionary.Keys);
                foreach (var factorizer in FactList)
                {
                    factorizerBox.Items.Add(factorizer);
                }
                factorizerBox.Text = factorizerBox.Items[0].ToString();
            }
            else
            {
                factorizerBox.Items.Clear();
                var FactList = new List<string>(FactorizersFactory.FactorizersDictionary.Keys);
                foreach (var factorizer in FactList)
                {
                    factorizerBox.Items.Add(factorizer);
                }
                factorizerBox.Text = factorizerBox.Items[0].ToString();
            }
        }

        private void MainForm_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            string path_to_exe = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path_to_help = Path.GetDirectoryName(path_to_exe);

            string url = path_to_help + "\\Help.chm";
            var data = Resources.Help;
            if (!File.Exists(url))
            {
                using (var stream = new FileStream("Help.chm", FileMode.Create))
                {
                    stream.Write(data, 0, data.Length);
                    stream.Flush();
                }
            }
            Help.ShowHelp(this, url);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
 