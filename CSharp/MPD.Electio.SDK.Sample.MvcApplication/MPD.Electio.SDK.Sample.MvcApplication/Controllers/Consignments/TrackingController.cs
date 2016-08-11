using System;
using System.Web.Mvc;
using MPD.Electio.SDK.Error;
using MPD.Electio.SDK.Interfaces.Services;

namespace MPD.Electio.SDK.Sample.MvcApplication.Controllers.Consignments
{
    [RoutePrefix("tracking")]
    public class TrackingController : Controller
    {
        private readonly ITrackingService _trackingService;

        public TrackingController(ITrackingService trackingService)
        {
            _trackingService = trackingService;
        }

        [HttpGet]
        [Route("{consignmentReference}")]
        public ActionResult GetTrackingEvents(string consignmentReference)
        {
            try
            {
                var viewModel =
                    _trackingService.GetFlattenedTrackingEventsForEachPackageByConsignment(consignmentReference);
                return View("~/Views/Tracking/Index.cshtml", viewModel);

            }
            catch (ApiException apiEx)
            {
                ViewBag.Message =
                    $"Could not get tracking events for consignment {consignmentReference}. {apiEx.Message} :: {apiEx.Error.Message}";
                return View("~/Views/Shared/Error.cshtml");
            }
            catch (Exception)
            {
                ViewBag.Message = $"An unexpected error occurred getting tracking events for consignment {consignmentReference}";
                return View("~/Views/Shared/Error.cshtml");
                
            }
        }
    }
}