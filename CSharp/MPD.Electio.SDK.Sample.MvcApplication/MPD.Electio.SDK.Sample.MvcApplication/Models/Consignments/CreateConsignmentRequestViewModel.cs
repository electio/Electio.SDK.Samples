using System.ComponentModel.DataAnnotations;
using MPD.Electio.SDK.DataTypes.Consignments;

namespace MPD.Electio.SDK.Sample.MvcApplication.Models.Consignments
{
    /// <summary>
    /// This is a sample of a simplified and 'flattened' representation
    /// of a <see cref="CreateConsignmentRequest"/>
    /// <remarks>
    /// This technique allows you to control your own internal models,
    /// including validation. Your own view models are passed back and forth
    /// to your controllers, before being mapped to the Electio Datatype
    /// to be sent to the Electio API.
    /// 
    /// This example uses DataAnnotations to provide validation, but you
    /// can use any technique, such as FluentValidation to provide
    /// your own validation rules.
    /// </remarks>
    /// </summary>
    public class CreateConsignmentRequestViewModel
    {
        public string ReferenceProvidedByCustomer { get; set; }

        [Required(ErrorMessage = "Please add a package description")]
        public string PackageDescription { get; set; }

        [Required(ErrorMessage = "Package length is required")]
        [Range(0, 1000, ErrorMessage = "Please choose a length between 0 and 1000cm")]
        public decimal PackageLength { get; set; }

        [Required(ErrorMessage = "Package height is required")]
        [Range(0, 1000, ErrorMessage = "Please choose a height between 0 and 1000cm")]
        public decimal PackageHeight { get; set; }

        [Required(ErrorMessage = "Package width is required")]
        [Range(0, 1000, ErrorMessage = "Please choose a width between 0 and 1000cm")]
        public decimal PackageWidth { get; set; }

        [Required(ErrorMessage = "Package weight is required")]
        [Range(0, 1000, ErrorMessage = "Please choose a weight between 0 and 1000kg")]
        public decimal PackageWeight { get; set; }

        [Required(ErrorMessage = "Package value is required")]
        [Range(0, 1000, ErrorMessage = "Please choose a length between 0 and 1000cm")]
        public decimal PackageValue { get; set; }

        [Required(ErrorMessage = "Please choose a currency")]
        public string Currency { get; set; }

        [Required(ErrorMessage = "Please choose an origin shipping location")]
        public string OriginShippingLocationReference { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string DestinationContactTitle { get; set; }

        [Required(ErrorMessage = "Forename is required")]
        public string DestinationContactForename { get; set; }

        [Required(ErrorMessage = "Surname is required")]
        public string DestinationContactSurname { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string DestinationContactEmail { get; set; }

        [Required(ErrorMessage = "Telephone is required")]
        public string DestinationContactTelephone { get; set; }

        [Required(ErrorMessage = "Address Line 1 is required")]
        public string DestinationAddressLine1 { get; set; }
        public string DestinationAddressLine2 { get; set; }
        public string DestinationAddressLine3 { get; set; }

        [Required(ErrorMessage = "Town is required")]
        public string DestinationAddressTown { get; set; }

        [Required(ErrorMessage = "Region/county is required")]
        public string DestinationAddressRegion { get; set; }

        [Required(ErrorMessage = "Postcode is required")]
        public string DestinationAddressPostcode { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public string DestinationAddressCountryIsoCode { get; set; }
    }
}