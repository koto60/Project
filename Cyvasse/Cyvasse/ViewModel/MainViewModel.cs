using Cyvasse.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cyvasse.ViewModel {
    /// <summary>
    /// The main view is mainly used for navigation between views. It is never shown.
    /// </summary>
    public class MainViewModel : ObservableObject {

        private IViewModel _currentViewModel;
        public IViewModel CurrentViewModel
        {
            get{ return _currentViewModel; }
            set
            {
                if (_currentViewModel != value)
                {
                    _currentViewModel = value;
                    OnPropertyChanged("CurrentViewModel");
                }
            }
        }

        private List<IViewModel> _viewModels;
        public List<IViewModel> ViewModels
        {
            get
            {
                if (_viewModels == null)
                    _viewModels = new List<IViewModel>();

                return _viewModels;
            }
        }

        public ICommand ViewMenuCommand { get; private set; }
        public ICommand ViewSoloGameCommand { get; private set; }

        /// <summary>
        /// Sets up the navigation, and makes the viewmodels and commands.
        /// </summary>
        public MainViewModel() {
            //All viewmodels are made here. When new are made remember to add them here.
            MenuViewModel menuVM = new MenuViewModel();
            SoloGameViewModel soloGameVM = new SoloGameViewModel();

            //All viewmodels are added to the list. When new are made remember to add them here.
            ViewModels.Add(menuVM);
            ViewModels.Add(soloGameVM);

            //Commands to change between the viewmodels. When new are made  remember to add them here.
            ViewSoloGameCommand = new RelayCommand(p => CurrentViewModel = soloGameVM);
            ViewMenuCommand = new RelayCommand(p => CurrentViewModel = menuVM);

            //Decides the viewmodel we start on.
            CurrentViewModel = menuVM;
        }
    }
}
