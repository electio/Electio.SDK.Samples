using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using MPD.Electio.SDK.Endpoints;
using MPD.Electio.SDK.Interfaces;
using MPD.Electio.SDK.Interfaces.Services;
using MPD.Electio.SDK.Services;

namespace MPD.Electio.SDK.Sample.MvcApplication.IoC.Installers
{
    /// <summary>
    /// Register the required dependencies for the Electio SDK
    /// </summary>
    /// <seealso cref="Castle.MicroKernel.Registration.IWindsorInstaller" />
    public class ElectioSdkInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            const string API_KEY_PARAM = "apiKey";
            const string API_KEY_SETTING = "ElectioApiKey";

            container.Register(
                //Many of the Electio services depend upon an ILogger.
                //This allows you to plug your own logger implementation in and capture anything logged by the SDK
                Component.For<ILogger>().ImplementedBy<SdkReferenceLogger>(),
                
                //The IEndpoints interface tells the SDK where to find the various API HTTPS endpoints it needs
                //Production.Instance is an implementation of IEndpoints which has pointers to the Electio production/live API
                Component.For<IEndpoints>().Instance(Production.Instance).LifestyleSingleton(),

                //The consignment service handles operations relating to consignments, such as creating a new consignment
                Component.For<IConsignmentService>()
                    .ImplementedBy<ConsignmentService>()
                    .DependsOn(Dependency.OnAppSettingsValue(API_KEY_PARAM, API_KEY_SETTING)),

                //The shipping location service allows you to creat, read, update and delete shipping locations
                Component.For<IShippingLocationsService>()
                    .ImplementedBy<ShippingLocationsServiceService>()
                    .DependsOn(Dependency.OnAppSettingsValue(API_KEY_PARAM, API_KEY_SETTING)),

                //The static data service allows you to query the Electio API for fixed data such as currencies and titles
                Component.For<IStaticDataService>()
                    .ImplementedBy<StaticDataService>()
                    .DependsOn(Dependency.OnAppSettingsValue(API_KEY_PARAM, API_KEY_SETTING)),

                Component.For<IConsignmentAllocationService>()
                    .ImplementedBy<ConsignmentAllocationService>()
                    .DependsOn(Dependency.OnAppSettingsValue(API_KEY_PARAM, API_KEY_SETTING)),

                Component.For<IQuoteService>()
                    .ImplementedBy<QuoteService>()
                    .DependsOn(Dependency.OnAppSettingsValue(API_KEY_PARAM, API_KEY_SETTING)),

                Component.For<ILabelService>()
                    .ImplementedBy<LabelService>()
                    .DependsOn(Dependency.OnAppSettingsValue(API_KEY_PARAM, API_KEY_SETTING)),

                Component.For<ITrackingService>()
                    .ImplementedBy<TrackingService>()
                    .DependsOn(Dependency.OnAppSettingsValue(API_KEY_PARAM, API_KEY_SETTING)),

                Component.For<IDeliveryOptionsService>()
                    .ImplementedBy<DeliveryOptionsService>()
                    .DependsOn(Dependency.OnAppSettingsValue(API_KEY_PARAM, API_KEY_SETTING))
                
            );
        }
    }
}