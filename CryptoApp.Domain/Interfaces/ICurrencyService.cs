﻿using CryptoApp.Domain.Models;
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
        Task<IList<Currency>> GetTopNCurrenciesOnMarket(int N, string market);

        Task<Currency> GetCurrencyByName(string currencyName); 
    }
}