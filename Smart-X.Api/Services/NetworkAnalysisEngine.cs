using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smart_X.Api.DataModels;

namespace SystemFrontEnd.Services;

public class SensorBstNode
{
    public SensorDataRecord Data { get; set; }
    public SensorBstNode Left { get; set; }
    public SensorBstNode Right { get; set; }

    public SensorBstNode(SensorDataRecord data) => Data = data;
}

public class SensorHistoryBst
{
    private SensorBstNode _root;

    public void Insert(SensorDataRecord record)
    {
        _root = InsertRec(_root, record);
    }

    private SensorBstNode InsertRec(SensorBstNode root, SensorDataRecord record)
    {
        if (root == null) return new SensorBstNode(record);

        if (record.TimeStamp < root.Data.TimeStamp)
            root.Left = InsertRec(root.Left, record);
        else
            root.Right = InsertRec(root.Right, record);

        return root;
    }

    public List<SensorDataRecord> RangeQuery(DateTime start, DateTime end)
    {
        var result = new List<SensorDataRecord>();
        RangeQueryRec(_root, start, end, result);
        return result;
    }

    private void RangeQueryRec(SensorBstNode root, DateTime start, DateTime end, List<SensorDataRecord> result)
    {
        if (root == null) return;

        if (root.Data.TimeStamp >= start)
            RangeQueryRec(root.Left, start, end, result);

        if (root.Data.TimeStamp >= start && root.Data.TimeStamp <= end)
            result.Add(root.Data);

        if (root.Data.TimeStamp <= end)
            RangeQueryRec(root.Right, start, end, result);
    }
}


public class NetworkAnalysisEngine
{
    private readonly Dictionary<string, Dictionary<string, double>> _adjacencyList = new(StringComparer.OrdinalIgnoreCase);
    private List<(string Source, string Target, double Weight)> _cachedEdges = new();
    private bool _isDirty = true; //flag to rebuild

    public NetworkAnalysisEngine()
    {
        EnsureGateWayExists();
    }

    private void EnsureGateWayExists()
    {
        if (!_adjacencyList.ContainsKey("Gateway Router"))
        {
            _adjacencyList["Gateway Router"] = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase);
        }
    }

    public void BuildDynamicTopology(IEnumerable<SensorDetailsResponse> sensors)
    {
        _adjacencyList.Clear();
        EnsureGateWayExists();

        var sensorList = sensors.ToList();

        foreach (var sensor in sensorList)
        {
            string nodeName = sensor.SensorName; // Matches graph node name

            // Assign a simulated weight/distance
            double weightToGateway = 1.5 + (Math.Abs(sensor.SensorMAC.GetHashCode()) % 20) / 10.0;
            AddEdge("Gateway Router", nodeName, weightToGateway);
        }

        //creates the mesh
        for (int i = 0; i < sensorList.Count; i++)
        {
            for (int j = i + 1; j < sensorList.Count; j++)
            {
                var nodeA = sensorList[i].SensorName;
                var nodeB = sensorList[j].SensorName;

                // if same location == strong connection / short distance
                if (sensorList[i].SensorLocation.Equals(sensorList[j].SensorLocation, StringComparison.OrdinalIgnoreCase))
                {
                    AddEdge(nodeA, nodeB, 1.2);
                }
            }
        }
    }


    public void AddEdge(string u, string v, double weight)
    {
        if (!_adjacencyList.ContainsKey(u)) _adjacencyList[u] = new();
        if (!_adjacencyList.ContainsKey(v)) _adjacencyList[v] = new();
        _adjacencyList[u][v] = weight;
        _adjacencyList[v][u] = weight; //undirected graph
        _isDirty = true;
    }

    // Graph transversal
    public HashSet<string> GetConnectedDevicesDfs(string startNode)
    {
        var visited = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var stack = new Stack<string>();
        stack.Push(startNode);

        while (stack.Count > 0)
        {
            var current = stack.Pop();
            if (!visited.Contains(current))
            {
                visited.Add(current);
                if (_adjacencyList.ContainsKey(current))
                {
                    foreach (var neighbor in _adjacencyList[current].Keys)
                    {
                        if (!visited.Contains(neighbor))
                            stack.Push(neighbor);
                    }
                }
            }
        }
        return visited;
    }

    //prims minimum spaning tree algo
    public Dictionary<string, string> ComputePrimsMst(string startNode)
    {
        var mstParent = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        var minHeap = new PriorityQueue<(string Node, string Parent, double Weight), double>();
        var visited = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        if (!_adjacencyList.ContainsKey(startNode)) return mstParent;

        visited.Add(startNode);
        foreach (var neighbor in _adjacencyList[startNode])
        {
            minHeap.Enqueue((neighbor.Key, startNode, neighbor.Value), neighbor.Value);
        }

        while (minHeap.Count > 0)
        {
            var (current, parent, weight) = minHeap.Dequeue();

            if (visited.Contains(current)) continue;

            visited.Add(current);
            mstParent[current] = parent;

            if (_adjacencyList.ContainsKey(current))
            {
                foreach (var neighbor in _adjacencyList[current])
                {
                    if (!visited.Contains(neighbor.Key))
                    {
                        minHeap.Enqueue((neighbor.Key, current, neighbor.Value), neighbor.Value);
                    }
                }
            }
                
        }

        return mstParent;
    }

    // trace packet hop path
    public List<string> GetHopPathToGateway(Dictionary<string, string> mstParent, string targetNode)
    {
        var path = new List<string>();
        var current = targetNode;

        while (current != null)
        {
            path.Add(current);
            mstParent.TryGetValue(current, out current);
        }

        path.Reverse(); // Gateway to Target Node
        return path;
    }

    public void ConnectNewNodeWithNearestNeighbors(SensorDetailsResponse newSensor, List<SensorDetailsResponse> allSensors)
    {
        string newNodeName = newSensor.SensorName;

        if (!_adjacencyList.ContainsKey(newNodeName))
        {
            _adjacencyList[newNodeName] = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase);
        }

        EnsureGateWayExists();

        // defualt
        AddEdge("Gateway Router", newNodeName, 2.0);

        // select random 3, getting 3 closest nodes are to comp expensive for the time frame of the demo vid
        var neighbors = allSensors
            .Where(s => !s.SensorMAC.Equals(newSensor.SensorMAC, StringComparison.OrdinalIgnoreCase) &&
                        !s.SensorName.Equals(newNodeName, StringComparison.OrdinalIgnoreCase))
            .Take(3) 
            .ToList();

        if (neighbors.Count == 0)
        {
            return; 
        }

        foreach (var neighbor in neighbors)
        {   
            AddEdge(newNodeName, neighbor.SensorName, 1.2);
        }

    }

    public List<(string Source, string Target, double Weight)> GetAllEdges()
    {
        var edgeList = new List<(string Source, string Target, double Weight)>();
        var addedPairs = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        foreach (var kvp in _adjacencyList)
        {
            string u = kvp.Key;
            foreach (var neighbor in kvp.Value)
            {
                string v = neighbor.Key;
                double weight = neighbor.Value;

                // Prevent duplicate undirected edges
                string pairKey = string.Compare(u, v, StringComparison.OrdinalIgnoreCase) < 0 ? $"{u}-{v}" : $"{v}-{u}";
                if (addedPairs.Add(pairKey))
                {
                    edgeList.Add((u, v, weight));
                }
            }
        }
        return edgeList;
    }


}
