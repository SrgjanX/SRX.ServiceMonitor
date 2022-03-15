//srgjanx

namespace SRX.ServiceMonitor.Models
{
    public class ProcessInfo
    {
        public string DisplayName { get; set; }
        public string ProcessName { get; set; }
        public string FilePath { get; set; }
        public ProcessStatus Status { get; set; }
    }
}