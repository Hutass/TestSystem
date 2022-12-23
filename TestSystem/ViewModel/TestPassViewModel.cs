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
    class TestPassViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        TestPassModel _model;
        TestPassUserControl _control;
        BLL.Models.PersonModel _currentUser;

        public TestPassViewModel(IDBCRUD dBCRUD, IAuthorizationService authorizationService, TestPassUserControl userControl, BLL.Models.PersonModel currentUser)
        {        
            _model = new TestPassModel(dBCRUD, authorizationService);
            _control = userControl;
            _currentUser = currentUser;
            Positions = new ObservableCollection<BLL.Models.PositionModel>(_model.GetPositions(_currentUser));
            foreach(BLL.Models.PositionModel p in Positions) { p.Name = p.Name.TrimEnd(); }
            RejectTestCommand = new RelayCommand(new Action<object>(OnRejectTest));
            AcceptTestCommand = new RelayCommand(new Action<object>(OnAcceptTest));
        }
        public ObservableCollection<BLL.Models.PositionModel> Positions { get; set; }
        public ObservableCollection<BLL.Models.QuestionModel> Questions { get; set; }
        public ObservableCollection<List<BLL.Models.AnswerModel>> Answers { get; set; }
        private BLL.Models.PositionModel _selectedPosition;

        public BLL.Models.PositionModel SelectedPosition
        {
            get
            {
                return _selectedPosition;
            }
            set
            {
                _selectedPosition = value;
                OnPropertyChanged(nameof(SelectedPosition));
                QuestionLoad();
            }
        }

        private ICommand _rejectTestCommand;
        public ICommand RejectTestCommand
        {
            get
            {
                return _rejectTestCommand;
            }
            set
            {
                _rejectTestCommand = value;
            }
        }
        private ICommand _acceptTestCommand;
        public ICommand AcceptTestCommand
        {
            get
            {
                return _acceptTestCommand;
            }
            set
            {
                _acceptTestCommand = value;
            }
        }

        private void OnRejectTest(object obj)
        {
            _control.QuestionStackPanel.Children.Clear();
            _control.TestStackPanel.Visibility = Visibility.Visible;
            _control.QuestionGrid.Visibility = Visibility.Collapsed;
            
        }
        private void OnAcceptTest(object obj)
        {
            SaveResult();
            _control.QuestionStackPanel.Children.Clear();
            _control.TestStackPanel.Visibility = Visibility.Visible;
            _control.QuestionGrid.Visibility = Visibility.Collapsed;
            Positions.Clear();
            Positions = new ObservableCollection<BLL.Models.PositionModel>(_model.GetPositions(_currentUser));
            foreach (BLL.Models.PositionModel p in Positions) { p.Name = p.Name.TrimEnd(); }
        }

        private void QuestionLoad()
        {
            _control.QuestionStackPanel.Children.Clear();
            _control.TestStackPanel.Visibility = Visibility.Collapsed;
            _control.QuestionGrid.Visibility = Visibility.Visible;
            _control.QuestionStackPanel.Children.Add(new Label { Content = _selectedPosition.Name, HorizontalAlignment = HorizontalAlignment.Center });
            Questions = new ObservableCollection<BLL.Models.QuestionModel>(_model.GetQuestions(_selectedPosition.ID));
            Answers = new ObservableCollection<List<BLL.Models.AnswerModel>>();

            for(int i=0;i<Questions.Count;i++)
            {
                _control.QuestionStackPanel.Children.Add(new TextBlock { Text = Questions[i].Text, HorizontalAlignment = HorizontalAlignment.Left, TextWrapping = TextWrapping.Wrap });

                Answers.Add(_model.GetAnswers(Questions[i].ID));
               

                for (int j=0;j<Answers[i].Count;j++)
                {
                    switch(Questions[i].TypeID)
                    {
                        case 1:
                            _control.QuestionStackPanel.Children.Add(new RadioButton {Content = Answers[i][j].Text, GroupName = $"Group{i}", IsChecked = false});
                            break;
                        case 2:
                            _control.QuestionStackPanel.Children.Add(new CheckBox { Content = Answers[i][j].Text, IsChecked = false });
                            break;
                        case 3:
                            _control.QuestionStackPanel.Children.Add(new TextBox { });
                            break;
                    }
                }
            }
            return;         
        }
        private void SaveResult()
        {
            List<BLL.Models.AnswerModel> bufAnswers = new List<BLL.Models.AnswerModel>();
            int j = 0, k = 0;
            for (int i = 0; i < _control.QuestionStackPanel.Children.Count; i++)
            {
                switch (_control.QuestionStackPanel.Children[i])
                {
                    case RadioButton b:
                        if (((RadioButton)_control.QuestionStackPanel.Children[i]).IsChecked == true)
                            bufAnswers.Add(Answers[j][k]);
                        if (k < Answers[j].Count-1)
                            k++;
                        else
                        {
                            k = 0;
                            if (j < Answers.Count - 1)
                                j++;
                            else
                                break;
                        }
                        break;
                    case CheckBox c:
                        if (((CheckBox)_control.QuestionStackPanel.Children[i]).IsChecked == true)
                            bufAnswers.Add(Answers[j][k]);
                        if (k < Answers[j].Count - 1)
                            k++;
                        else
                        {
                            k = 0;
                            if (j < Answers.Count - 1)
                                j++;
                            else
                                break;
                        }
                        break;
                    case TextBox t:
                        if (((TextBox)_control.QuestionStackPanel.Children[i]).Text != null)
                            if(((TextBox)_control.QuestionStackPanel.Children[i]).Text == Answers[j][k].Text )
                            bufAnswers.Add(Answers[j][k]);
                            else
                            {
                                Answers[j][k].Cost = 0 - Answers[j][k].Cost;
                                bufAnswers.Add(Answers[j][k]);
                            }
                        if (k < Answers[j].Count - 1)
                            k++;
                        else
                        {
                            k = 0;
                            if (j < Answers.Count - 1)
                                j++;
                            else
                                break;
                        }
                        break;
                }
            }
            return;
        }
    }  
}
