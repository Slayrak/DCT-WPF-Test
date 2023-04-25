using CryptoApp.Domain.Interfaces;
using CryptoApp.Domain.Models;
using LiveCharts.Defaults;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json.Nodes;
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

        public async Task<IList<string>> GetAllCurrenciesName()
        {
            var result = new List<string>();

            var response = await _httpClient.GetAsync("https://api.coincap.io/v2/assets");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var records = JObject.Parse(content)["data"];

                foreach (var elem in records)
                {
                    var record = "";
                    record = elem["id"].ToString();
                    result.Add(record);
                }
            }

            return result;
        }

        public async Task<IList<Market>> GetMarkets(string currencyCode)
        {
            var result = new List<Market>();

            var response = await _httpClient.GetAsync($"https://api.coincap.io/v2/assets/{currencyCode}/markets");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var records = JObject.Parse(content)["data"];

                foreach (var elem in records)
                {
                    double price;

                    var record = new Market();
                    record.MarketName = elem["exchangeId"].ToString();
                    Double.TryParse(elem["priceUsd"].ToString(), CultureInfo.InvariantCulture, out price);
                    record.Price = price;
                    result.Add(record);
                }
            }

            result = result.OrderBy(x => x.Price).ToList();

            return result;
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
                        result.CurrencyCode = elem["id"].ToString();
                        result.Price = elem["priceUsd"].ToString();
                        result.Volume = elem["volumeUsd24Hr"].ToString();
                        result.PriceChangeInPercents = elem["changePercent24Hr"].ToString();

                        break;
                    }
                }
            }

            return result;

        }

        public async Task<List<PointStorage>> GetCandles(string coinName)
        {
            var myData = new List<PointStorage>();
            var response = await _httpClient.GetAsync($"https://api.coingecko.com/api/v3/coins/{coinName}/ohlc?vs_currency=usd&days=1");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var records = JArray.Parse(content);

                foreach(var elem in records)
                {
                    double open;
                    double high;
                    double low;
                    double close;

                    long time;
                    long.TryParse(elem[0].ToString(), out time);
                    time /= 1000;
                    DateTime result = DateTimeOffset.FromUnixTimeMilliseconds(time).DateTime;


                    Double.TryParse(elem[1].ToString(), out open);
                    Double.TryParse(elem[2].ToString(), out high);
                    Double.TryParse(elem[3].ToString(), out low);
                    Double.TryParse(elem[4].ToString(), out close);

                    myData.Add(new PointStorage
                    {
                        Date = result,
                        Open = open,
                        High = high,
                        Low = low,
                        Close = close,
                    });
                }
            }

            return myData;
        }

        public async Task<IList<ConvertCurrencies>> ConvertCurrencies(string baseName, string quoteName)
        {
            var result = new List<ConvertCurrencies>();

            var response = await _httpClient.GetAsync($"https://api.coincap.io/v2/markets?baseId={baseName}&quoteId={quoteName}");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var records = JObject.Parse(content)["data"];

                foreach (var elem in records)
                {
                    double price;
                    Double.TryParse(elem["priceUsd"].ToString(), CultureInfo.InvariantCulture, out price);
                    double quoteprice;
                    Double.TryParse(elem["priceQuote"].ToString(), CultureInfo.InvariantCulture, out quoteprice);


                    var record = new ConvertCurrencies();
                    record.MarketId = elem["exchangeId"].ToString();
                    record.BaseId = elem["baseId"].ToString();
                    record.QuoteId = elem["quoteId"].ToString();
                    record.PriceQuote = quoteprice;
                    record.PriceUSD = price;
                    result.Add(record);
                }
            }

            result = result.OrderBy(x => x.PriceQuote).ToList();

            return result;
        }
    }
}
