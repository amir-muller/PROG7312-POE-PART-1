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

namespace SystemFrontEnd;

public partial class frmNetworkTopology : Form
{
    // graph object
    private GViewer _viewer;


    public frmNetworkTopology()
    {
        InitializeComponent();
        InitializeGraph();
    }

    // initialize graph
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


        // add nodes
        var connections = new (string Source, string Target)[]
        {
            ("Gateway Router", "Node A"),
            ("Gateway Router", "Node B"),
            ("Gateway Router", "Node C"),
            ("Node A", "Node B"),
            ("Node A", "Node D"),
            ("Node B", "Node C"),
            ("Node B", "Node D"),
            ("Node B", "Node E"),
            ("Node C", "Node E"),
            ("Node D", "Node E")
        };

        // remove arrows on bi connections 
        foreach (var (source, target) in connections)
        {
            Edge edge = graph.AddEdge(source, target);

            edge.Attr.ArrowheadAtTarget = ArrowStyle.None;
            edge.Attr.ArrowheadAtSource = ArrowStyle.None;
        }

        //style nodes
        foreach (Node node in graph.Nodes)
        {
            node.Attr.Shape = Shape.Circle;
            node.Attr.FillColor = Microsoft.Msagl.Drawing.Color.LightBlue;
            node.Attr.LineWidth = 2;
            node.Label.FontSize = 12;
        }

        // style gateway node
        Node gateway = graph.FindNode("Gateway router");

        if (gateway != null)
        {
            gateway.Attr.FillColor = Microsoft.Msagl.Drawing.Color.LightGreen;
        }


        // set to viewer
        _viewer.Graph = graph;
        panel1.Controls.Clear();
        panel1.Controls.Add(_viewer);
        _viewer.Dock = DockStyle.Fill;


    }

}
