using CryptoApp.Domain.Models;
using LiveCharts.Defaults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.Domain.Interfaces
{
    public interface ICurrencyService
    {
        Task<IList<Currency>> GetTop10Currencies();
        Task<IList<Market>> GetMarkets(string currencyCode);
        Task<List<PointStorage>> GetCandles(string coinName);
        Task<Currency> GetCurrencyByName(string currencyName); 
    }
}
