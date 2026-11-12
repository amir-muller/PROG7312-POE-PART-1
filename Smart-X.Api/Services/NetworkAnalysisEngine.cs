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

    public NetworkAnalysisEngine()
    {
        // mesh network for testing rn
        AddEdge("Gateway Router", "Node A", 1.2);
        AddEdge("Gateway Router", "Node B", 2.5);
        AddEdge("Gateway Router", "Node C", 1.8);
        AddEdge("Node A", "Node B", 1.0);
        AddEdge("Node A", "Node D", 3.1);
        AddEdge("Node B", "Node C", 1.4);
        AddEdge("Node B", "Node D", 2.0);
        AddEdge("Node B", "Node E", 2.2);
        AddEdge("Node C", "Node E", 1.5);
        AddEdge("Node D", "Node E", 1.1);
        AddEdge("Node D", "Node F", 2.8);
        AddEdge("Node E", "Node F", 1.9);
        AddEdge("Node F", "Gateway Router", 4.0);
    }

    public void AddEdge(string u, string v, double weight)
    {
        if (!_adjacencyList.ContainsKey(u)) _adjacencyList[u] = new();
        if (!_adjacencyList.ContainsKey(v)) _adjacencyList[v] = new();
        _adjacencyList[u][v] = weight;
        _adjacencyList[v][u] = weight; //undirected graph
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

            foreach (var neighbor in _adjacencyList[current])
            {
                if (!visited.Contains(neighbor.Key))
                {
                    minHeap.Enqueue((neighbor.Key, current, neighbor.Value), neighbor.Value);
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
}




}