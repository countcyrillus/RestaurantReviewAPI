using Infrastructure.Interfaces;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;

namespace DataAccessLayer.Tests
{
    [TestClass()]
    public class TestBase
    {
        protected IUnityContainer _container;
        protected IDataAccessLayer _dataAccessLayer;

        [TestInitialize()]
        public void initialize()
        {
            var section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            _container = new UnityContainer().LoadConfiguration(section);

            _dataAccessLayer = _container.Resolve<IDataAccessLayer>();
        }
    }
}
