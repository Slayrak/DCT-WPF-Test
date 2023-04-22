using CryptoApp.Commands;
using CryptoApp.Domain.Models;
using CryptoApp.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CryptoApp.ViewModel
{
    public class CurrencyViewModel : ViewModelBase
    {
        private readonly Currency _currency;

        public CurrencyViewModel(Currency currency, NavigationStore navigationStore, Func<TopCurrenciesViewModel> topCurrenciesViewModel)
        {
            _currency = currency;

            OpenTop10 = new NavigateCommand(navigationStore, topCurrenciesViewModel);
        }

        public string CurrencyName => _currency.CurrencyName;
        public string CurrencyCode => _currency.CurrencyCode;
        public double Price => _currency.Price;
        public double Volume => _currency.Volume;
        public double PriceChange => _currency.PriceChange;
        public int PopularityRating => _currency.PopularityRating;

        public IEnumerable<string> AvailableMarketsNames => _currency.AvailableMarketsNames;

        public ICommand OpenTop10 { get; }
    }
}
