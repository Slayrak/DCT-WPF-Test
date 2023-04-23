using CryptoApp.Domain.Interfaces;
using CryptoApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.Commands
{
    public class TopCurrenciesCommand : CommandBase
    {
        private readonly TopCurrenciesViewModel _topCurrenciesViewModel;
        private readonly ICurrencyService _currencyService;
        public TopCurrenciesCommand(TopCurrenciesViewModel topCurrenciesViewModel, ICurrencyService currencyService)
        {
            _topCurrenciesViewModel = topCurrenciesViewModel;

            _currencyService = currencyService;
        }
        public async override void Execute(object? parameter)
        {
            if(parameter == "GetTopNByMarket")
            {

            } 
            else
            {
                _topCurrenciesViewModel.CurrencyViewModels.Clear();

                var currencies = await _currencyService.GetTop10Currencies();

                currencies.ToList().ForEach(x =>
                {
                    var record = new CurrencyListInstanceViewModel(x);
                    _topCurrenciesViewModel.CurrencyViewModels.Add(record);
                });
            }


        }
    }
}
