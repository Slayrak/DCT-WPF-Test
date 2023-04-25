using CryptoApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.ViewModel
{
    public class MarketCurrencyViewModel
    {
        private readonly Market _market;
        public MarketCurrencyViewModel(Market market)
        {
            _market = market;
        }
        public double Price => _market.Price;
        public string MarketName => _market.MarketName;
    }
}
