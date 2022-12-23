using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TestSystem.Model;

namespace TestSystem.ViewModel
{
    class AnswerCRUDViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        BLL.Models.PersonModel _currentUser;
        AnswerCRUDModel _model;
        View.AnswerCRUDUserControl _control;
        public ObservableCollection<BLL.Models.QuestionModel> Questions { get; set; }
        public ObservableCollection<BLL.Models.AnswerModel> Answers { get; set; }


        private BLL.Models.QuestionModel _selectedQuestion;
        public BLL.Models.QuestionModel SelectedQuestion
        {
            get
            {
                return _selectedQuestion;
            }
            set
            {
                _selectedQuestion = value;
                SelectedAnswer.QuestionID = _selectedQuestion.ID;
                OnPropertyChanged("SelectedType");

            }
        }
        private BLL.Models.AnswerModel _selectedAnswer;
        public BLL.Models.AnswerModel SelectedAnswer
        {
            get
            {
                return _selectedAnswer;
            }
            set
            {
                if (_selectedAnswer != null && _selectedQuestion.TypeID > 0 && _selectedQuestion.PositionID > 0)
                    _model.UpdateAnswer(_selectedAnswer);
                _selectedAnswer = value;
                if (_selectedAnswer != null)
                {
                    if (_selectedAnswer.QuestionID != null)
                        _control.QuestionComboBox.SelectedItem = Questions.Where(i => i.ID == _selectedAnswer.QuestionID).First();

                }
                OnPropertyChanged("SelectedAnswer");
            }
        }

        private ICommand addCommand;
        public ICommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      BLL.Models.AnswerModel result = new BLL.Models.AnswerModel();
                      Answers.Insert(0, result);
                      result.ID = _model.CreateAnswer(result);
                      SelectedAnswer = result;
                  }));
            }
        }


        private ICommand removeCommand;
        public ICommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                  (removeCommand = new RelayCommand(obj =>
                  {
                      if (Answers.Count > 0 && _selectedAnswer != null)
                      {
                          BLL.Models.AnswerModel result = _selectedAnswer;
                          if (result != null)
                          {
                              _selectedAnswer = Answers.First();
                              Answers.Remove(result);
                              _model.DeleteAnswer(result);
                          }
                      }
                  }));
            }
        }



        public AnswerCRUDViewModel(IDBCRUD dBCRUD, IAuthorizationService authorizationService, View.AnswerCRUDUserControl control, BLL.Models.PersonModel currentUser)
        {
            _model = new AnswerCRUDModel(dBCRUD, authorizationService);
            _currentUser = currentUser;
            _control = control;
            Answers = new ObservableCollection<BLL.Models.AnswerModel>(_model.GetAnswers());
            foreach(BLL.Models.AnswerModel ans in Answers) { ans.Text = ans.Text.TrimEnd(); };
            Questions = new ObservableCollection<BLL.Models.QuestionModel>(_model.GetQuestions());
            _selectedAnswer = new BLL.Models.AnswerModel();
            _selectedQuestion = new BLL.Models.QuestionModel();
            SelectedAnswer = Answers[0];
            SelectedQuestion = Questions.Where(i => i.ID == SelectedAnswer.QuestionID).First();
        }
    }
}
