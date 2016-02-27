using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Web;

namespace RestaurantService
{
    public class UnityServiceHostFactory : ServiceHostFactory
    {
        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            UnityServiceHost host = new UnityServiceHost(serviceType, baseAddresses);
            var section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            IUnityContainer unity = new UnityContainer().LoadConfiguration(section);
            host.Container = unity;

            return host;
        }
    }
}