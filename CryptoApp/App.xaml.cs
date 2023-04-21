using CryptoApp.Application.Services;
using CryptoApp.Domain.Interfaces;
using CryptoApp.Domain.Models;
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
        private Currency _currency;
        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();

            _navigationStore = new NavigationStore();

            _currency= new Currency();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddTransient<ICurrencyService, CurrencyService>();

            services.AddSingleton<MainWindow>();
            services.AddTransient<TopCurrenciesViewModel>();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            _navigationStore.CurrentViewModel = CreateTopCurrenciesViewModel();

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore, serviceProvider)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }

        private TopCurrenciesViewModel CreateTopCurrenciesViewModel()
        {
            return new TopCurrenciesViewModel(serviceProvider.GetRequiredService<ICurrencyService>(), _navigationStore, CreateCurrencyViewModel, CreateConvertCurrenciesViewModel);
        }
        private CurrencyViewModel CreateCurrencyViewModel()
        {
            return new CurrencyViewModel(_currency, _navigationStore, CreateTopCurrenciesViewModel);
        }

        private ConvertCurrenciesViewModel CreateConvertCurrenciesViewModel()
        {
            return new ConvertCurrenciesViewModel();
        }
    }
}