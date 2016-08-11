using MPD.Electio.SDK.DataTypes.DeliveryOptions;

namespace MPD.Electio.SDK.Sample.MvcApplication.Models.DeliveryOptions
{
    public class DeliveryOptionsViewModel
    {
        public DeliveryOptionsRequest Request { get; set; }
        public int? DaysToGetOptionsFor { get; set; }
        public DeliveryOptionsResponse Response { get; set; }
    }
}