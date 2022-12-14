using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.ComponentModel;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using System.Globalization;
using System.Windows.Data;
using BLL.Interfaces;
using TestSystem.Model;

namespace TestSystem.ViewModel
{
    internal class MainViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public BLL.Models.PersonModel currentUser;

        private BLL.Models.PersonModel _user;
        public BLL.Models.PersonModel UserLogin
        {
            get
            {
                if(_user == null) _user = new BLL.Models.PersonModel();
                return _user;
               
            }
            set
            {
                _user = value;
                OnPropertyChanged(nameof(UserLogin));
            }
        }

        private ICommand _maximizeButtonCommand;
        public ICommand MaximizeButtonCommand
        {
            get
            {
                return _maximizeButtonCommand;
            }
            set
            {
                _maximizeButtonCommand = value;
            }
        }

        private ICommand _minimizeButtonCommand;
        public ICommand MinimizeButtonCommand
        {
            get
            {
                return _minimizeButtonCommand;
            }
            set
            {
                _minimizeButtonCommand = value;
            }
        }

        private ICommand _closeButtonCommand;
        public ICommand CloseButtonCommand
        {
            get
            {
                return _closeButtonCommand;
            }
            set
            {
                _closeButtonCommand = value;
            }
        }

        private ICommand _loginButtonCommand;
        public ICommand LoginButtonCommand
        {
            get
            {
                return _loginButtonCommand;
            }
            set
            {
                _loginButtonCommand = value;
            }
        }

        private ICommand _registrationButtonCommand;
        public ICommand RegistrationButtonCommand
        {
            get
            {
                return _registrationButtonCommand;
            }
            set
            {
                _registrationButtonCommand = value;
            }
        }

        private ICommand _registrationCancelButtonCommand;
        public ICommand RegistrationCancelButtonCommand
        {
            get
            {
                return _registrationCancelButtonCommand;
            }
            set
            {
                _registrationCancelButtonCommand = value;
            }
        }

        private ICommand _reloginButtonCommand;
        public ICommand ReloginButtonCommand
        {
            get
            {
                return _reloginButtonCommand;
            }
            set
            {
                _reloginButtonCommand = value;
            }
        }

        private ICommand _reloginCancelButtonCommand;
        public ICommand ReloginCancelButtonCommand
        {
            get
            {
                return _reloginCancelButtonCommand;
            }
            set
            {
                _reloginCancelButtonCommand = value;
            }
        }

        private ICommand _logoutButtonCommand;
        public ICommand LogoutButtonCommand
        {
            get
            {
                return _logoutButtonCommand;
            }
            set
            {
                _logoutButtonCommand = value;
            }
        }
        //private ICommand _listViewSelectionChangedCommand;
        //public ICommand ListViewSelectionChangedCommand
        //{
        //    get
        //    {
        //        return _listViewSelectionChangedCommand;
        //    }
        //    set
        //    {
        //        _listViewSelectionChangedCommand = value;
        //    }
        //}

        MainModel _model;
        View.MainWindow _control;
        IDBCRUD _dBCRUD;
        IAuthorizationService _authorizationService;
        public MainViewModel(IDBCRUD dBCRUD, IAuthorizationService authorizationService, View.MainWindow control)
        {
            MaximizeButtonCommand = new RelayCommand(new Action<object>(OnMaximizeButtonCLick));
            MinimizeButtonCommand = new RelayCommand(new Action<object>(OnMinimizeButtonClick));
            CloseButtonCommand = new RelayCommand(new Action<object>(OnCloseButtonClick));
            LoginButtonCommand = new RelayCommand(new Action<object>(OnLoginButtonClick));
            RegistrationButtonCommand = new RelayCommand(new Action<object>(OnRegistrationButtonClick));
            RegistrationCancelButtonCommand = new RelayCommand(new Action<object>(OnCancelRegistrationButtonClick));
            ReloginButtonCommand = new RelayCommand(new Action<object>(OnReloginButtonClick));
            ReloginCancelButtonCommand = new RelayCommand(new Action<object>(OnCancelReloginButtonClick));
            LogoutButtonCommand = new RelayCommand(new Action<object>(OnLogoutButtonClick));
            //ListViewSelectionChangedCommand = new RelayCommand(new Action<object>(OperationsListView_SelectionChangedCommand));
            currentUser = new BLL.Models.PersonModel();
            _authorizationService = authorizationService;
            _model = new MainModel(dBCRUD, authorizationService);
            _control = control;
        }
        private void OnCloseButtonClick(object obj)
        {
            ((Window)obj).Close();
        }
        private void OnMaximizeButtonCLick(object obj)
        {
                if (((Window)obj).WindowState == WindowState.Maximized)
                {
                    ((Window)obj).WindowState = WindowState.Normal;
                    return;
                }
            ((Window)obj).WindowState = WindowState.Maximized;
        }
        private void OnMinimizeButtonClick(object obj)
        {
            ((Window)obj).WindowState = WindowState.Minimized;
        }

