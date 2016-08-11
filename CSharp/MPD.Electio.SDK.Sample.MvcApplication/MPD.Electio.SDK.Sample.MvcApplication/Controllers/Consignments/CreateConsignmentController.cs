using System;
using System.Linq;
using System.Web.Mvc;
using System.Threading.Tasks;
using MPD.Electio.SDK.Interfaces.Services;
using MPD.Electio.SDK.Sample.MvcApplication.Models.Consignments;
using MPD.Electio.SDK.Error;
using MPD.Electio.SDK.Sample.MvcApplication.Extensions;
using MPD.Electio.SDK.Sample.MvcApplication.Mapping;

namespace MPD.Electio.SDK.Sample.MvcApplication.Controllers.Consignments
{
    [RoutePrefix("consignments")]
    public class CreateConsignmentController : BaseController
    {
        private readonly IConsignmentService _consignmentService;
        private readonly IShippingLocationsService _shippingLocationsService;
        private readonly IStaticDataService _staticDataService;

        public CreateConsignmentController(
            IConsignmentService consignmentService,
            IShippingLocationsService shippingLocationsService,
            IStaticDataService staticDataService)
        {
            _consignmentService = consignmentService;
            _shippingLocationsService = shippingLocationsService;
            _staticDataService = staticDataService;
        }

        [HttpGet]
        [Route("create")]
        public async Task<ActionResult> Index()
        {
            var viewModel = await BuildViewModel();

            return View("~/Views/CreateConsignment/Index.cshtml", viewModel);
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> CreateConsignment(CreateConsignmentViewModel request)
        {
            if (!ModelState.IsValid)
            {
                var model = await BuildViewModel();
                model.CreateConsignmentRequest = request.CreateConsignmentRequest;
                return View("~/Views/CreateConsignment/Index.cshtml", model);
            }

            try
            {
                var electioRequest = ConsignmentMapping.MapCreateConsignmentRequest(request.CreateConsignmentRequest);
                
                var created = _consignmentService.CreateConsignment(electioRequest);

                var defaultModel = await BuildViewModel();
                defaultModel.CreateConsignmentRequest = request.CreateConsignmentRequest;

                if (created != null)
                {
                    var createdLink = created.FirstOrDefault();
                    defaultModel.CreatedConsignmentLink = createdLink?.Href;
                    defaultModel.Created = true;
                }
                else
                {
                    defaultModel.CreatedConsignmentLink = string.Empty;
                    defaultModel.Created = false;
                }

                return View("~/Views/CreateConsignment/Index.cshtml", defaultModel);
            }
            catch (ApiException apiEx) //the electio SDK will throw an ApiException for failed calls
            {
                ViewBag.Message = $"The API responded with an error: {apiEx.Message} :: {apiEx.Error.Message}";
                return View("~/Views/Shared/Error.cshtml");
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"An error occurred creating the consignment. {ex.Message}";
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        private async Task<CreateConsignmentViewModel> BuildViewModel()
        {
            //This will retrieve all shipping locations that your account has assigned to them
            //you can only create consignments for shipping locations to which you have been granted access
            var getShippingLocations = _shippingLocationsService.GetAssignedShippingLocationsAsync();

            //This will retrieve the currencies supported by Electio
            var getCurrencies = _staticDataService.GetSupportedCurrenciesAsync();

            //This will retrieve the countries supported by Electio
            var getCountries = _staticDataService.GetSupportedCountriesAsync();

            //This will retrieve the supported titles, e.g. Mr, Mrs
            var getTitles = _staticDataService.GetSupportedTitlesAsync();

            //Here we execute all three tasks asynchronously
            await Task.WhenAll(getShippingLocations, getCurrencies, getCountries, getTitles);

            var shippingLocations = await getShippingLocations; //get the result
            var currencies = await getCurrencies; //get the result
            var countries = await getCountries; //get the result
            var titles = await getTitles;

            return new CreateConsignmentViewModel()
            {
                CreateConsignmentRequest = new CreateConsignmentRequestViewModel(),
                Currencies = currencies.ToSelectList(),
                ShippingLocations = shippingLocations.ToSelectList(),
                Countries = countries.ToSelectList(),
                Titles = titles.ToSelectList()
            };
        }
    }
}