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
        Load += async (_, _) =>
        {
            try { 
                await InitializeTopologyDataAsync(); 
            }
            catch (Exception ex) 
            { 
                MessageBox.Show($"Topology failed: {ex.Message}"); 
            }
        };
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

        await InitializeGraphAsync();
    }



    // initialize graph method
    public async Task InitializeGraphAsync()
    {
        // create Gviewer
        _viewer = new GViewer
        {
            Dock = DockStyle.Fill,
            ToolBarIsVisible = false // hide toolbar
        };

        // create graph
        Graph graph = new Graph("Topology Graph");
  
        graph.LayoutAlgorithmSettings = new Microsoft.Msagl.Layout.Layered.SugiyamaLayoutSettings();

        Microsoft.Msagl.Drawing.Node gatewayNode = graph.AddNode("Gateway Router");
        gatewayNode.Attr.Shape = Shape.Circle;
        gatewayNode.Attr.FillColor = Microsoft.Msagl.Drawing.Color.LightGreen;
        gatewayNode.Attr.LineWidth = 2;
        gatewayNode.Label.FontSize = 8;

        //creating sensor nodes
        foreach (var sensor in _activeSensors)
        {
            string nodeName = sensor.SensorName;

            Microsoft.Msagl.Drawing.Node node = graph.AddNode(nodeName);
            node.Attr.Shape = Shape.Circle;
            node.Attr.FillColor = Microsoft.Msagl.Drawing.Color.LightCoral;
            node.Attr.LineWidth = 2;
            node.Label.FontSize = 8;
        }

        // fetch edges
        var edges = await _apiClient.GetAllEdgesAsync();

        if (edges != null)
        {
            foreach (var e in edges)
            {
                if (string.IsNullOrEmpty(e.Source) || string.IsNullOrEmpty(e.Target))
                    continue;

                if (graph.FindNode(e.Source) != null && graph.FindNode(e.Target) != null)
                {
                    var edge = graph.AddEdge(e.Source, e.Target);
                    edge.Attr.ArrowheadAtTarget = ArrowStyle.None;
                    edge.Attr.ArrowheadAtSource = ArrowStyle.None;
                }
            }
        }

        // fall back
        if (graph.EdgeCount == 0)
        {
            foreach (var sensor in _activeSensors)
            {
                var edge = graph.AddEdge("Gateway Router", sensor.SensorName);
                edge.Attr.ArrowheadAtTarget = ArrowStyle.None;
                edge.Attr.ArrowheadAtSource = ArrowStyle.None;
            }
        }


        if (this.InvokeRequired)
        {
            this.Invoke(new Action(() => RenderGraph(graph)));
        }
        else
        {
            RenderGraph(graph);
        }
    }

    private void RenderGraph(Graph graph)
    {
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

        var currentSensor = _activeSensors.FirstOrDefault(s => s.SensorMAC.Equals(_selectedSensorMac, StringComparison.OrdinalIgnoreCase));
        string nodeName = currentSensor?.SensorName ?? "Gateway Router";


        // integrate date time clicker
        DateTime startDate = dtpStart.Value.Date.ToUniversalTime();
        DateTime endDate = dtpEnd.Value.Date.AddDays(1).AddTicks(-1).ToUniversalTime();

        try
        {
            //calling the api endpoint
            var historicalData = await _apiClient.GetHistoricalRangeAsync(
                _selectedSensorMac,
                startDate,
                endDate
            );

            // show results on the datagrid
            dataGridView1.DataSource = historicalData;

            // get graph transversel for connected subnets
            var subnetDevices = await _apiClient.GetSubnetDevicesDfsAsync("Gateway Router");
            string subnetDevicesStr = subnetDevices != null ? string.Join(", ", subnetDevices) : "None";

            //pims algo mini spani treee
            var mstResult = await _apiClient.GetMstHopPathAsync("Gateway Router", nodeName);
            var hopPath = mstResult?.Path ?? new List<string> { nodeName };
            string pathStr = string.Join(" => ", Enumerable.Reverse(hopPath)); // node => gateway
            int totalHops = mstResult?.TotalHops ?? 0;


            //display results summary
            redOutput.Clear();
            redOutput.Text = $"===Network Topology Analysis===\n" +
                 $"Selected Node: {nodeName} with MAC {_selectedSensorMac}\n\n" +
                 $"Graph Traversal (DFS from gateway)\n{subnetDevicesStr}\n\n" +
                 $"Minimum Spanning Tree (Prim's)\n" +
                 $"Hop path: {pathStr}\n" +
                 $"Total Hops: {totalHops}\n";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Failed to fetch network analysis: {ex.Message}", "API Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
        
                   
}

