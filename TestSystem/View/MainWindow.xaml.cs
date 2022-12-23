﻿using BLL.Interfaces;
using BLL.Util;
using Lab2.Util;
using Ninject;
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
        //временно
        IDBCRUD dBCRUD;
        IAuthorizationService authorizationService;
        MainViewModel _viewModel;

        public MainWindow(IDBCRUD dBCRUD, IAuthorizationService authorizationService)
        {

            InitializeComponent();
            _viewModel = new MainViewModel(dBCRUD, authorizationService, this);
            DataContext = _viewModel;         
            this.dBCRUD = dBCRUD;
            this.authorizationService = authorizationService;
            UpperPanel.MouseLeftButtonDown += DragMove_Window;
        }

        private void DragMove_Window(object sender, MouseButtonEventArgs e)
        {
           DragMove();
        }

        private void OperationsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = OperationsListView.SelectedIndex;

            switch (index)
            {
                case 0:
                    break;
                case 1:
                    contentGrid.Children.Clear();
                    contentGrid.Children.Add(new TestPassUserControl(dBCRUD, authorizationService, _viewModel.currentUser));
                    break;
                case 2:
                    contentGrid.Children.Clear();
                    contentGrid.Children.Add(new TestResultCRUDUserControl(dBCRUD, authorizationService, _viewModel.currentUser));               
                    break;
                case 3:
                    contentGrid.Children.Clear();
                    contentGrid.Children.Add(new PositionCRUDUserControl(dBCRUD, authorizationService, _viewModel.currentUser));
                    break;
                case 4:
                    contentGrid.Children.Clear();
                    contentGrid.Children.Add(new QuestionCRUDUserControl(dBCRUD, authorizationService, _viewModel.currentUser));
                    break;
                case 5:
                    contentGrid.Children.Clear();
                    contentGrid.Children.Add(new AnswerCRUDUserControl(dBCRUD, authorizationService, _viewModel.currentUser));
                    break;
                case 6:
                    contentGrid.Children.Clear();
                    contentGrid.Children.Add(new PersonCRUDUserControl(dBCRUD, authorizationService, _viewModel.currentUser));
                    break;
            }
        }

        public void ChangeControl(UserControl control)
        {
            contentGrid.Children.Clear();
            contentGrid.Children.Add(control);
        }

        //private void OnReloginButtonClick(object sender, RoutedEventArgs e)
        //{
        //    logoutPanel.Visibility = Visibility.Collapsed;
        //    mainWindowGrid.Visibility = Visibility.Collapsed;
        //    loginGrid.Visibility = Visibility.Visible;
        //    reloginDialogWindow.IsOpen = false;
        //}
        //private void OnCancelReloginButtonClick(object sender, RoutedEventArgs e)
        //{
        //    reloginDialogWindow.IsOpen = false;

        //}


        //private void OnLogoutButtonClick(object sender, RoutedEventArgs e)
        //{
        //    reloginDialogWindow.IsOpen = true;
        //}


        //private void AcceptAutorization()
        //{
        //    loginGrid.Visibility = Visibility.Collapsed;
        //    this.WindowState = WindowState.Maximized;
        //    this.loginPasswordBox.Password = null;
        //    logoutPanel.Visibility = Visibility.Visible;
        //    mainWindowGrid.Visibility = Visibility.Visible;
        //}

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

