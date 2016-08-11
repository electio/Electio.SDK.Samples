using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using MPD.Electio.SDK.Interfaces.Services;

namespace MPD.Electio.SDK.Sample.MvcApplication.Controllers.Consignments
{
    [RoutePrefix("consignments/allocate")]
    public class AllocateController : BaseController
    {
        private readonly IConsignmentAllocationService _consignmentAllocationService;

        public AllocateController(IConsignmentAllocationService consignmentAllocationService)
        {
            _consignmentAllocationService = consignmentAllocationService;
        }

        [HttpPost]
        [Route("{consignmentReference}/{quoteReference:guid}")]
        public async Task<ActionResult> AllocateWithQuote(string consignmentReference, Guid quoteReference)
        {
            try
            {
                await _consignmentAllocationService.AllocateConsignmentAsync(consignmentReference, quoteReference);
                ViewBag.Message =
                    $"Quote reference {quoteReference} has been selected for consignment {consignmentReference}";
                ViewBag.RelativeLink = "/consignments/allocated";
                return View("~/Views/Shared/Success.cshtml");
            }
            catch (Exception ex)
            {
                ViewBag.Message =
                    $"An error occurred selecting quote {quoteReference} for consignment {consignmentReference}";
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        [HttpPost]
        [Route("{consignmentReference}")]
        public async Task<ActionResult> Allocate(string consignmentReference)
        {
            try
            {
                await _consignmentAllocationService.AllocateConsignmentAsync(consignmentReference);
                ViewBag.Message = $"Consignment {consignmentReference} has been queued for allocation successfully.";
                ViewBag.RelativeLink = "/consignments/allocated";
                return View("~/Views/Shared/Success.cshtml");
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"An error occurred trying to allocate consignment {consignmentReference}";
                return View("~/Views/Shared/Error.cshtml");
            }
        }
    }
}