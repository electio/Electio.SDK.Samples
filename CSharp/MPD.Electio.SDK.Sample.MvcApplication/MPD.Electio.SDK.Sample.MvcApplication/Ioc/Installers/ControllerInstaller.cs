using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace MPD.Electio.SDK.Sample.MvcApplication.Ioc.Installers
{
    public class ControllerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //register all instances of IController with the castle windsor container
            container.Register(Classes.FromThisAssembly()
                .BasedOn<IController>()
                .LifestyleTransient());
        }
    }
}