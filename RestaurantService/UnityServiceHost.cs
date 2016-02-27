using Microsoft.Practices.Unity;
using System;
using System.ServiceModel;

namespace RestaurantService
{
    public class UnityServiceHost : ServiceHost
    {
        public IUnityContainer Container { set; get; }

        public UnityServiceHost() : base()
        {
            Container = new UnityContainer();
        }

        public UnityServiceHost(Type serviceType, params Uri[] baseAddresses) : base(serviceType, baseAddresses)
        {
            Container = new UnityContainer();
        }

        protected override void OnOpening()
        {
            new UnityServiceBehavior(Container).AddToHost(this);
            base.OnOpening();
        }
    }
}