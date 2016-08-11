using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using MPD.Electio.SDK.Interfaces.Services;

namespace MPD.Electio.SDK.Sample.MvcApplication.Controllers.Consignments
{
    [RoutePrefix("consignments")]
    public class QuotesController : Controller
    {
        private readonly IQuoteService _quoteService;

        public QuotesController(IQuoteService quoteService)
        {
            _quoteService = quoteService;
        }

        [HttpGet]
        [Route("{consignmentReference}/quotes")]
        public async Task<ActionResult> GetQuotes(string consignmentReference)
        {
            try
            {
                var quoteResult = await _quoteService.CreateQuotesAsync(consignmentReference);
                return View("~/Views/Quotes/Index.cshtml", quoteResult);
            }
            catch (Exception)
            {
                ViewBag.Message = $"An error occurred getting quotes for consignment {consignmentReference}";
                return View("~/Views/Shared/Error.cshtml");
            }
        }
    }
}