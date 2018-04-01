using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Windows.Forms;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;

namespace UI
{
    public partial class ResultsForm : Form
    {
        (string name, List<double> residual, double time)[] Methods;
        int methods_number;

        public ResultsForm()
        {

        }

        public ResultsForm((string name, SolverCore.Loggers.SaveBufferLogger log, double time)[] _Methods)
        {
            if (_Methods == null)
            {
                throw new ArgumentNullException();
            }

            InitializeComponent();

            methods_number = _Methods.Length;
            Methods = new(string name, List<double> residual, double time)[methods_number];
            dataGridView1.Rows.Add(methods_number);
            for (int i = 0; i < methods_number; i++)
            {
                Methods[i].name = _Methods[i].name;//название метода
                var _residual = _Methods[i].log.GetList();
                
                int item_num = _residual.Count;
                Methods[i].residual = new List<double>();

                for (int j = 0; j < item_num; j++)
                    Methods[i].residual.Add(_residual[j]);

                dataGridView1.Rows[i].Cells["method"].Value = _Methods[i].name;
                dataGridView1.Rows[i].Cells["res_res"].Value = _residual[item_num - 1];
                dataGridView1.Rows[i].Cells["itercount"].Value = item_num;
                dataGridView1.Rows[i].Cells["time"].Value = _Methods[i].time;
            }
            var addHeight = dataGridView1.RowTemplate.Height * dataGridView1.Rows.Count;
            dataGridView1.Height += addHeight;
            Height += addHeight;

            int maxiter = 0;
            for (int i = 0; i < methods_number; i++)
                if (Methods[i].residual.Count > maxiter)
                    maxiter = Methods[i].residual.Count;

            if (maxiter > 1)
            {

                ChartArea chartArea1 = new ChartArea();
                chartArea1.AxisX.Minimum = 1;
                chartArea1.AxisX.Maximum = maxiter;
                //chartArea1.AxisX.ScaleView.Zoomable = true;
                //chartArea1.AxisY.ScaleView.Zoomable = true;
                chart1.ChartAreas.Add(chartArea1);

                Legend legend1 = new Legend();
                chartArea1.Name = "Графики невязок";
                legend1.Docking = Docking.Bottom;
                chart1.Legends.Add(legend1);

                Series[] myGraphics = new Series[methods_number];

                for (int i = 0; i < methods_number; i++)
                {
                    myGraphics[i] = new Series();
                    int m = Methods[i].residual.Count;
                    myGraphics[i].Name = myGraphics[i].LegendText = Methods[i].name;
                    for (int j = 1; j <= m; j++)
                        myGraphics[i].Points.AddXY(j, Methods[i].residual[j - 1]);
                    for (int j = m + 1; j <= maxiter; j++)
                        myGraphics[i].Points.AddXY(j, Methods[i].residual[m - 1]);
                    myGraphics[i].ChartType = SeriesChartType.Line;

                    chart1.Series.Add(myGraphics[i]);
                }
            }
            else
            {
                chart1.Visible = false;
                dataGridView1.Location = chart1.Location;
                Height -= chart1.Height;
            }

       }

        private void ResultsForm_Load(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
 
}