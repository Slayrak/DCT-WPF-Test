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
        private readonly Func<string, ViewModelBase> _createCurrencyViewModel;

        public NavigateCommand(NavigationStore navigationStore, Func<ViewModelBase> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public NavigateCommand(NavigationStore navigationStore, Func<string,ViewModelBase> createViewModel)
        {
            _navigationStore = navigationStore;
            _createCurrencyViewModel = createViewModel;
        }

        public override void Execute(object? parameter)
        {

            if(_createCurrencyViewModel != null)
            {

                _navigationStore.CurrentViewModel = _createCurrencyViewModel(parameter.ToString());
                /*_navigationStore.CurrentViewModel = new CurrencyViewModel(_navigationStore);*/ //попробуй завтра це змінити якомога швидше!
            }
            else
            {
                _navigationStore.CurrentViewModel = _createViewModel();
            }
        }
    }
}
