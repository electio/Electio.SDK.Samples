using System.Text.RegularExpressions;

namespace MPD.Electio.SDK.Sample.MvcApplication.Extensions
{
    public static class StringExtensions
    {
        private const string CONSIGNMENT_REF_PATTERN = "EC-[0-9A-Z]{3}-[0-9A-Z]{3}-[0-9A-Z]{3}$";

        /// <summary>
        /// Tries the extract consignment reference from a string
        /// <remarks>
        /// Consignment references are assigned in the format EC-XXX-XXX-XXX where X is an alphanumeric character.
        /// This methods uses a regular expression to extract a matching consignment reference.
        /// </remarks>
        /// </summary>
        /// <param name="input">The input string to extract a reference from.</param>
        /// <returns>
        /// Either <value>EC-XXX-XXX-XXX</value> for a matched consignment reference,
        /// otherwise <value><c>string.Empty</c></value></returns>
        public static string TryExtractConsignmentReference(this string input)
        {
            var regex = new Regex(CONSIGNMENT_REF_PATTERN);
            var reference = regex.Match(input);
            return reference.Success
                ? reference.Groups[0].Value
                : string.Empty;
        }
    }
}