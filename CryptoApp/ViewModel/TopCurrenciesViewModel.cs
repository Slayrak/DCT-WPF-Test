using CryptoApp.Commands;
using CryptoApp.Domain.Interfaces;
using CryptoApp.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows;
using System.Windows.Input;

namespace CryptoApp.ViewModel
{
    public class TopCurrenciesViewModel : ViewModelBase
    {
        private readonly ICurrencyService _currencyService;
        public ObservableCollection<CurrencyListInstanceViewModel> CurrencyViewModels { get; set; } = new ObservableCollection<CurrencyListInstanceViewModel>();

        private int _topN = 10;
        public int TopN 
        { 
            get { return _topN; } 
            set 
            {
                _topN = value;
                OnPropertyChanged(nameof(TopN));
            } 
        }

        private string _currencyName;
        public string CurrencyName 
        { 
            get { return _currencyName; } 
            set 
            {
                _currencyName = value;
                OnPropertyChanged(nameof(CurrencyName));
            } 
        }

        public ICommand SearchCommand { get; }
        public ICommand GetCurrencyData { get; }
        public ICommand MakeExchange { get; }
        public ICommand OpenCurrencyInfo { get; }
        public ICommand OpenCurrencyConverter { get; }
        public ICommand GetTopCurrenciesCommand { get; }

        public TopCurrenciesViewModel(ICurrencyService currencyService, NavigationStore navigationStore, Func<string, CurrencyViewModel> currenciesViewModel, Func<ConvertCurrenciesViewModel> convertViewModel)
        {
            _currencyService = currencyService;
            GetTop10Currencies();

            SearchCommand = new SearchCommand(this, _currencyService);
            OpenCurrencyInfo = new NavigateCommand(navigationStore, currenciesViewModel);
            OpenCurrencyConverter = new NavigateCommand(navigationStore, convertViewModel);
            GetTopCurrenciesCommand = new TopCurrenciesCommand(this, _currencyService);
        }

        private async void GetTop10Currencies()
        {
            CurrencyViewModels.Clear();

            var currencies = await _currencyService.GetTop10Currencies();

            currencies.ToList().ForEach(x =>
            {
                var record = new CurrencyListInstanceViewModel(x);
                CurrencyViewModels.Add(record);
            });
        }
    }
}
