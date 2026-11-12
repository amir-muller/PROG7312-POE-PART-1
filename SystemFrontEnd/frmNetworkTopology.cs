using Microsoft.Msagl.Core.Layout;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;
using Microsoft.Msagl.Layout.MDS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SystemFrontEnd.Services;


namespace SystemFrontEnd;

public partial class frmNetworkTopology : Form
{
    // Global variables
    private GViewer _viewer;
    private Dictionary<string, SensorDetailsResponse> _sensorDetailsMap = new(StringComparer.OrdinalIgnoreCase);
    private ApiClient _apiClient = new ApiClient();
    private List<SensorDetailsResponse> _activeSensors = new();

    // selected sensor
    private string _selectedSensorMac = string.Empty;


    public frmNetworkTopology()
    {
        InitializeComponent();
        _ = InitializeTopologyDataAsync();
    }

    public async Task InitializeTopologyDataAsync()
    {
        var sensors = await _apiClient.GetAllSensorDetailsAsync();

        if (sensors != null)
        {
            _activeSensors = sensors;
            foreach (var s in sensors)
            {                
                _sensorDetailsMap[s.SensorName] = s;
            }
        }

        InitializeGraph();
    }



    // initialize graph method
    public void InitializeGraph()
    {
        // create Gviewer
        _viewer = new GViewer
        {
            Dock = DockStyle.Fill,
            ToolBarIsVisible = false // hide toolbar
        };


        // create graph
        Graph graph = new Graph("Topology Graph");
        graph.LayoutAlgorithmSettings = new MdsLayoutSettings();


        //add gateway node
        Microsoft.Msagl.Drawing.Node gatewayNode = graph.AddNode("Gateway Router");
        gatewayNode.Attr.Shape = Shape.Circle;
        gatewayNode.Attr.FillColor = Microsoft.Msagl.Drawing.Color.LightGreen;
        gatewayNode.Attr.LineWidth = 2;
        gatewayNode.Label.FontSize = 8;

        //creating nodes and edges
        foreach (var sensor in _activeSensors)
        {
            string nodeName = sensor.SensorName;

            Microsoft.Msagl.Drawing.Node node = graph.AddNode(nodeName);
            node.Attr.Shape = Shape.Circle;
            node.Attr.FillColor = Microsoft.Msagl.Drawing.Color.LightCoral;
            node.Attr.LineWidth = 2;
            node.Label.FontSize = 8;

            //link senosr to gateway
            Microsoft.Msagl.Drawing.Edge edge = graph.AddEdge("Gateway Router", nodeName);
            edge.Attr.ArrowheadAtTarget = ArrowStyle.None;
            edge.Attr.ArrowheadAtSource = ArrowStyle.None;
        }


        _viewer.Graph = graph;
        panel1.Controls.Clear();
        panel1.Controls.Add(_viewer);
        _viewer.Dock = DockStyle.Fill;

        _viewer.Click -= Viewer_Click;
        _viewer.Click += Viewer_Click;

    }


    //view click event handeler
    private async void Viewer_Click(Object sender, EventArgs e)
    {
        if (_viewer.SelectedObject is Microsoft.Msagl.Drawing.Node selectedNode)
        {
            string nodeId = selectedNode.Id;

            lblNodeName.Text = $"Node Name: {nodeId}";

            if (_sensorDetailsMap.TryGetValue(nodeId, out var details))
            {
                _selectedSensorMac = details.SensorMAC;

                label2.Text = $"Node Mac: {details.SensorMAC}";
                label3.Text = $"Node Type: {details.SensorCategory} ({details.SensorLocation})";

            }
            else
            {
                _selectedSensorMac = string.Empty;
                label2.Text = "Node Mac: ";
                label3.Text = "Node Type: ";

            }
        }
    }








    private void label1_Click(object sender, EventArgs e) { }


    private void label3_Click(object sender, EventArgs e) { }

    private void btnHome_Click(object sender, EventArgs e)
    {
        //hide
        this.Hide();

        //open menu form
        Form1 menu = new Form1();
        menu.Show();
    }

    private async void btnSearchNodes_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(_selectedSensorMac))
        {
            MessageBox.Show("Please select a sensor node first before searching");
            return;
        }

        DateTime startDate = dtpStart.Value;
        DateTime endDate = dtpEnd.Value.Date.AddDays(1).AddSeconds(-1);

        //calling the api endpoint
        var historicalData = await _apiClient.GetHistoricalRangeAsync(
            _selectedSensorMac,
            startDate,
            endDate
        );

        int count = historicalData?.Count ?? 0;

        double averageValue = (historicalData != null && historicalData.Any())
        ? historicalData.Average(d => d.SensorValue)
        : 0;

        redOutput.Text = $"Date Range Query Results:\n\n" +
                    $"From: {startDate:yyyy-MM-dd} To: {endDate:yyyy-MM-dd}\n" +
                    $"Total Records Found: {count}\n" +
                    $"Average Sensor Value: {averageValue:F2}";
                    



        //    //Query Historical Data
        //    var historicalData = await _apiClient.GetHistoricalRangeAsync(
        //        details.SensorMAC,
        //        DateTime.UtcNow.AddDays(-7),
        //        DateTime.UtcNow
        //    );

        //    int recordCount = historicalData?.Count ?? 0;

        //    //Route Graph Traversal
        //    var subnetDevices = await _apiClient.GetSubnetDevicesDfsAsync("Gateway Router");
        //    int subnetCount = subnetDevices?.Count ?? 0;

        //    //Route Minimum Spanning Tree
        //    var mstResult = await _apiClient.GetMstHopPathAsync("Gateway Router", nodeId);
        //    string hopPathString = mstResult != null && mstResult.Path != null
        //        ? string.Join(" -> ", mstResult.Path)
        //        : nodeId;


        //    redOutput.Text = $"Selected Sensor: {nodeId}\n" +
        //                    $"MAC: {details.SensorMAC}\n\n" +
        //                    $"Historical Records (BST Range Query): {recordCount}\n" +
        //                    $"Subnet Connected Count (DFS): {subnetCount}\n" +
        //                    $"Power-Efficient MST Hop Path: {hopPathString}" +
        //                    "Dynamic Network Topology Analysis";

        //}
        //else
        //{
        //    label2.Text = "Node Mac: N/A (Gateway)";
        //    label3.Text = "Node Type: Network Core Router";
        //}
    }
}
