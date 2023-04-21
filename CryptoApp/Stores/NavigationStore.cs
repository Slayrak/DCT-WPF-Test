using CryptoApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.Stores
{
    public class NavigationStore
    {
        public ViewModelBase _currentViewModel;

        public ViewModelBase CurrentViewModel 
        {
            get { return _currentViewModel; }
            set { _currentViewModel = value; }
        }
    }
}
