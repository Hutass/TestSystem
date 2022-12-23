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
    class SetTestViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        BLL.Models.PersonModel _currentUser;
        SetTestModel _model;


        public ObservableCollection<BLL.Models.PositionModel> Positions { get; set; }
        public ObservableCollection<BLL.Models.PersonModel> Persones { get; set; }
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
                OnPropertyChanged("SelectedPerson");
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
                      if(_selectedPerson != null && _selectedPosition != null)
                      {
                          _model.CreateResult(_selectedPosition, _selectedPerson);
                      }
                  }));
            }
        }


        public SetTestViewModel(IDBCRUD dBCRUD, IAuthorizationService authorizationService, BLL.Models.PersonModel currentUser)
        {
            _model = new SetTestModel(dBCRUD, authorizationService);
            _currentUser = currentUser;

            Positions = new ObservableCollection<BLL.Models.PositionModel>(_model.GetPositions());
            Persones = new ObservableCollection<BLL.Models.PersonModel>(_model.GetPersons());
            _selectedPosition = new BLL.Models.PositionModel();
            _selectedPerson = new BLL.Models.PersonModel();
            SelectedPosition = Positions[0];
            SelectedPerson = Persones[0];
        }
    }
}
