using System.Threading.Tasks;
using System.Web.Mvc;
using MPD.Electio.SDK.DataTypes.Consignments;
using MPD.Electio.SDK.Interfaces.Services;

namespace MPD.Electio.SDK.Sample.MvcApplication.Controllers.Consignments
{
    [RoutePrefix("consignments")]
    public class GetConsignmentsController : Controller
    {
        private readonly IConsignmentService _consignmentService;

        public GetConsignmentsController(IConsignmentService consignmentService)
        {
            _consignmentService = consignmentService;
        }

        [HttpGet]
        [Route("view/{state?}")]
        public async Task<ActionResult> Index(string state)
        {
            var searchTerms = new ConsignmentSearchTerms()
            {
                State = state
            };
            var consignments = await _consignmentService.SearchConsignmentsAsync(searchTerms);
            return View("~/Views/GetConsignments/Index.cshtml", consignments);
        }
    }
}