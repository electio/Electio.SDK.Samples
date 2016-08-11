using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using MPD.Electio.SDK.DataTypes.Common;
using MPD.Electio.SDK.DataTypes.Consignments;
using MPD.Electio.SDK.DataTypes.Consignments.Enums;
using MPD.Electio.SDK.DataTypes.DeliveryOptions;
using MPD.Electio.SDK.Error;
using MPD.Electio.SDK.Interfaces.Services;
using MPD.Electio.SDK.Sample.MvcApplication.Models.DeliveryOptions;
using Address = MPD.Electio.SDK.DataTypes.Consignments.Address;

namespace MPD.Electio.SDK.Sample.MvcApplication.Controllers.DeliveryOptions
{
    [RoutePrefix("deliveryoptions")]
    public class DeliveryOptionsController : Controller
    {
        private readonly IShippingLocationsService _shippingLocationsService;
        private readonly IDeliveryOptionsService _deliveryOptionsService;

        public DeliveryOptionsController(
            IShippingLocationsService shippingLocationsService,
            IDeliveryOptionsService deliveryOptionsService)
        {
            _shippingLocationsService = shippingLocationsService;
            _deliveryOptionsService = deliveryOptionsService;
        }

        [HttpGet]
        [Route]
        public async Task<ActionResult> Index()
        {
            var model = await BuildMockRequest();

            var result = await _deliveryOptionsService.GetDeliveryOptionsAsync(model.Request);

            model.Response = result;

            return View("~/Views/DeliveryOptions/Index.cshtml", model);
        }

        [HttpPost]
        [Route("{deliveryOptionReference}/select")]
        public async Task<ActionResult> SelectDeliveryOption(string deliveryOptionReference)
        {
            try
            {
                var result = await _deliveryOptionsService.SelectOptionAsync(deliveryOptionReference);
                if (result != null && result.Any())
                {
                    var apiLink = result.First();
                    return View("~/Views/DeliveryOptions/Selected.cshtml", apiLink);
                }

                ViewBag.Message = $"Delivery option {deliveryOptionReference} could not be selected. Please choose another.";
                return RedirectToAction("Index", "DeliveryOptions");
            }
            catch (ApiException apiEx)
            {
                ViewBag.Message =
                    $"An error occurred trying to select option {deliveryOptionReference}. {apiEx.Message} :: {apiEx.Error.Message}";
                return RedirectToAction("Index", "DeliveryOptions");
            }
            catch (Exception)
            {
                ViewBag.Message =
                    $"An unexpected error occurred trying to select option {deliveryOptionReference}. Please try again";
                return RedirectToAction("Index", "DeliveryOptions");
            }
        }

        /// <summary>
        /// In order to demonstrate the functionality of delivery options, here we are
        /// creating a fictional basket containing a single item,
        /// and we are going to request options for the user's registered address.
        /// <remarks>
        /// In a standard e-commerce platform flow you could pre-load delivery options
        /// based on logged in users' registered / known delivery address(es) or
        /// request options on the fly using manually entered addresses.
        /// </remarks>
        /// </summary>
        /// <returns>A pre-populated delivery options request</returns>
        private async Task<DeliveryOptionsViewModel> BuildMockRequest()
        {
            var shippingLocations = await _shippingLocationsService.GetAssignedShippingLocationsAsync();
            if (!shippingLocations.Any())
            {
                throw new Exception("Could not find any assigned shipping locations. Ensure you have at least one shipping location assigned to your Electio account.");
            }

            var selectedLocation = shippingLocations.First(); //here we are pre-selecting a shipping location from your available locations

            var request = new DeliveryOptionsRequest()
            {
                Addresses = new List<Address>()
                {
                    new Address()
                    {
                        AddressType = ConsignmentAddressType.Origin,
                        ShippingLocationReference = selectedLocation.Reference
                    },
                    new Address() //we are mocking the user's registered address here
                    {
                        AddressType = ConsignmentAddressType.Destination,
                        Contact = new Contact()
                        {
                            Title = "Mr",
                            FirstName = "Test",
                            LastName = "McTest",
                            Email = "test.mctest@test.com",
                            Telephone = "0161123456"
                        },
                        AddressLine1 = "123 Test Street",
                        AddressLine2 = "Testington",
                        Town = "Testington",
                        Region = "Testishire",
                        Country = new Country()
                        {
                            IsoCode = new IsoCode()
                            {
                                TwoLetterCode = "GB"
                            }
                        },
                        Postcode = "M1 5WG"
                    }
                },
                Packages = new List<Package>()
                {
                    new Package()
                    {
                        Description = "100% Pure Cotton Socks",
                        PackageReferenceProvidedByCustomer = "PCSKS_22",
                        Value = new Money()
                        {
                            Currency = new Currency()
                            {
                                IsoCode = "GBP"
                            },
                            Amount = 2.99M
                        },
                        Dimensions = new Dimensions()
                        {
                            Length = 10,
                            Width = 5,
                            Height = 5
                        },
                        Weight = new Weight()
                        {
                            Kg = 0.2M
                        }
                    }
                }
            };

            return new DeliveryOptionsViewModel()
            {
                Request = request
            };
        }
    }
}