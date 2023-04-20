using CryptoApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.ViewModel
{
    public class CurrencyViewModel : ViewModelBase
    {
        private readonly Currency _currency;

        public CurrencyViewModel(Currency currency)
        {
            _currency = currency;
        }

        public string CurrencyName => _currency.CurrencyName;
        public string CurrencyCode => _currency.CurrencyCode;
        public double Price => _currency.Price;
        public double Volume => _currency.Volume;
        public double PriceChange => _currency.PriceChange;
        public int PopularityRating { get; set; }

        public IEnumerable<string> AvailableMarketsNames => _currency.AvailableMarketsNames;

    }
}
