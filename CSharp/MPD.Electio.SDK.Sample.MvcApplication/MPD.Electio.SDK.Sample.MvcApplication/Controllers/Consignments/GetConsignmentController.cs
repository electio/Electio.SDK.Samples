using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPD.Electio.SDK.Interfaces.Services;

namespace MPD.Electio.SDK.Sample.MvcApplication.Controllers.Consignments
{
    [RoutePrefix("consignment")]
    public class GetConsignmentController : BaseController
    {
        private readonly IConsignmentService _consignmentService;

        public GetConsignmentController(IConsignmentService consignmentService)
        {
            _consignmentService = consignmentService;
        }

        [HttpGet]
        [Route("{consignmentReference}")]
        public ActionResult GetConsignment(string consignmentReference)
        {
            var consignment = _consignmentService.GetConsignment(consignmentReference);
            return View("~/Views/GetConsignment/Index.cshtml", consignment);
        }
    }
}