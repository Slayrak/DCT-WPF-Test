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
    public class ConvertCommand : CommandBase
    {
        private readonly ConvertCurrenciesViewModel _convertCurrenciesViewModel;
        private readonly ICurrencyService _currencyService;

        public ConvertCommand(ConvertCurrenciesViewModel convertCurrenciesViewModel, ICurrencyService currencyService)
        {
            _convertCurrenciesViewModel = convertCurrenciesViewModel;

            _currencyService = currencyService;
        }

        public async override void Execute(object? parameter)
        {
            var result = await _currencyService.ConvertCurrencies(_convertCurrenciesViewModel.SelectedBaseItem, _convertCurrenciesViewModel.SelectedQuoteItem);
            _convertCurrenciesViewModel.ConvertCurrencyModels.Clear();

            result.ToList().ForEach(x =>
            {
                var record = new ConvertCurrenciesListElementInstance(x);
                _convertCurrenciesViewModel.ConvertCurrencyModels.Add(record);
            });
        }
    }
}
