using CryptoApp.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.Commands
{
    public class SearchCommand : CommandBase
    {
        private readonly TopCurrenciesViewModel _topCurrenciesViewModel;
        public SearchCommand(TopCurrenciesViewModel topCurrenciesViewModel)
        {
            _topCurrenciesViewModel = topCurrenciesViewModel;

            _topCurrenciesViewModel.PropertyChanged += OnViewPropertyChanged;
        }

        private void OnViewPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(TopCurrenciesViewModel.CurrencyName))
            {
                OnCanExecuteChanged();
            }
        }

        public override void Execute(object? parameter)
        {
            throw new NotImplementedException();
        }

        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_topCurrenciesViewModel.CurrencyName) && base.CanExecute(parameter);
        }
    }
}
