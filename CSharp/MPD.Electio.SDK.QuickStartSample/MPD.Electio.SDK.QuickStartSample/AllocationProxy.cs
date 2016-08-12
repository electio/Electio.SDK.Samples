using System;
using MPD.Electio.SDK.Endpoints;
using MPD.Electio.SDK.Error;
using MPD.Electio.SDK.Interfaces;
using MPD.Electio.SDK.Services;

namespace MPD.Electio.SDK.QuickStartSample
{
    internal class AllocationProxy
    {
        private readonly string _apiKey;
        private readonly IEndpoints _endpoints;
        private readonly ILogger _logger;

        internal AllocationProxy()
        {
            _apiKey = System.Configuration.ConfigurationManager.AppSettings["ApiKey"];
            _endpoints = Production.Instance;
            _logger = new SdkReferenceLogger();
        }

        internal void AllocateConsignment(string consignmentReference)
        {
            var allocationService = new ConsignmentAllocationService(_apiKey, _endpoints, _logger);

            try
            {
                allocationService.AllocateConsignment(consignmentReference);
                Console.WriteLine("Consignment allocated successfully!");
            }
            catch (ApiException apiEx)
            {
                Console.WriteLine("An error occurred allocating the consignment.");
                Console.WriteLine(apiEx.Message);
                Console.WriteLine(apiEx.Error?.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred allocating the consignment.");
                Console.WriteLine(ex.Message);
            }
        }
    }
}