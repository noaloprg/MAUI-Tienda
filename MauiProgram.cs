using Microsoft.Extensions.Logging;

namespace Tienda
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");

                    fonts.AddFont("Aerosoldier.otf", "Aerosoldier");
                    fonts.AddFont("Network.ttf", "Network");
                    fonts.AddFont("jumpis.otf", "jumpis");
                    fonts.AddFont("aerosoldier_symbol.otf", "aerosoldier_symbol");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
