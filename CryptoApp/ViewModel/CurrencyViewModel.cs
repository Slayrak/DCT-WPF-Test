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
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CryptoApp.ViewModel
{
    public class CurrencyViewModel : ViewModelBase
    {
        private readonly ICurrencyService _currencyService;

        private readonly DelegateCommand _loadCommand;
        private readonly Currency _currency;

        public ObservableCollection<OhlcPoint> Points { get; set; } = new ObservableCollection<OhlcPoint>();
        public ObservableCollection<DateTime> Dates { get; set; } = new ObservableCollection<DateTime>();

        public CurrencyViewModel(ICurrencyService currencyService, Currency currency, NavigationStore navigationStore, Func<TopCurrenciesViewModel> topCurrenciesViewModel)
        {
            _currency = currency;

            CurrencyName = _currency.CurrencyName;
            CurrencyCode = _currency.CurrencyCode;
            Price = _currency.Price;
            Volume = _currency.Volume;
            PopularityRating = _currency.PopularityRating;

            _currencyService = currencyService;
            OpenTop10 = new NavigateCommand(navigationStore, topCurrenciesViewModel);
            GetCurrencyInfo();
            GetCandles();

        }

        private string[] _labels;
        public string[] Labels
        {
            get { return _labels; }
            set
            {
                _labels = value;
                OnPropertyChanged(nameof(Labels));
            }
        }


        private SeriesCollection seriesCollection;
        public SeriesCollection SeriesCollection
        {
            get
            {
                return seriesCollection;
            }
            set
            {
                seriesCollection = value;
                OnPropertyChanged(nameof(this.SeriesCollection));
            }
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
            var _currency = await _currencyService.GetCurrencyByName(this._currency.CurrencyName);

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

        private async void GetCandles()
        {
            Points.Clear();
            
            var result =  await _currencyService.GetCandles(currencyCode);
            result.ToList().ForEach(x =>
            {
                Points.Add(new OhlcPoint(x.Open, x.High, x.Low, x.Close));
                Dates.Add(x.Date);
            });


            SeriesCollection = new SeriesCollection
            {
                new CandleSeries
                {
                    Values = Points.AsChartValues(),
                    Title = "Data for 1 day",
                }
            };

            Labels = new string[Dates.Count + 1];
            Labels[0] = DateTime.Now.ToString("dd MMM");
            for (int i = 0; i < Dates.Count; i++)
            {
                Labels[i + 1] = Dates[i].ToString();
            }
        }
    }
}
