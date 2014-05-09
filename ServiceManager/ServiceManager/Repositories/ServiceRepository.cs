using System.Collections.Generic;
using Microsoft.Win32;
using ServiceManager.Models;

namespace ServiceManager.Repositories
{
    public class ServiceRepository
    {
        public IEnumerable<Service> GetAll()
        {
            var services = new List<Service>();

            var reg = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Solmundr\\ServiceManager", RegistryKeyPermissionCheck.ReadWriteSubTree);
            if (reg != null)
            {
                var serviceNames = reg.GetValueNames();
                foreach (var serviceName in serviceNames)
                {
                    var valueKind = reg.GetValueKind(serviceName);
                    if (valueKind == RegistryValueKind.String)
                    {
                        var serviceDescription = reg.GetValue(serviceName);
                        services.Add(new Service
                            {
                                ServiceName = serviceName,
                                DisplayName = serviceDescription.ToString()
                            });
                    }
                }
            }

            return services;
        }
    }
}