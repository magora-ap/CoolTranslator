using CoolTranslator.Core.Abstract;
using CoolTranslator.RestApi.Concrete;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;

namespace CoolTranslator.RestApi
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IPathMapper, PathMapper>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}