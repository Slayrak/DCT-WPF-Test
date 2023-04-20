using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CryptoApp.ViewModel
{
    public class TopCurrenciesViewModel : ViewModelBase
    {
        public IList<CurrencyViewModel> CurrencyViewModels { get; set; }

        private int _topN = 10;
        public int TopN 
        { 
            get { return _topN; } 
            set 
            {
                _topN = value;
                OnPropertyChanged(nameof(TopN));
            } 
        }

        private string _currencyName;
        public string CurrencyName 
        { 
            get { return _currencyName; } 
            set 
            {
                _currencyName = value;
                OnPropertyChanged(nameof(CurrencyName));
            } 
        }

        public ICommand SearchCommand { get; }
        public ICommand GetCurrencyData { get; }
        public ICommand MakeExchange { get; }
    }
}
