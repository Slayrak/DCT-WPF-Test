using CryptoApp.Commands;
using CryptoApp.Domain.Interfaces;
using CryptoApp.Domain.Models;
using CryptoApp.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CryptoApp.ViewModel
{
    public class ConvertCurrenciesViewModel : ViewModelBase
    {
        private readonly ICurrencyService _currencyService;

        public ObservableCollection<ConvertCurrenciesListElementInstance> ConvertCurrencyModels { get; set; } = new ObservableCollection<ConvertCurrenciesListElementInstance>();
        public ObservableCollection<string> CurrencyNames { get; set; } = new ObservableCollection<string>();
        public ConvertCurrenciesViewModel(ICurrencyService currencyService, NavigationStore navigationStore, Func<TopCurrenciesViewModel> topCurrenciesViewModel)
        {

            _currencyService = currencyService;

            GetAllCurrenciesName();

            ConvertCurrenciesCommand = new ConvertCommand(this, _currencyService);
            OpenTop10 = new NavigateCommand(navigationStore, topCurrenciesViewModel);
        }

        private string _selectedBaseItem = "ethereum";
        public string SelectedBaseItem
        {
            get { return _selectedBaseItem; }
            set
            {
                _selectedBaseItem = value;
                OnPropertyChanged(nameof(SelectedBaseItem));
            }
        }

        private string _selectedQuoteItem = "bitcoin";
        public string SelectedQuoteItem
        {
            get { return _selectedQuoteItem; }
            set
            {
                _selectedBaseItem = value;
                OnPropertyChanged(nameof(SelectedQuoteItem));
            }
        }

        public ICommand ConvertCurrenciesCommand { get; }
        public ICommand OpenTop10 { get; }

        private async void GetAllCurrenciesName()
        {
            CurrencyNames.Clear();

            var currencies = await _currencyService.GetAllCurrenciesName();

            currencies.ToList().ForEach(CurrencyNames.Add);
        }

    }
}
