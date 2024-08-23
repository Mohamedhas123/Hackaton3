using System.Reflection.PortableExecutable;

namespace HackatonGroup3_Web.Models
{
    public class HomeViewModel
    {
        public string? MachineStatus { get; set; } = "Disconnected";
        public List<Output>? Outputs { get; set; } = new() 
        {
            new () { DisplayName= "Control Voltage On", State=false },
            new () { DisplayName= "Control Voltage On Indicator", State=false },
            new () { DisplayName= "System On", State=false },
            new () { DisplayName= "System On Indicator", State=false },
            new () { DisplayName= "Stop Unlock", State=false },
            new () { DisplayName= "Stop Operated Indicator", State=false },
            new () { DisplayName= "Collecting Failure Indicator", State=false },
            new () { DisplayName= "Starting Alarm Horn", State=false },
        }; 
    }
}
