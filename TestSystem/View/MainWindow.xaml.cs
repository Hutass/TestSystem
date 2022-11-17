using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TestSystem.ViewModel;

namespace TestSystem.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnCloseButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void OnMaximizeButtonCLick(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                return;
            }
            this.WindowState = WindowState.Maximized;

        }
        private void OnMinimizeButtonClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }


        private void OnLoginButtonClick(object sender, RoutedEventArgs e)
        {
            registrationDialogWindow.IsOpen = true;
        }
        private void OnCancelLoginButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void OnRegistrationButtonClick(object sender, RoutedEventArgs e)
        {
            registrationDialogWindow.IsOpen = false;
            AcceptAutorization();
        }
        private void OnCancelRegistrationButtonClick(object sender, RoutedEventArgs e)
        {
            registrationDialogWindow.IsOpen = false;

        }


        private void OnReloginButtonClick(object sender, RoutedEventArgs e)
        {
            logoutPanel.Visibility = Visibility.Collapsed;
            mainWindowGrid.Visibility = Visibility.Collapsed;
            loginGrid.Visibility = Visibility.Visible;
            reloginDialogWindow.IsOpen = false;
        }
        private void OnCancelReloginButtonClick(object sender, RoutedEventArgs e)
        {
            reloginDialogWindow.IsOpen = false;

        }


        private void OnLogoutButtonClick(object sender, RoutedEventArgs e)
        {
            reloginDialogWindow.IsOpen = true;
        }


        private void AcceptAutorization()
        {
            loginGrid.Visibility = Visibility.Collapsed;
            this.WindowState = WindowState.Maximized;
            this.loginPasswordBox.Password = null;
            logoutPanel.Visibility = Visibility.Visible;
            mainWindowGrid.Visibility = Visibility.Visible;
        }

        private void OperationsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = OperationsListView.SelectedIndex;

            switch (index)
            {
                case 0:
                    break;
                case 1:
                    contentGid.Children.Clear();
                    contentGid.Children.Add(new TestPassUserControl());
                    break;
                case 2:
                    break;

            }
        }

    }
}

