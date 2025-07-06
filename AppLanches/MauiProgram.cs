using Microsoft.Extensions.Logging;
using AppLanches.Services;
using AppLanches.Validations;
using CommunityToolkit.Maui;


namespace AppLanches
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit() // ✅ THIS is critical
            .ConfigureFonts();


#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.Services.AddHttpClient();
            builder.Services.AddSingleton<ApiService>();
            builder.Services.AddSingleton<IValidator, Validator>();

            return builder.Build();
        }
    }
}
