using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.Domain.Models
{
    public class Curency
    {
        public string CurrencyName { get; set; } = "";
        public string CurrencyCode { get; set; } = "";
        public double Price { get; set; }
        public double Volume { get; set; }
        public double PriceChange { get; set; }

        public List<string> AvailableMarketsNames { get; set; } = new List<string>();

    }
}
