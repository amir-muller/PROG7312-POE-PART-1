using Smart_X.Api.DataModels;

namespace Smart_X.Api.Services
{
    public class RealTimeSensorManager
    {
        //dictionary
        private readonly Dictionary<string, SensorDetailsResponse> _liveDevices = new(StringComparer.OrdinalIgnoreCase);

        //tele queue
        private readonly Queue<SensorDataResponse> _telemetryQueue = new();

        //prio queue
        private readonly PriorityQueue<SensorDataResponse, int> _priorityAlertQueue = new();

        //stack
        private readonly Stack<string> _commandHistoryStack = new();

        //sorteddic
        private readonly SortedDictionary<DateTime, List<SensorDataResponse>> _historicalLogs = new();

        //hashset
        private readonly HashSet<string> _activeErrorNodes = new(StringComparer.OrdinalIgnoreCase);

        public void RegisterDevice(SensorDetailsResponse device)
        {
            _liveDevices[device.SensorMAC] = device;
        }

        public void EnqueueTelemetry(SensorDataResponse data, bool isCritical = false, int priority = 5)
        {
            if (isCritical)
            {
                _priorityAlertQueue.Enqueue(data, priority);
            }
            else
            {
                _telemetryQueue.Enqueue(data);
            }

            // Add to SortedDictionary history
            if (!_historicalLogs.ContainsKey(data.TimeStamp))
            {
                _historicalLogs[data.TimeStamp] = new List<SensorDataResponse>();
            }
            _historicalLogs[data.TimeStamp].Add(data);
        }


        public void RecordManualCommand(string commandDescription)
        {
            _commandHistoryStack.Push(commandDescription);
        }

        public string? UndoLastCommand()
        {
            return _commandHistoryStack.Count > 0 ? _commandHistoryStack.Pop() : null;
        }

        public void ReportErrorNode(string sensorMac)
        {
            _activeErrorNodes.Add(sensorMac);
        }

        public void ResolveErrorNode(string sensorMac)
        {
            _activeErrorNodes.Remove(sensorMac);
        }

        public Dictionary<string, object> GetDashboardState()
        {
            return new Dictionary<string, object>
        {
            { "LiveDevices", _liveDevices.Values.ToList() },
            { "QueueCount", _telemetryQueue.Count },
            { "PriorityAlertsCount", _priorityAlertQueue.Count },
            { "UndoStackCount", _commandHistoryStack.Count },
            { "ActiveErrors", _activeErrorNodes.ToList() },
            { "HistoricalLogsCount", _historicalLogs.Count }
        };
        }




    }
}
