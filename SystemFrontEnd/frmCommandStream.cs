using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SystemFrontEnd
{
    public partial class frmCommandStream : Form
    {
        //private LightningChartUltimate chart;
        private Chart lineChart;

        private Dictionary<string, DeviceModel> registeredDevices = new Dictionary<string, DeviceModel>();


        string[] axisX = { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };
        double[] axisY = { 15, 28, 22, 35, 42, 35, -4 };

        public frmCommandStream()
        {
            InitializeComponent();

            // ini the chart
            SetupChart();
            LoadChartData(axisX, axisY);
        }




        //ini chart
        private void SetupChart()
        {
            lineChart = new Chart();
            lineChart.Dock = DockStyle.Fill;

            ChartArea chartArea = new ChartArea("MainArea");
            chartArea.AxisX.Title = "Days";
            chartArea.AxisY.Title = "Temps";
            chartArea.AxisX.Interval = 1;

            lineChart.ChartAreas.Add(chartArea);

            //add to panel
            chartPanel.Controls.Add(lineChart);
        }



        private void LoadChartData(string[] axisX, double[] axisY)
        {
            lineChart.Series.Clear();

            Series series = new Series("Sensor Temps");

            //set line chart
            series.ChartType = SeriesChartType.Line;


            series.BorderWidth = 3;
            series.Color = System.Drawing.Color.Blue;
            series.MarkerStyle = MarkerStyle.Circle;
            series.MarkerSize = 8;

            //add data


            for (int i = 0; i < axisX.Length; i++)
            {
                int pointIndex = series.Points.AddXY(i, axisY[i]);
                series.Points[pointIndex].AxisLabel = axisX[i];
            }


            //add to chart
            lineChart.Series.Add(series);
        }


        private void btnGoHome_Click(object sender, EventArgs e)
        {
            // hide form
            this.Hide();

            // open menu form
            Form1 menuForm = new Form1();
            menuForm.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
