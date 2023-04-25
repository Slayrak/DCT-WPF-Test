using CryptoApp.Domain.Interfaces;
using CryptoApp.Domain.Models;
using CryptoApp.Stores;
using CryptoApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CryptoApp.Commands
{
    public class NavigateCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<ViewModelBase> _createViewModel;
        private readonly Func<Currency, ViewModelBase> _createCurrencyViewModel;
        private readonly ICurrencyService _currencyService;

        public NavigateCommand(NavigationStore navigationStore, Func<ViewModelBase> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public NavigateCommand(NavigationStore navigationStore, Func<Currency,ViewModelBase> createViewModel, ICurrencyService currencyService)
        {
            _navigationStore = navigationStore;
            _createCurrencyViewModel = createViewModel;
            _currencyService = currencyService;
        }

        public override async void Execute(object? parameter)
        {

            if(_createCurrencyViewModel != null)
            {

                var result = await _currencyService.GetCurrencyByName(parameter.ToString());

                _navigationStore.CurrentViewModel = _createCurrencyViewModel(result);
            }
            else
            {
                _navigationStore.CurrentViewModel = _createViewModel();
            }
        }
    }
}
