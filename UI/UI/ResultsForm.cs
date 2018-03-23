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

            ChartArea chartArea1 = new ChartArea();
            Legend legend1 = new Legend();
            chartArea1.Name = "Графики невязок";
            chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            chart1.Legends.Add(legend1);

            Series[] myGraphics = new Series[methods_number];

            int maxiter = 0;
            for (int i = 0; i < methods_number; i++)
                if (Methods[i].residual.Count > maxiter)
                    maxiter = Methods[i].residual.Count;

                for (int i = 0; i < methods_number; i++)
            {
                myGraphics[i] = new Series();
                int m = Methods[i].residual.Count;
                myGraphics[i].Name = myGraphics[i].LegendText = Methods[i].name;
                for (int j = 0; j < m; j++)
                    myGraphics[i].Points.AddXY(j, Methods[i].residual[j]);
                for (int j = m; j < maxiter; j++)
                    myGraphics[i].Points.AddXY(j, Methods[i].residual[m - 1]);
                myGraphics[i].ChartType = SeriesChartType.Line;

                chart1.Series.Add(myGraphics[i]);
            }
       }
    }
 
}