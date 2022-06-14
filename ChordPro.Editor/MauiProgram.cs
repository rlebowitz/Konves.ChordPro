using ChordPro.Editor.Data;
using Microsoft.AspNetCore.Components.WebView.Maui;
using Radzen;

namespace ChordPro.Editor
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
                });

            builder.Services.AddMauiBlazorWebView();

            builder.Services.AddSingleton<WeatherForecastService>();
            builder.Services.AddScoped<DialogService>();
#if WINDOWS
            builder.Services.AddTransient<IFileSavePicker, Platforms.Windows.FileSavePicker>();
#endif
            return builder.Build();
        }
    }
}