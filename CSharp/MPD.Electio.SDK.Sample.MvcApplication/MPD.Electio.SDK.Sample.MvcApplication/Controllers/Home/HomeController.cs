using System.Web.Mvc;

namespace MPD.Electio.SDK.Sample.MvcApplication.Controllers.Home
{
    [RoutePrefix("")]
    public class HomeController : BaseController
    {
        [HttpGet]
        [Route]
        public ActionResult Index()
        {
            return View("~/Views/Home/Index.cshtml");
        }
    }
}