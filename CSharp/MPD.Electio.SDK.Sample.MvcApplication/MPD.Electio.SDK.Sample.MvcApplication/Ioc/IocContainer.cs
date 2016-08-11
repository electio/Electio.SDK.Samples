using System.Web.Mvc;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace MPD.Electio.SDK.Sample.MvcApplication.IoC
{
    public static class IocContainer
    {
        public static void Setup()
        {
            var container = new WindsorContainer().Install(FromAssembly.This());

            var controllerFactory = new WindsorControllerFactory(container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }
    }
}