using CryptoApp.Commands;
using CryptoApp.Domain.Interfaces;
using CryptoApp.Domain.Models;
using CryptoApp.Stores;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CryptoApp.ViewModel
{
    public class CurrencyViewModel : ViewModelBase
    {
        public SeriesCollection SeriesCollection { get; set; }
        private readonly ICurrencyService _currencyService;

        private readonly string _currencyName;

        public CurrencyViewModel(ICurrencyService currencyService, string currencyName, NavigationStore navigationStore, Func<TopCurrenciesViewModel> topCurrenciesViewModel)
        {
            _currencyName = currencyName;
            _currencyService= currencyService;
            OpenTop10 = new NavigateCommand(navigationStore, topCurrenciesViewModel);
            GetCurrencyInfo();

            SeriesCollection = new SeriesCollection();
            var myData = new List<OhlcPoint>
            {
            new OhlcPoint(100, 110, 90, 105),
            new OhlcPoint(105, 120, 100, 110),
            new OhlcPoint(110, 115, 100, 110),
            new OhlcPoint(110, 125, 105, 120),
            new OhlcPoint(120, 125, 110, 115),
            new OhlcPoint(115, 120, 105, 110),
            new OhlcPoint(110, 115, 105, 110),
            new OhlcPoint(110, 120, 100, 115),
            new OhlcPoint(115, 125, 110, 115),
            new OhlcPoint(120, 125, 115, 120),
            new OhlcPoint(120, 130, 115, 125),
            new OhlcPoint(125, 130, 120, 125),
            new OhlcPoint(125, 130, 120, 125),
            new OhlcPoint(125, 130, 120, 125),
            new OhlcPoint(125, 130, 120, 125),
            new OhlcPoint(125, 130, 120, 125),
            new OhlcPoint(125, 130, 120, 125),
            new OhlcPoint(125, 130, 120, 125),
            new OhlcPoint(125, 130, 120, 125),
            new OhlcPoint(125, 130, 120, 125)
            };
            SeriesCollection.Add(new CandleSeries
            {
                Values = myData.AsChartValues(),
                Title = "My Data"
            });

        }

        private string currencyName;
        public string CurrencyName {
            get { return currencyName; }
            set
            {
                currencyName = value;
                OnPropertyChanged(nameof(CurrencyName));
            }
        }

        private string currencyCode;
        public string CurrencyCode {
            get { return currencyCode; }
            set
            {
                currencyCode = value;
                OnPropertyChanged(nameof(CurrencyCode));
            }
        }

        private string price;
        public string Price {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        private string volume;
        public string Volume {
            get { return volume; }
            set
            {
                volume = value;
                OnPropertyChanged(nameof(Volume));
            }
        }

        private string priceChange;
        public string PriceChange {
            get { return priceChange; }
            set
            {
                priceChange = value;
                OnPropertyChanged(nameof(PriceChange));
            }
        }

        private int popularityRating;
        public int PopularityRating
        {
            get { return popularityRating; }
            set
            {
                popularityRating = value;
                OnPropertyChanged(nameof(PopularityRating));
            }
        }

        public IList<MarketCurrencyViewModel> AvailableMarketsNames;

        public ICommand OpenTop10 { get; }

        private async void GetCurrencyInfo()
        {
            var _currency = await _currencyService.GetCurrencyByName(_currencyName);

            CurrencyName = _currency.CurrencyName;
            CurrencyCode = _currency.CurrencyCode;
            Price = _currency.Price;
            Volume = _currency.Volume;
            PriceChange = _currency.PriceChangeInPercents;
            PopularityRating= _currency.PopularityRating;

            double number;

            Double.TryParse(Price, CultureInfo.InvariantCulture, out number);

            var check = number;
        }
    }
}
