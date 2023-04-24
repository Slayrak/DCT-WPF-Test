using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.Domain.Models
{
    public class Currency
    {
        public string CurrencyName { get; set; } = "";
        public string CurrencyCode { get; set; } = "";
        public string Price { get; set; }
        public string Volume { get; set; }
        public string PriceChangeInPercents { get; set; }
        public int PopularityRating { get; set; }

        public IList<string> AvailableMarketsNames { get; set; } = new List<string>();

    }
}
