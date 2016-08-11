using System.Collections.Generic;
using System.Web.Mvc;
using MPD.Electio.SDK.DataTypes.Consignments;

namespace MPD.Electio.SDK.Sample.MvcApplication.Models.Consignments
{
    public class CreateConsignmentViewModel
    {
        public CreateConsignmentRequestViewModel CreateConsignmentRequest { get; set; }

        /// <summary>
        /// The available shipping locations to populate the create consignment view
        /// </summary>
        public IEnumerable<SelectListItem> ShippingLocations { get; set; }

        /// <summary>
        /// The available currencies to populate the create consignment view
        /// </summary>
        public IEnumerable<SelectListItem> Currencies { get; set; }

        /// <summary>
        /// The available countries to populate the create consignment view
        /// </summary>
        public IEnumerable<SelectListItem> Countries { get; set; }

        /// <summary>
        /// Gets or sets the titles e.g. Mr, Mrs, Dr
        /// </summary>
        public IEnumerable<SelectListItem> Titles { get; set; }

        /// <summary>
        /// Gets or sets the created consignment link.
        /// </summary>
        public string CreatedConsignmentLink { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CreateConsignmentViewModel"/> has been created successfully.
        /// </summary>
        /// <value>
        ///   <c>true</c> if created; otherwise, <c>false</c>.
        /// </value>
        public bool Created { get; set; }
    }
}