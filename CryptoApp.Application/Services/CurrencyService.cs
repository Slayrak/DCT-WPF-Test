using CryptoApp.Domain.Interfaces;
using CryptoApp.Domain.Models;
using Newtonsoft.Json.Linq;
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
            var result = new List<Currency>();

            var response = await _httpClient.GetAsync("https://api.coincap.io/v2/assets");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var records = JObject.Parse(content)["data"];

                foreach(var elem in records)
                {
                    var record = new Currency();
                    record.CurrencyName = elem["name"].ToString();
                    record.PopularityRating = Convert.ToInt32(elem["rank"].ToString());
                    result.Add(record);
                }
            }

            result = result.OrderBy(x => x.PopularityRating).Take(10).ToList();

            return result;
        }
        public Task<IList<Currency>> GetTopNCurrenciesOnMarket(int N, string market)
        {
            throw new NotImplementedException();
        }

        public async Task<Currency> GetCurrencyByName(string currencyName)
        {
            var result = new Currency();

            var response = await _httpClient.GetAsync("https://api.coincap.io/v2/assets");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var records = JObject.Parse(content)["data"];

                foreach (var elem in records)
                {
                    if(currencyName == elem["name"].ToString())
                    {
                        result.CurrencyName = elem["name"].ToString();
                        result.PopularityRating = Convert.ToInt32(elem["rank"].ToString());
                        break;
                    }
                }
            }

            return result;

        }
    }
}
