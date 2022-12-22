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

        public ObservableCollection<BLL.Models.TestResultModel> Results { get; set; }
        private BLL.Models.TestResultModel _selectedResult;
        public BLL.Models.TestResultModel SelectedResult
        {
            get
            {
                return _selectedResult;
            }
            set
            {
                _selectedResult = value;
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
                      SelectedResult = result;
                  }));
            }
        }

        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                  (removeCommand = new RelayCommand(obj =>
                  {
                      if (Results.Count > 0 && _selectedResult != null)
                      {
                          BLL.Models.TestResultModel result = obj as BLL.Models.TestResultModel;
                          if (result != null)
                          {
                              Results.Remove(result);
                          }
                      }
                  }));
            }
        }

        

        public TestResultCRUDViewModel(IDBCRUD dBCRUD, IAuthorizationService authorizationService, BLL.Models.PersonModel currentUser)
        {
            _model = new TestResultCRUDModel(dBCRUD, authorizationService);
            _currentUser = currentUser;
            Results = new ObservableCollection<BLL.Models.TestResultModel>( _model.GetResults());
        }
    }
}
