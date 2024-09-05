using Avalonia.MusicStore.ViewModels;
using Avalonia.MusicStore.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Avalonia.MusicStore.Services
{
    internal static class RegistServices
    {
        internal static IServiceCollection RegistAllServices(this IServiceCollection services)
        {
            services.RegistViewSerivces();
            return services;
        }

        internal static IServiceCollection RegistViewSerivces(this IServiceCollection services)
        {
            #region Views
            services.AddSingleton<MainView>();
            services.AddSingleton<MainWindow>();

            services.AddTransient<MusicStoreView>();
            services.AddTransient<MusicStoreWindow>();
            #endregion

            #region ViewModels
            services.AddSingleton<MainViewModel>();
            services.AddTransient<MusicStoreViewModel>();
            #endregion

            return services;
        }
    }
}
