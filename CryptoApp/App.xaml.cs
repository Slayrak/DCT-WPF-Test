using CryptoApp.Application.Services;
using CryptoApp.Domain.Interfaces;
using CryptoApp.Stores;
using CryptoApp.View;
using CryptoApp.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CryptoApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        private ServiceProvider serviceProvider;
        private readonly NavigationStore _navigationStore;

        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();

            _navigationStore = new NavigationStore();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddTransient<ICurrencyService, CurrencyService>();

            services.AddSingleton<MainWindow>();
            services.AddTransient<TopCurrenciesViewModel>();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            _navigationStore .CurrentViewModel = new TopCurrenciesViewModel(serviceProvider.GetRequiredService<ICurrencyService>());

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore, serviceProvider)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}