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
        private readonly HttpClient _httpClient;

        public CurrencyService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<IList<Currency>> GetTop10Currencies() 
        {
            var response = await _httpClient.GetAsync("https://api.coincap.io/v2/assets");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
            }


            return null;
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
