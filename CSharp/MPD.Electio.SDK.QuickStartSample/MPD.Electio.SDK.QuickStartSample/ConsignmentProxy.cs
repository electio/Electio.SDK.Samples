using System;
using System.Collections.Generic;
using System.Linq;
using MPD.Electio.SDK.DataTypes.Common;
using MPD.Electio.SDK.DataTypes.Consignments;
using MPD.Electio.SDK.DataTypes.Consignments.Enums;
using MPD.Electio.SDK.Endpoints;
using MPD.Electio.SDK.Error;
using MPD.Electio.SDK.Interfaces;
using MPD.Electio.SDK.Services;
using Address = MPD.Electio.SDK.DataTypes.Consignments.Address;

namespace MPD.Electio.SDK.QuickStartSample
{
    internal class ConsignmentProxy
    {
        private readonly string _apiKey;
        private readonly IEndpoints _endpoints;
        private readonly ILogger _logger;

        public ConsignmentProxy()
        {
            _apiKey = System.Configuration.ConfigurationManager.AppSettings["ApiKey"];
            _endpoints = Production.Instance;
            _logger = new SdkReferenceLogger();
        }

        internal void CreateNewConsignment()
        {
            var consignmentService = new ConsignmentService(_apiKey, _endpoints, _logger);
            var request = SampleCreateConsignmentRequest();

            try
            {
                var result = consignmentService.CreateConsignment(request);
                if (result.Any())
                {
                    foreach (var apiLink in result)
                    {
                        Console.WriteLine(apiLink.Href);
                    }
                }
            }
            catch (ApiException apiEx)
            {
                Console.WriteLine("An error occurred creating the consignment.");
                Console.WriteLine(apiEx.Message);
                Console.WriteLine(apiEx.Error?.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred creating the consignment.");
                Console.WriteLine(ex.Message);
            }
        }

        internal void GetConsignment(string consignmentReference)
        {
            var consignmentService = new ConsignmentService(_apiKey, _endpoints, _logger);

            var consignment = consignmentService.GetConsignment(consignmentReference);

            Console.WriteLine($"Reference: {consignment.Reference}");
            Console.WriteLine($"Created: {consignment.DateCreated:dd/MM/yyyy HH:mm}");
            foreach (var address in consignment.Addresses)
            {
                Console.WriteLine($"Address type: {address.AddressType}");
                Console.WriteLine($"Address line 1: {address.AddressLine1}");
                Console.WriteLine($"Postcode: {address.Postcode}");
            }
            foreach (var package in consignment.Packages)
            {
                Console.WriteLine($"Package reference: {package.Reference}");
                Console.WriteLine($"Package weight: {package.Weight.Kg} (kg)");
            }

            Console.WriteLine($"Consignment current state: {consignment.ConsignmentState}");
        }

        private static CreateConsignmentRequest SampleCreateConsignmentRequest()
        {
            return new CreateConsignmentRequest()
            {
                Addresses = new List<Address>()
                {
                    new Address()
                    {
                        AddressType = ConsignmentAddressType.Origin,
                        ShippingLocationReference = "ElectioDemoFourWarehouse"
                    },
                    new Address()
                    {
                        AddressType = ConsignmentAddressType.Destination,
                        AddressLine1 = "123 Some Street",
                        AddressLine2 = "Testsby",
                        AddressLine3 = "Testington",
                        Town = "Testville",
                        Region = "Testishire",
                        Postcode = "M1 5WG",
                        Country = new Country()
                        {
                            IsoCode = new IsoCode()
                            {
                                TwoLetterCode = "GB"
                            }
                        },
                        Contact = new Contact()
                        {
                            Title = "Mr",
                            FirstName = "Peter",
                            LastName = "McPetersson",
                            Email = "peter.mcpetersson@mcpeterssonlimited.co.uk",
                            Telephone = "0161 123441"
                        }
                    }
                },
                Packages = new List<Package>()
                {
                    new Package()
                    {
                        PackageReferenceProvidedByCustomer = "MyPackageRef001",
                        Description = "Socks",
                        Dimensions = new Dimensions(10M, 10M, 10M),
                        Weight = new Weight()
                        {
                            Kg = 0.5M
                        },
                        Value = new Money()
                        {
                            Amount = 5.99M,
                            Currency = new Currency()
                            {
                                IsoCode = "GBP"
                            }
                        }
                    }
                },
                ConsignmentReferenceProvidedByCustomer = "Your own reference"
            };
        }
    }
}
