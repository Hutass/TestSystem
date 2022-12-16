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

        MainModel model;
        public MainViewModel(IDBCRUD dBCRUD, IAuthorizationService authorizationService)
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
            model = new MainModel(dBCRUD, authorizationService);
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
            switch (model.AuthorizationCheck(UserLogin))
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

            switch (model.AuthorizationCheck(UserLogin))
            {
                case 0:
                    
                    break;
                case 1:
                    
                    break;
                case 2:
                    model.CreatePerson(UserLogin);
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
        }

        //private void OperationsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    int index = OperationsListView.SelectedIndex;

        //    switch (index)
        //    {
        //        case 0:
        //            break;
        //        case 1:
        //            contentGid.Children.Clear();
        //            contentGid.Children.Add(new TestPassUserControl());
        //            break;
        //        case 2:
        //            break;

        //    }
        //}


    }
}
