using CryptoApp.Commands;
using CryptoApp.Domain.Models;
using CryptoApp.Stores;
using LiveCharts;
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
        public SeriesCollection SeriesCollection { get; set; }

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

        public IList<MarketCurrencyViewModel> AvailableMarketsNames;

        public ICommand OpenTop10 { get; }
    }
}
