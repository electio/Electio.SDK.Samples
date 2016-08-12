using System;
using MPD.Electio.SDK.DataTypes.LabelGeneration;
using MPD.Electio.SDK.Endpoints;
using MPD.Electio.SDK.Error;
using MPD.Electio.SDK.Interfaces;
using MPD.Electio.SDK.Services;

namespace MPD.Electio.SDK.QuickStartSample
{
    internal class LabelProxy
    {
        private readonly string _apiKey;
        private readonly IEndpoints _endpoints;
        private readonly ILogger _logger;

        public LabelProxy()
        {
            _apiKey = System.Configuration.ConfigurationManager.AppSettings["ApiKey"];
            _endpoints = Production.Instance;
            _logger = new SdkReferenceLogger();
        }

        internal void GetLabelForConsignment(string consignmentReference)
        {
            var labelService = new LabelService(_apiKey, _endpoints, _logger);
            try
            {
                var label = labelService.GetLabels(consignmentReference);
                var outputPath = WriteLabel(label, consignmentReference);
                Console.WriteLine($"File written to {outputPath}");
            }
            catch (ApiException apiEx)
            {
                Console.WriteLine("An error occurred getting the label.");
                Console.WriteLine(apiEx.Message);
                Console.WriteLine(apiEx.Error?.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred getting the label.");
                Console.WriteLine(ex.Message);
            }
        }

        private static string WriteLabel(GetLabelsResponse labelsResponse, string consignmentReference)
        {
            const string OUTPUT_PATH = "C:\\temp\\";
            if (!System.IO.Directory.Exists(OUTPUT_PATH))
            {
                throw new Exception("Cannot save file - specified path does not exist.");
            }

            var fileName = $"{consignmentReference}-label.pdf";
            var fullOutputPath = System.IO.Path.Combine(OUTPUT_PATH, fileName);
            System.IO.File.WriteAllBytes(fullOutputPath, labelsResponse.File);
            return fullOutputPath;
        }
    }
}