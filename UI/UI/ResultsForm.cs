using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Windows.Forms;
using System.Web;
using System.Windows.Forms.DataVisualization.Charting;

namespace UI
{
    public partial class ResultsForm : Form
    {
        (string name, List<double> residual)[] Methods;
        int methods_number;
        public ResultsForm((string name, SolverCore.Loggers.SaveBufferLogger log)[] _Methods)
        {
            methods_number = _Methods.Length;
            Methods = new(string name, List<double> residual)[methods_number];

            //Methods = _Methods;//Опасность!!! передача по сслыке!
            for (int i = 0; i < methods_number; i++)
            {
                Methods[i].name = _Methods[i].name;
                var _residual = _Methods[i].log.GetList();
                int item_num = _residual.Count;
                Methods[i].residual = new List<double>(item_num);
                for (int j = 0; j < item_num; j++)
                    Methods[i].residual[j] = _residual[j];
            }
            InitializeComponent();
        }

        private void MakeGrapics()
        {


        }
        private Chart chart1;

        private void ResultsForm_Load(object sender, EventArgs e)
        {
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
        }
    }
}