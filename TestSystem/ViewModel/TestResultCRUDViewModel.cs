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
    class TestResultCRUDViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        BLL.Models.PersonModel _currentUser;
        TestResultCRUDModel _model;
        View.TestResultCRUDUserControl _control;

        public ObservableCollection<BLL.Models.TestResultModel> Results { get; set; }
        public ObservableCollection<BLL.Models.PositionModel> Positions { get; set; }
        public ObservableCollection<BLL.Models.PersonModel> Persons { get; set; }
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
                SelectedResult.PositionID = _selectedPosition.ID;
                OnPropertyChanged("SelectedPosition");

            }
        }
        private BLL.Models.PersonModel _selectedPerson;
        public BLL.Models.PersonModel SelectedPerson
        {
            get
            {
                return _selectedPerson;
            }
            set
            {
                _selectedPerson = value;
                SelectedResult.PersonID = _selectedPerson.ID;        
                OnPropertyChanged("SelectedPerson");
                
            }
        }
        private BLL.Models.TestResultModel _selectedResult;
        public BLL.Models.TestResultModel SelectedResult
        {
            get
            {
                return _selectedResult;
            }
            set
            {           
                if(_selectedResult != null && _selectedResult.PersonID>0 && _selectedResult.PositionID>0)
                    _model.UpdateResult(_selectedResult);
                _selectedResult = value;
                if (_selectedResult != null)
                {
                    if (_selectedResult.PositionID != null)
                        _control.PositionComboBox.SelectedItem = Positions.Where(i => i.ID == _selectedResult.PositionID).First();
                    if (_selectedResult.PersonID != null)
                        _control.PersonComboBox.SelectedItem = Persons.Where(i => i.ID == _selectedResult.PersonID).First();
                }
                OnPropertyChanged("SelectedResult");
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
                      BLL.Models.TestResultModel result = new BLL.Models.TestResultModel();
                      Results.Insert(0, result);
                      result.ID = _model.CreateResult(result);
                      SelectedResult = result;
                      SelectedResult.PositionID = _selectedPosition.ID;
                      SelectedResult.PersonID = _selectedPerson.ID;

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
                      if (Results.Count > 0 && _selectedResult != null)
                      {
                          BLL.Models.TestResultModel result = _selectedResult;
                          if (result != null)
                          {
                              _selectedResult = Results.First();
                              Results.Remove(result);
                              _model.DeleteResult(result);
                          }
                      }
                  }));
            }
        }

        

        public TestResultCRUDViewModel(IDBCRUD dBCRUD, IAuthorizationService authorizationService, View.TestResultCRUDUserControl control, BLL.Models.PersonModel currentUser)
        {
            _model = new TestResultCRUDModel(dBCRUD, authorizationService);
            _currentUser = currentUser;
            _control = control;
            Results = new ObservableCollection<BLL.Models.TestResultModel>( _model.GetResults());
            Positions = new ObservableCollection<BLL.Models.PositionModel>( _model.GetPositions());
            Persons = new ObservableCollection<BLL.Models.PersonModel>(_model.GetPersons());
            _selectedResult = new BLL.Models.TestResultModel();
            _selectedPosition = new BLL.Models.PositionModel();
            _selectedPerson = new BLL.Models.PersonModel();
            SelectedResult = Results[0];          
            SelectedPosition = Positions.Where(i => i.ID == SelectedResult.PositionID).First();
            SelectedPerson = Persons.Where(i => i.ID == SelectedResult.PersonID).First();
        }
    }
}
