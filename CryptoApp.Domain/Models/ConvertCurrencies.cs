using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.Domain.Models
{
    public class ConvertCurrencies
    {
        public string MarketId { get; set; }
        public string BaseId { get; set; }
        public string QuoteId { get; set; }
        public double PriceQuote { get; set; }
        public double PriceUSD { get; set; }
    }
}
