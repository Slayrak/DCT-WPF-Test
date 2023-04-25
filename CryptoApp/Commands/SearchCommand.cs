using CryptoApp.Domain.Interfaces;
using CryptoApp.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.Commands
{
    public class SearchCommand : CommandBase
    {
        private readonly TopCurrenciesViewModel _topCurrenciesViewModel;
        private readonly ICurrencyService _currencyService;
        public SearchCommand(TopCurrenciesViewModel topCurrenciesViewModel, ICurrencyService currencyService)
        {
            _topCurrenciesViewModel = topCurrenciesViewModel;
            _topCurrenciesViewModel.PropertyChanged += OnViewPropertyChanged;

            _currencyService = currencyService;
        }

        private void OnViewPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(TopCurrenciesViewModel.CurrencyName))
            {
                OnCanExecuteChanged();
            }
        }

        public async override void Execute(object? parameter)
        {
            if(!string.IsNullOrEmpty(_topCurrenciesViewModel.CurrencyName))
            {
                var result = await _currencyService.GetCurrencyByName(_topCurrenciesViewModel.CurrencyName);
                _topCurrenciesViewModel.CurrencyViewModels.Clear();

                var record = new CurrencyListElementInstanceViewModel(result);
                _topCurrenciesViewModel.CurrencyViewModels.Add(record);

            }
            else
            {
                _topCurrenciesViewModel.CurrencyViewModels.Clear();

                var currencies = await _currencyService.GetTop10Currencies();

                currencies.ToList().ForEach(x =>
                {
                    var record = new CurrencyListElementInstanceViewModel(x);
                    _topCurrenciesViewModel.CurrencyViewModels.Add(record);
                });
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return base.CanExecute(parameter);
        }
    }
}