        private void OnLoginButtonClick(object obj)
        {
            var values = (object[])obj;

            UserLogin.Password = ((PasswordBox)values[6]).Password;
            switch (_model.AuthorizationCheck(UserLogin))
            {
                case 0:
                    AcceptAutorization(obj);
                    break;
                case 1:
                    ((PasswordBox)values[6]).Foreground = System.Windows.Media.Brushes.Red;
                    break;
                case 2:
                    ((DialogHost)values[0]).IsOpen = true;
                    break;
                case 3:
                    break;
            }
        }

        private void OnRegistrationButtonClick(object obj)
        {
            var values = (object[])obj;

            switch (_model.AuthorizationCheck(UserLogin))
            {
                case 0:
                    
                    break;
                case 1:
                    
                    break;
                case 2:
                    _model.CreatePerson(UserLogin);
                    ((DialogHost)values[0]).IsOpen = false;
                    AcceptAutorization(obj);
                    break;
                case 3:
                    break;
            }

        }
        private void OnCancelRegistrationButtonClick(object obj)
        {
            ((DialogHost)obj).IsOpen = false;

        }


        private void OnReloginButtonClick(object obj)
        {
            var values = (object[])obj;

            ((StackPanel)values[0]).Visibility = Visibility.Collapsed;
            ((Grid)values[1]).Visibility = Visibility.Collapsed;
            ((Grid)values[2]).Visibility = Visibility.Visible;
            ((DialogHost)values[3]).IsOpen = false;


            _control.Item1.Visibility = Visibility.Visible;
            _control.Item2.Visibility = Visibility.Visible;
            _control.Item3.Visibility = Visibility.Visible;
            _control.Item4.Visibility = Visibility.Visible;
            _control.Item5.Visibility = Visibility.Visible;
            _control.Item6.Visibility = Visibility.Visible;
            _control.Item7.Visibility = Visibility.Visible;
            _control.Item8.Visibility = Visibility.Visible;
        }
        private void OnCancelReloginButtonClick(object obj)
        {
            ((DialogHost)obj).IsOpen = false;

        }


        private void OnLogoutButtonClick(object obj)
        {
            ((DialogHost)obj).IsOpen = true;

        }


        private void AcceptAutorization(object obj)
        {
            var values = (object[])obj;

            ((Grid)values[1]).Visibility = Visibility.Collapsed;
            ((Window)values[2]).WindowState = WindowState.Maximized;
            ((PasswordBox)values[3]).Password = null;
            ((StackPanel)values[4]).Visibility = Visibility.Visible;
            ((Grid)values[5]).Visibility = Visibility.Visible;
            currentUser = _model.GetPerson(UserLogin.Mail);
            _control.contentGrid.Children.Clear();
            _control.contentGrid.Children.Add(new View.StartUserControl());

            switch (currentUser.RightsID)
            {
                case 1:
                    _control.Item1.Visibility = Visibility.Collapsed;
                    _control.Item4.Visibility = Visibility.Collapsed;
                    _control.Item5.Visibility = Visibility.Collapsed;
                    _control.Item6.Visibility = Visibility.Collapsed;
                    _control.Item7.Visibility = Visibility.Collapsed;
                    _control.Item8.Visibility = Visibility.Collapsed;
                    break;
                case 2:
                    _control.Item2.Visibility = Visibility.Collapsed;
                    _control.Item4.Visibility = Visibility.Collapsed;
                    _control.Item5.Visibility = Visibility.Collapsed;
                    _control.Item6.Visibility = Visibility.Collapsed;
                    _control.Item7.Visibility = Visibility.Collapsed;
                    _control.Item8.Visibility = Visibility.Collapsed;
                    break;
                case 3:
                    _control.Item2.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        //private void OperationsListView_SelectionChangedCommand(object obj)
        //{
        //    var values = (object[])obj;

        //    int index = ((ListView)values[0]).SelectedIndex;

        //    switch (index)
        //    {
        //        case 0:
        //            break;
        //        case 1:
        //            ((Grid)values[1]).Children.Clear();
        //            ((Grid)values[1]).Children.Add(new TestPassUserControl(_dBCRUD, _authorizationService));
        //            break;
        //        case 2:
        //            break;

        //    }
        //}


    }
}
