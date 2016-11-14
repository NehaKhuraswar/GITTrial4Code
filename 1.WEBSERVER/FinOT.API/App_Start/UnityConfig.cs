using Unity.WebApi;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Web.Http;

namespace RAP.API
{
    public static class UnityConfig
    {
        public static void Register()
        {
            var container = new UnityContainer();
            container.LoadConfiguration();
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}