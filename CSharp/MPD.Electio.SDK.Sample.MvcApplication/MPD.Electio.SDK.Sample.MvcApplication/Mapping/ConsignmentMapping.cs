using System.Collections.Generic;
using MPD.Electio.SDK.DataTypes.Common;
using MPD.Electio.SDK.DataTypes.Consignments;
using MPD.Electio.SDK.DataTypes.Consignments.Enums;
using MPD.Electio.SDK.Sample.MvcApplication.Models.Consignments;
using Address = MPD.Electio.SDK.DataTypes.Consignments.Address;

namespace MPD.Electio.SDK.Sample.MvcApplication.Mapping
{
    /// <summary>
    /// This demonstrates a sample technique for mapping your own
    /// internal viewmodels to Electio Datatypes.
    /// <remarks>
    /// This particular technique is implemented in as simple a way as possible.
    /// More complex examples may benefit from a more flexible and maintainable
    /// approach, such as using a third-party library e.g. AutoMapper <see href="http://automapper.org/"/>
    /// </remarks>
    /// </summary>
    public static class ConsignmentMapping
    {
        public static CreateConsignmentRequest MapCreateConsignmentRequest(CreateConsignmentRequestViewModel viewModel)
        {
            return new CreateConsignmentRequest()
            {
                Addresses = new List<Address>()
                {
                    new Address()
                    {
                        AddressType = ConsignmentAddressType.Origin,
                        ShippingLocationReference = viewModel.OriginShippingLocationReference
                    },
                    new Address()
                    {
                        AddressType = ConsignmentAddressType.Destination,
                        Contact = new Contact()
                        {
                            Title = viewModel.DestinationContactTitle,
                            FirstName = viewModel.DestinationContactForename,
                            LastName = viewModel.DestinationContactSurname,
                            Email = viewModel.DestinationContactEmail,
                            Telephone = viewModel.DestinationContactTelephone
                        },
                        AddressLine1 = viewModel.DestinationAddressLine1,
                        AddressLine2 = viewModel.DestinationAddressLine2,
                        AddressLine3 = viewModel.DestinationAddressLine3,
                        Town = viewModel.DestinationAddressTown,
                        Region = viewModel.DestinationAddressRegion,
                        Postcode = viewModel.DestinationAddressPostcode,
                        Country = new Country()
                        {
                            IsoCode = new IsoCode()
                            {
                                TwoLetterCode = viewModel.DestinationAddressCountryIsoCode
                            }
                        }
                    }
                },
                Packages = new List<Package>()
                {
                    new Package()
                    {
                        Description = viewModel.PackageDescription,
                        Dimensions = new Dimensions()
                        {
                            Length = viewModel.PackageLength,
                            Height = viewModel.PackageHeight,
                            Width = viewModel.PackageWidth
                        },
                        Weight = new Weight()
                        {
                            Kg = viewModel.PackageWeight
                        },
                        Value = new Money()
                        {
                            Amount = viewModel.PackageValue,
                            Currency = new Currency()
                            {
                                IsoCode = viewModel.Currency
                            }
                        }
                    }
                },
                ConsignmentReferenceProvidedByCustomer = viewModel.ReferenceProvidedByCustomer
            };
        }
    }
}