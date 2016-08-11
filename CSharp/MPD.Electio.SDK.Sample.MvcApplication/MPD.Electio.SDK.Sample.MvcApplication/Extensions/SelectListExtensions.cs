using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MPD.Electio.SDK.DataTypes.Common;
using MPD.Electio.SDK.DataTypes.Profile;

namespace MPD.Electio.SDK.Sample.MvcApplication.Extensions
{
    public static class SelectListExtensions
    {
        /// <summary>
        /// Helper function to convert a list of <see cref="ShippingLocation"/> into a SelectList
        /// </summary>
        /// <param name="shippingLocations">The shipping locations.</param>
        public static IEnumerable<SelectListItem> ToSelectList(this IEnumerable<ShippingLocation> shippingLocations)
        {
            return shippingLocations.Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Reference
                });

        }

        public static IEnumerable<SelectListItem> ToSelectList(this IEnumerable<Country> countries)
        {
            return countries.Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.IsoCode.TwoLetterCode
                });
        }

        public static IEnumerable<SelectListItem> ToSelectList(this IEnumerable<Currency> currencies)
        {
            return currencies.Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.IsoCode
                });
        }

        public static IEnumerable<SelectListItem> ToSelectList(this IEnumerable<string> stringItems)
        {
            return stringItems.Select(x => new SelectListItem()
            {
                Text = x,
                Value = x
            });
        }
    }
}