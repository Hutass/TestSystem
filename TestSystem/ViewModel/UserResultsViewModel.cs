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
using System.Collections.ObjectModel;

namespace TestSystem.ViewModel
{
    class UserResultsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        UserResultsModel _model;
        View.UserResultsUserControl _control;
        BLL.Models.PersonModel _currentUser;

        public UserResultsViewModel(IDBCRUD dBCRUD, IAuthorizationService authorizationService, View.UserResultsUserControl userControl, BLL.Models.PersonModel currentUser)
        {
            _model = new UserResultsModel(dBCRUD, authorizationService);
            _control = userControl;
            _currentUser = currentUser;
            QuestionLoad();
        }

        private void QuestionLoad()
        {
            _control.QuestionStackPanel.Children.Clear();
            List<BLL.Models.PersonModel> persones = new List<BLL.Models.PersonModel>();
            if (_currentUser.RightsID == 1)
                persones = _model.GetPerson(_currentUser);
            else
                persones = _model.GetPersones();


            for (int i = 0; i < persones.Count; i++)
            {
                _control.QuestionStackPanel.Children.Add(new TextBlock { Text = $"{persones[i].Mail.TrimEnd()} - {persones[i].Surname.TrimEnd()} {persones[i].Name.TrimEnd()} {persones[i].MiddleName.TrimEnd()}", HorizontalAlignment = HorizontalAlignment.Left, TextWrapping = TextWrapping.Wrap, Margin = new Thickness { Top = 20 } });
                List<BLL.Models.TestResultModel> results = _model.GetResults(persones[i]);

                for (int j = 0; j < results.Count; j++)
                {
                    if (results[j].Score > 0)
                        _control.QuestionStackPanel.Children.Add(new TextBlock { Text = $"{results[j].Date} пройден тест {_model.GetPosition(results[j])} с результатом {results[j].Score}", HorizontalAlignment = HorizontalAlignment.Left, TextWrapping = TextWrapping.Wrap, Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 0, 0, 0)), Margin = new Thickness{ Left = 40 } });
                    else
                        _control.QuestionStackPanel.Children.Add(new TextBlock { Text = $"{results[j].Date} пройден тест {_model.GetPosition(results[j])} с результатом {results[j].Score}", HorizontalAlignment = HorizontalAlignment.Left, TextWrapping = TextWrapping.Wrap, Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 200, 50, 50)), Margin = new Thickness { Left = 40 } });

                }
            }
            return;
        }      
    }
}
