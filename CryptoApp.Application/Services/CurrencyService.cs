using CryptoApp.Domain.Interfaces;
using CryptoApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.Application.Services
{
    public class CurrencyService : ICurrencyService
    {
        public Task<IList<Currency>> GetTop10Currencies() 
        {
            throw new NotImplementedException();
        }
        public Task<IList<Currency>> GetTopNCurrenciesOnMarket(int N, string market)
        {
            throw new NotImplementedException();
        }

        public Task<Currency> GetCurrencyByName(string currencyName)
        {
            throw new NotImplementedException();
        }
    }
}
