using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using Microsoft.Win32;
using ServiceManager.Models;

namespace ServiceManager.Repositories
{
    public class WindowsServices
    {
        public IEnumerable<ServiceModel> GetConfiguredServices()
        {
            var serviceModels = new List<ServiceModel>();

            var reg = GetApplicationRegistryKey();
            if (reg != null)
            {
                var serviceNames = reg.GetValueNames();
                foreach (var serviceName in serviceNames)
                {
                    var valueKind = reg.GetValueKind(serviceName);
                    if (valueKind == RegistryValueKind.String)
                    {
                        var serviceDescription = reg.GetValue(serviceName);
                        var serviceModel = new ServiceModel
                            {
                                ServiceName = serviceName,
                                DisplayName = serviceDescription.ToString()
                            };
                        GetStatus(serviceModel);
                        serviceModels.Add(serviceModel);
                    }
                }
            }

            return serviceModels;
        }

        public IEnumerable<ServiceModel> GetWindowsServices()
        {
            var services = ServiceController.GetServices();

            var serviceModels = (from service in services
                                 select new ServiceModel
                                     {
                                         ServiceName = service.ServiceName,
                                         DisplayName = service.DisplayName,
                                         Status = service.Status
                                     }).AsEnumerable();

            return serviceModels;
        }

        public void GetStatus(ServiceModel serviceModel)
        {
            var sc = new ServiceController(serviceModel.ServiceName);

            serviceModel.Status = sc.Status;
        }

        public void StartService(ServiceModel serviceModel)
        {
            var sc = new ServiceController(serviceModel.ServiceName);
            if ((sc.Status == ServiceControllerStatus.Stopped) || (sc.Status == ServiceControllerStatus.Paused))
            {
                sc.Start();
                sc.WaitForStatus(ServiceControllerStatus.Running);
            }

            serviceModel.Status = sc.Status;
        }

        public void StopService(ServiceModel serviceModel)
        {
            var sc = new ServiceController(serviceModel.ServiceName);
            if ((sc.Status == ServiceControllerStatus.Running) || (sc.Status == ServiceControllerStatus.Paused))
            {
                sc.Stop();
                sc.WaitForStatus(ServiceControllerStatus.Stopped);
            }

            serviceModel.Status = sc.Status;
        }

        public void RemoveServiceReference(ServiceModel serviceModel)
        {
            var reg = GetApplicationRegistryKey();
            if (reg == null) return;

            var serviceNames = reg.GetValueNames();
            if (serviceNames.Contains(serviceModel.ServiceName))
                reg.DeleteValue(serviceModel.ServiceName);
        }

        private RegistryKey GetApplicationRegistryKey()
        {
            return Registry.LocalMachine.OpenSubKey("SOFTWARE\\Solmundr\\ServiceManager", RegistryKeyPermissionCheck.ReadWriteSubTree);
        }

        public void AddServiceReference(ServiceModel serviceModel)
        {
            var reg = GetApplicationRegistryKey();
            if (reg == null) return;

            var serviceNames = reg.GetValueNames();
            if (serviceNames.Contains(serviceModel.ServiceName))
                return;

            reg.SetValue(serviceModel.ServiceName, serviceModel.DisplayName);
        }
    }
}