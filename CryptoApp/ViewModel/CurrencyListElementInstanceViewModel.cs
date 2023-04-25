using CryptoApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.ViewModel
{
    public class CurrencyListElementInstanceViewModel
    {
        private readonly Currency _currency;
        public CurrencyListElementInstanceViewModel(Currency currency)
        {
            _currency = currency;
        }

        public string CurrencyName => _currency.CurrencyName;
        public int PopularityRating => _currency.PopularityRating;


    }
}
