using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using MPD.Electio.SDK.Error;
using MPD.Electio.SDK.Interfaces.Services;

namespace MPD.Electio.SDK.Sample.MvcApplication.Controllers.Consignments
{
    [RoutePrefix("labels")]
    public class LabelsController : BaseController
    {
        private readonly ILabelService _labelsService;

        public LabelsController(ILabelService labelsService)
        {
            _labelsService = labelsService;
        }

        [Route("{consignmentReference}")]
        public async Task<ActionResult> GetLabels(string consignmentReference)
        {
            try
            {
                var label = await _labelsService.GetLabelsAsync(consignmentReference);
                if (label != null && label.File.Length > 0)
                {
                    return File(label.File, label.ContentType, $"{consignmentReference}-label.pdf");
                }

                ViewBag.Message = $"No label could be found for consignment {consignmentReference}";
                return View("~/Views/Shared/Error.cshtml");
            }
            catch (ApiException apiEx)
            {
                ViewBag.Message =
                    $"An error occurred retrieving labels for consignment {consignmentReference}. {apiEx.Message} :: {apiEx.Error.Message}";
                return View("~/Views/Shared/Error.cshtml");
            }
            catch (Exception)
            {
                ViewBag.Message =
                    $"An unexpected error occurred retrieving labels for consignment {consignmentReference}.";
                return View("~/Views/Shared/Error.cshtml");
            }
        }
    }
}