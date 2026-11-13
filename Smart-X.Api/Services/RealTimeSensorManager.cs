using Smart_X.Api.DataModels;

namespace Smart_X.Api.Services
{
    public class RealTimeSensorManager
    {
        private readonly object _lock = new();

        private readonly Dictionary<string, SensorDetailsResponse> _liveDevices = new(StringComparer.OrdinalIgnoreCase);
        private readonly Queue<SensorDataResponse> _telemetryQueue = new();
        private readonly PriorityQueue<SensorDataResponse, int> _priorityAlertQueue = new(); 
        private readonly Stack<string> _commandHistoryStack = new();
        private readonly SortedDictionary<DateTime, List<SensorDataResponse>> _historicalLogs = new();
        private readonly HashSet<string> _activeErrorNodes = new(StringComparer.OrdinalIgnoreCase);
        private readonly LinkedList<string> _processedFeed = new();

        private const int MaxFeed = 50;
        private const int MaxQueueSize = 1000;

        public void RegisterDevice(SensorDetailsResponse device)
        {
            lock (_lock) { _liveDevices[device.SensorMAC] = device; }
        }

        public void EnqueueTelemetry(SensorDataResponse data, bool isCritical = false, int priority = 5)
        {
            lock (_lock)
            {
                if (isCritical)
                {
                    _priorityAlertQueue.Enqueue(data, priority);
                    _activeErrorNodes.Add(data.SensorMAC);
                }
                else
                {
                    if (_telemetryQueue.Count >= MaxQueueSize) _telemetryQueue.Dequeue(); // safety cap
                    _telemetryQueue.Enqueue(data);
                    _activeErrorNodes.Remove(data.SensorMAC); // back to normal
                }

                if (!_historicalLogs.TryGetValue(data.TimeStamp, out var list))
                {
                    list = new List<SensorDataResponse>();
                    _historicalLogs[data.TimeStamp] = list;
                }
                list.Add(data);
            }
        }

        // Alerts always jump ahead of normal telemetry
        public string? ProcessNext()
        {
            lock (_lock)
            {
                string entry;
                if (_priorityAlertQueue.TryDequeue(out var alert, out var priority))
                    entry = $"[ALERT p{priority}] {alert.SensorMAC} = {alert.SensorValue} @ {alert.TimeStamp:HH:mm:ss}";
                else if (_telemetryQueue.TryDequeue(out var reading))
                    entry = $"[OK] {reading.SensorMAC} = {reading.SensorValue} @ {reading.TimeStamp:HH:mm:ss}";
                else
                    return null;

                _processedFeed.AddFirst(entry);
                while (_processedFeed.Count > MaxFeed) _processedFeed.RemoveLast();
                return entry;
            }
        }

        public void RecordManualCommand(string commandDescription)
        {
            lock (_lock) { _commandHistoryStack.Push(commandDescription); }
        }

        public string? UndoLastCommand()
        {
            lock (_lock) { return _commandHistoryStack.Count > 0 ? _commandHistoryStack.Pop() : null; }
        }

        public void ReportErrorNode(string sensorMac) { lock (_lock) { _activeErrorNodes.Add(sensorMac); } }
        public void ResolveErrorNode(string sensorMac) { lock (_lock) { _activeErrorNodes.Remove(sensorMac); } }

        public Dictionary<string, object> GetDashboardState()
        {
            lock (_lock)
            {
                return new Dictionary<string, object>
                {
                    { "LiveDevices", _liveDevices.Values.ToList() },
                    { "QueueCount", _telemetryQueue.Count },
                    { "PriorityAlertsCount", _priorityAlertQueue.Count },
                    { "UndoStackCount", _commandHistoryStack.Count },
                    { "UndoHistory", _commandHistoryStack.ToList() },   // top of stack first
                    { "ActiveErrors", _activeErrorNodes.ToList() },
                    { "HistoricalLogsCount", _historicalLogs.Count },
                    { "RecentlyProcessed", _processedFeed.ToList() }
                };
            }
        }
    }
}