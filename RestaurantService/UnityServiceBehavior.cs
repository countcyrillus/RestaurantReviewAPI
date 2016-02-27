using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Web;

namespace RestaurantService
{
    public class UnityServiceBehavior : IServiceBehavior
    {
        public UnityInstanceProvider InstanceProvider { get; set; }
        private ServiceHost serviceHost = null;

        public UnityServiceBehavior()
        {
            InstanceProvider = new UnityInstanceProvider();
        }

        public UnityServiceBehavior(IUnityContainer unity)
        {
            InstanceProvider = new UnityInstanceProvider();
            InstanceProvider.Container = unity;
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcherBase cdb in serviceHostBase.ChannelDispatchers)
            {
                ChannelDispatcher cd = cdb as ChannelDispatcher;
                if (cd != null)
                {
                    foreach (EndpointDispatcher ed in cd.Endpoints)
                    {
                        InstanceProvider.ServiceType = serviceDescription.ServiceType;
                        ed.DispatchRuntime.InstanceProvider = InstanceProvider;
                    }
                }
            }
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        { }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        { }

        public void AddToHost(ServiceHost host)
        {
            // only add to host once
            if (serviceHost != null) return;
            host.Description.Behaviors.Add(this);
            serviceHost = host;
        }
    }
}