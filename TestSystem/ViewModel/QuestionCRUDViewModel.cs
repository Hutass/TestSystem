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
    class QuestionCRUDViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        BLL.Models.PersonModel _currentUser;
        QuestionCRUDModel _model;
        View.QuestionCRUDUserControl _control;
        public ObservableCollection<BLL.Models.QuestionModel> Questions { get; set; }
        public ObservableCollection<BLL.Models.QuestionTypeModel> Types { get; set; }
        public ObservableCollection<BLL.Models.PositionModel> Positions { get; set; }

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
                SelectedQuestion.PositionID = _selectedPosition.ID;
                OnPropertyChanged("SelectedPosition");

            }
        }
        private BLL.Models.QuestionTypeModel _selectedType;
        public BLL.Models.QuestionTypeModel SelectedType
        {
            get
            {
                return _selectedType;
            }
            set
            {
                _selectedType = value;
                SelectedQuestion.TypeID = _selectedType.ID;
                OnPropertyChanged("SelectedType");

            }
        }
        private BLL.Models.QuestionModel _selectedQuestion;
        public BLL.Models.QuestionModel SelectedQuestion
        {
            get
            {
                return _selectedQuestion;
            }
            set
            {
                if (_selectedQuestion != null && _selectedQuestion.TypeID > 0 && _selectedQuestion.PositionID > 0)
                    _model.UpdateQuestion(_selectedQuestion);
                _selectedQuestion = value;
                if (_selectedQuestion != null)
                {
                    if (_selectedQuestion.PositionID != null)
                        _control.PositionComboBox.SelectedItem = Positions.Where(i => i.ID == _selectedQuestion.PositionID).First();
                    if (_selectedQuestion.TypeID != null)
                        _control.PersonComboBox.SelectedItem = Types.Where(i => i.ID == _selectedQuestion.TypeID).First();
                }
                OnPropertyChanged("SelectedQuestion");
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
                      BLL.Models.QuestionModel result = new BLL.Models.QuestionModel();
                      Questions.Insert(0, result);
                      result.ID = _model.CreateQuestion(result);
                      SelectedQuestion = result;
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
                      if (Questions.Count > 0 && _selectedQuestion != null)
                      {
                          BLL.Models.QuestionModel result = _selectedQuestion;
                          if (result != null)
                          {
                              _selectedQuestion = Questions.First();
                              Questions.Remove(result);
                              _model.DeleteQuestion(result);
                          }
                      }
                  }));
            }
        }



        public QuestionCRUDViewModel(IDBCRUD dBCRUD, IAuthorizationService authorizationService, View.QuestionCRUDUserControl control, BLL.Models.PersonModel currentUser)
        {
            _model = new QuestionCRUDModel(dBCRUD, authorizationService);
            _currentUser = currentUser;
            _control = control;
            Questions = new ObservableCollection<BLL.Models.QuestionModel>(_model.GetQuestions());
            Positions = new ObservableCollection<BLL.Models.PositionModel>(_model.GetPositions());
            Types = new ObservableCollection<BLL.Models.QuestionTypeModel>(_model.GetTypes());
            _selectedQuestion = new BLL.Models.QuestionModel();
            _selectedPosition = new BLL.Models.PositionModel();
            _selectedType = new BLL.Models.QuestionTypeModel();
            SelectedQuestion = Questions[0];
            SelectedPosition = Positions.Where(i => i.ID == SelectedQuestion.PositionID).First();
            SelectedType = Types.Where(i => i.ID == SelectedQuestion.TypeID).First();
        }
    }
}