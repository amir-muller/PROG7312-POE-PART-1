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
using SystemFrontEnd.Services;

namespace SystemFrontEnd
{
    public partial class frmCommandStream : Form
    {

        private readonly ApiClient _apiClient;
        private List<SensorDetailsResponse> _allDevices = new();


        //private LightningChartUltimate chart;
        private Chart lineChart;

        string[] axisX = { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };
        double[] axisY = { 15, 28, 22, 35, 42, 35, -4 };

        public frmCommandStream()
        {
            InitializeComponent();
            _apiClient = new ApiClient();

            // ini the chart
            SetupChart();
            LoadChartData(axisX, axisY);

        }

        private async void frmCommandStream_Load(object sender, EventArgs e)
        {
            SetupUI();
            await LoadDashboardDataAsync();

            RefreshTimer.Start();
            RefreshTimer.Interval = 2000;
            RefreshTimer.Tick += RefreshTimer_Tick;
        }


        private void SetupUI()
        {
            this.Text = "Real-Time Command Stream Dashboard";

        }

        private async void RefreshTimer_Tick(object sender, EventArgs e)
        {
            await LoadDashboardDataAsync();
        }

        private async Task LoadDashboardDataAsync()
        {
            try
            {
                var sensors = await _apiClient.GetAllSensorDetailsAsync();
                if (sensors != null)
                {
                    _allDevices = sensors;
                    Search();
                }

                var telemetry = await _apiClient.GetAllDataAsync();
                if (telemetry != null && dgvTelemetry != null)
                {
                    dgvTelemetry.DataSource = telemetry;
                }
            }
            catch (Exception ex)
            {
                if (lblSearch != null)
                {
                    lblSearch.Text = $"Stream Connection Error: {ex.Message}";
                }
            }
        }

        private void Search()
        {
            if (cmbFilterCategory == null || txtSearch == null || dgvDevices == null) return;

            var selectedCategory = cmbFilterCategory.SelectedItem?.ToString();
            var searchTerm = txtSearch.Text.Trim().ToLower();

            var filtered = _allDevices.AsEnumerable();

            // Filter by category
            if (!string.IsNullOrEmpty(selectedCategory) && selectedCategory != "All Categories")
            {
                filtered = filtered.Where(s => s.SensorCategory.Equals(selectedCategory, StringComparison.OrdinalIgnoreCase));
            }

            // Search by Name, Location, or MAC
            if (!string.IsNullOrEmpty(searchTerm))
            {
                filtered = filtered.Where(s =>
                    s.SensorName.ToLower().Contains(searchTerm) ||
                    s.SensorLocation.ToLower().Contains(searchTerm) ||
                    s.SensorMAC.ToLower().Contains(searchTerm));
            }

            dgvDevices.DataSource = filtered.ToList();
        }





        //================================================================================
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

        private void lblSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            Search();
        }

        private void cmbFilterCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            Search();
        }

        private async void btnTriggerPump_Click(object sender, EventArgs e)
        {
            string command = "Manual Override: Trigger Water Pump";
            await _apiClient.PostManualOverrideAsync(command);
            if (lblStatus != null) lblStatus.Text = $"Command Sent: {command}";
            await LoadDashboardDataAsync();
        }

        private void frmCommandStream_FormClosing(object sender, FormClosingEventArgs e)
        {
            RefreshTimer.Stop();
        }
    }
}
