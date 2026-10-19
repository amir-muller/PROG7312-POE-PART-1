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
        private List<SensorDataResponse> _allTelemetry = new();


        //private LightningChartUltimate chart;
        private Chart lineChart;      

        public frmCommandStream()
        {
            InitializeComponent();
            _apiClient = new ApiClient();
        }

        private async void frmCommandStream_Load(object sender, EventArgs e)
        {
            // ini the chart
            SetupChart();           

            SetupUI();
            await LoadDashboardDataAsync();

            RefreshTimer.Start();
            RefreshTimer.Interval = 2000;
            RefreshTimer.Tick += RefreshTimer_Tick;
        }


        private void SetupUI()
        {
            this.Text = "Real-Time Command Stream Dashboard";

            //pop filter cmb
            if (cmbFilterCategory != null)
            {
                cmbFilterCategory.Items.Clear();
                cmbFilterCategory.Items.Add("All Categories");
                cmbFilterCategory.Items.Add("Moisture");
                cmbFilterCategory.Items.Add("Temperature"); 
                cmbFilterCategory.SelectedIndex = 0;
            }
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
                }

                var telemetry = await _apiClient.GetAllDataAsync();
                if (telemetry != null)
                {
                    _allTelemetry = telemetry;
                }

                Search();
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
            if (cmbFilterCategory == null || txtSearch == null) return;

            var selectedCategory = cmbFilterCategory.SelectedItem?.ToString();
            var searchTerm = txtSearch.Text.Trim().ToLower();

            // filter based on devices
            var filteredDevices = _allDevices.AsEnumerable();

            if (!string.IsNullOrEmpty(selectedCategory) && selectedCategory != "All Categories")
            {
                filteredDevices = filteredDevices.Where(s =>
                    !string.IsNullOrEmpty(s.SensorCategory) &&
                    s.SensorCategory.Trim().Equals(selectedCategory.Trim(), StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                filteredDevices = filteredDevices.Where(s =>
                    s.SensorName.ToLower().Contains(searchTerm) ||
                    s.SensorLocation.ToLower().Contains(searchTerm) ||
                    s.SensorMAC.ToLower().Contains(searchTerm));
            }

            var matchingDevicesList = filteredDevices.ToList();
            if (dgvDevices != null)
            {
                dgvDevices.DataSource = matchingDevicesList;
            }

            //filter telemertry
            var matchingMacs = new HashSet<string>(matchingDevicesList.Select(d => d.SensorMAC), StringComparer.OrdinalIgnoreCase);

            var filteredTelemetry = _allTelemetry
                .Where(t => matchingMacs.Contains(t.SensorMAC))
                .ToList();

            if (dgvTelemetry != null)
            {
                dgvTelemetry.DataSource = filteredTelemetry;
            }

            
            UpdateMultiSensorChart(matchingDevicesList, filteredTelemetry);
        }

        private void UpdateMultiSensorChart(List<SensorDetailsResponse> devices, List<SensorDataResponse> telemetry)
        {
            if (lineChart == null) return;

            lineChart.Series.Clear();
   
            Color[] colors = { Color.Blue, Color.Red, Color.Green, Color.Orange, Color.Purple, Color.Cyan, Color.Magenta };
            int colorIndex = 0;

            foreach (var device in devices)
            {
                //get latest 15 points
                var sensorReadings = telemetry
                    .Where(t => t.SensorMAC.Equals(device.SensorMAC, StringComparison.OrdinalIgnoreCase))
                    .OrderBy(t => t.TimeStamp)
                    .TakeLast(15)
                    .ToList();

                if (!sensorReadings.Any()) continue;

                //seperate line for each sensor
                Series series = new Series(device.SensorName);
                series.ChartType = SeriesChartType.Line;
                series.BorderWidth = 3;
                series.Color = colors[colorIndex % colors.Length];
                series.MarkerStyle = MarkerStyle.Circle;
                series.MarkerSize = 7;
                series.Legend = "ChartLegend";

                for (int i = 0; i < sensorReadings.Count; i++)
                {
                    var reading = sensorReadings[i];
                    int pointIndex = series.Points.AddXY(i, reading.SensorValue);


                    //axis time stamp on x
                    series.Points[pointIndex].AxisLabel = reading.TimeStamp.ToString("HH:mm:ss");
                }

                lineChart.Series.Add(series);
                colorIndex++;
            }
        }




        //================================================================================
        //ini chart
        private void SetupChart()
        {
            lineChart = new Chart();
            lineChart.Dock = DockStyle.Fill;

            ChartArea chartArea = new ChartArea("MainArea");
            chartArea.AxisX.Title = "Time";
            chartArea.AxisY.Title = "Sensor Values";
            chartArea.AxisX.Interval = 1;

            lineChart.ChartAreas.Add(chartArea);

            Legend Legend = new Legend("ChartLegend");
            lineChart.Legends.Add(Legend);

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
            RefreshTimer.Stop();

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
        //    string command = "Manual Override: Trigger Water Pump";
        //    await _apiClient.PostManualOverrideAsync(command);
        //    if (lblStatus != null) lblStatus.Text = $"Command Sent: {command}";
        //    await LoadDashboardDataAsync();
        }

        private void frmCommandStream_FormClosing(object sender, FormClosingEventArgs e)
        {
            RefreshTimer.Stop();
        }

        private void frmCommandStream_FormClosed(object sender, FormClosedEventArgs e)
        {
            RefreshTimer.Stop();
        }
    }
}
