using System.Reflection.PortableExecutable;

namespace HackatonGroup3_Web.Models
{
    public class HomeViewModel
    {
        public string? MachineStatus { get; set; } = "Disconnected";
        public List<Output>? Outputs { get; set; } = new() 
        {
            new () { DisplayName= "Motor 1", State=false },
            new () { DisplayName= "Motor 2", State=false }
        }; 
    }
}
