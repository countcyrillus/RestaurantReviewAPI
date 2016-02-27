using Microsoft.Practices.Unity;
using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace RestaurantService
{
    public class UnityInstanceProvider : IInstanceProvider
    {
        public IUnityContainer Container { get; set; }
        public Type ServiceType { get; set; }

        public UnityInstanceProvider() : this(null)
        {

        }

        public UnityInstanceProvider(Type type)
        {
            ServiceType = type;
            Container = new UnityContainer();
        }

        public object GetInstance(InstanceContext instanceContext)
        {
            return GetInstance(instanceContext, null);
        }

        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            return Container.Resolve(ServiceType);
        }

        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {

        }
    }
}