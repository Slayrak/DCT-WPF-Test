using CryptoApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.ViewModel
{
    public class ConvertCurrenciesListElementInstance
    {
        private readonly ConvertCurrencies _convertCurrencies;

        public ConvertCurrenciesListElementInstance(ConvertCurrencies convertCurrencies)
        {
            _convertCurrencies = convertCurrencies;
        }
        public string MarketId => _convertCurrencies.MarketId;
        public string BaseId => _convertCurrencies.BaseId;
        public string QuoteId => _convertCurrencies.QuoteId;
        public double PriceQuote => _convertCurrencies.PriceQuote;
        public double PriceUSD => _convertCurrencies.PriceUSD;
    }
}
