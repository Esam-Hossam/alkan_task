using alkan_task.ViewModels;
using alkan_task.Views;
using Microsoft.Extensions.Logging;

namespace alkan_task
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            //builder.Services.AddTransient<HomePage>();
            //builder.Services.AddTransient<MainViewModel>();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
            
#endif

            return builder.Build();
        }
    }
}