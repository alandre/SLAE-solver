using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Windows.Forms;
using System.Web;
using System.Windows.Forms.DataVisualization.Charting;

namespace UI
{
    public partial class ResultsForm : Form
    {// Говорили что нужно выводить время, в логерах его не нашел но в возможность выводить его есть; если нет возможности его добавить, то надо как-то вручную нули забить
        //Если ничего не вышло разкоментить то что ниже
        //public ResultsForm()
        //{ }

        private Chart chart1;
        (string name, List<double> residual, double time)[] Methods;
        (string name, double residual, long maxiter, double time)[] dataSource;
        int methods_number;


        public ResultsForm((string name, SolverCore.Loggers.SaveBufferLogger log, double time )[] _Methods)
        {
            methods_number = _Methods.Length;
            Methods = new(string name, List<double> residual, double time)[methods_number];
            dataSource = new(string name, double residual, long maxiter, double time)[methods_number];
            for (int i = 0; i < methods_number; i++)
            {
                dataSource[i].name = Methods[i].name = _Methods[i].name;//название метода
                var _residual = _Methods[i].log.GetList();
                
                int item_num = _residual.Count;
                dataSource[i].residual = _residual[item_num - 1];//конечная невязка
                dataSource[i].maxiter = item_num;
                dataSource[i].time = _Methods[i].time;
                Methods[i].residual = new List<double>(item_num);

                for (int j = 0; j < item_num; j++)
                    Methods[i].residual[j] = _residual[j];
            }

            ChartArea chartArea1 = new ChartArea();
            Legend legend1 = new Legend();
            this.chart1 = new Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();

            // chart1
            // 
            chartArea1.Name = "Графики невязок";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(12, 12);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(499, 285);
            //
            ///
            ////
            Series[] myGraphics = new Series[methods_number];
            for (int i = 0; i < methods_number; i++)
            {
                int m = Methods[i].residual.Count;
                myGraphics[i].Name = myGraphics[i].Legend = Methods[i].name;
                for (int j = 0; j < m; j++)
                    myGraphics[i].Points.AddXY(j, Methods[i].residual[j]);
                myGraphics[i].ChartType = SeriesChartType.Line;

                chart1.Series.Add(myGraphics[i]);
            }
            ////
            ///
            //

            this.ClientSize = new System.Drawing.Size(933, 322);
            this.Controls.Add(this.chart1);
            this.Name = "ResultsForm";
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            InitializeComponent();
        }
    }
 
}