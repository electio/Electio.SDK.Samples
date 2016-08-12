using System;
using System.Collections.Generic;
using MPD.Electio.SDK.DataTypes.Consignments;
using MPD.Electio.SDK.Endpoints;
using MPD.Electio.SDK.Error;
using MPD.Electio.SDK.Interfaces;
using MPD.Electio.SDK.Services;

namespace MPD.Electio.SDK.QuickStartSample
{
    internal class ManifestProxy
    {
        private readonly string _apiKey;
        private readonly IEndpoints _endpoints;
        private readonly ILogger _logger;

        public ManifestProxy()
        {
            _apiKey = System.Configuration.ConfigurationManager.AppSettings["ApiKey"];
            _endpoints = Production.Instance;
            _logger = new SdkReferenceLogger();
        }

        internal void ManifestConsignment(string consignmentReference)
        {
            var consignmentService = new ConsignmentService(_apiKey, _endpoints, _logger);
            var request = new ManifestConsignmentsRequest()
            {
                ConsignmentReferences = new List<string>()
                {
                    consignmentReference
                }
            };

            try
            {
                consignmentService.ManifestConsignments(request);
                Console.WriteLine("Consignment queued for manifest successfully.");
            }
            catch (ApiException apiEx)
            {
                Console.WriteLine("An error occurred manifesting the consignment.");
                Console.WriteLine(apiEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred manifesting the consignment.");
                Console.WriteLine(ex.Message);
            }
        }
    }
}