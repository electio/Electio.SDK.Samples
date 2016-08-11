using System.Web.Mvc;
using System.Web.Routing;
using MPD.Electio.SDK.Sample.MvcApplication.IoC;

namespace MPD.Electio.SDK.Sample.MvcApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            //register routes
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //register all of our dependencies
            IocContainer.Setup();
        }
    }
}