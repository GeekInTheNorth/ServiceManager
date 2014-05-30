using System.ServiceProcess;

namespace ServiceManager.Models
{
    public class ServiceModel
    {
        public string DisplayName { get; set; }

        public string ServiceName { get; set; }

        public ServiceControllerStatus Status { get; set; }
    }
}