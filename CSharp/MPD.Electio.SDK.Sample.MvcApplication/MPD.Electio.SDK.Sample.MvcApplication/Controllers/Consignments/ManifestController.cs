using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using MPD.Electio.SDK.DataTypes.Consignments;
using MPD.Electio.SDK.Interfaces.Services;

namespace MPD.Electio.SDK.Sample.MvcApplication.Controllers.Consignments
{
    [RoutePrefix("consignments/manifest")]
    public class ManifestController : BaseController
    {
        private readonly IConsignmentService _consignmentService;

        public ManifestController(IConsignmentService consignmentService)
        {
            _consignmentService = consignmentService;
        }

        [HttpPost]
        [Route("{consignmentReference}")]
        public async Task<ActionResult> ManifestConsignment(string consignmentReference)
        {
            try
            {
                var request = new ManifestConsignmentsRequest()
                {
                    ConsignmentReferences = new List<string>()
                    {
                        consignmentReference
                    }
                };

                await _consignmentService.ManifestConsignmentsAsync(request);
                ViewBag.Message = $"Consignment {consignmentReference} queued for manifest successfully.";
                ViewBag.RelativeLink = "/consignments/manifested";
                return View("~/Views/Shared/Success.cshtml");
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"An error occurred trying to manifest consignment {consignmentReference}";
                return View("~/Views/Shared/Error.cshtml");
            }
        }
    }
}