using Microsoft.Extensions.DependencyInjection;
using QrCodeGenerator.Core.Generators;

namespace QrCodeGenerator.Core;

public class CoreRegistrator
{
    public static void RegisterCoreServices(IServiceCollection services)
    {
        services.AddTransient<PureGenerator>();
        services.AddTransient<CzechPaymentGenerator>();
    }
}
