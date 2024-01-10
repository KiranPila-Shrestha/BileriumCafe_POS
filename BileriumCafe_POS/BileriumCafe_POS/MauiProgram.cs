using BileriumCafe_POS.Data;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using BileriumCafe_POS.Services;

namespace BileriumCafe_POS
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

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif
            builder.Services.AddMudServices();
            builder.Services.AddScoped<UserServices>();
            builder.Services.AddSingleton<WeatherForecastService>();
            builder.Services.AddSingleton<CoffeeService>();
            builder.Services.AddSingleton<AddItemService>();
            builder.Services.AddSingleton<OrderItemService>();
            builder.Services.AddSingleton<OrderService>();
            builder.Services.AddSingleton<CustomerService>();
            builder.Services.AddSingleton<ReportService>();

            return builder.Build();
        }
    }
}