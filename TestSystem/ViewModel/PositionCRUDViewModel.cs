using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Model;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TestSystem.Model;

namespace TestSystem.ViewModel
{
    class PositionCRUDViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        BLL.Models.PersonModel _currentUser;
        PositionCRUDModel _model;

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
                if (_selectedPosition != null)
                    _model.UpdatePosition(_selectedPosition);
                _selectedPosition = value;
                OnPropertyChanged("SelectedPosition");

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
                      BLL.Models.PositionModel result = new BLL.Models.PositionModel();
                      Positions.Insert(0, result);
                      result.ID = _model.CreatePosition(result);
                      SelectedPosition = result;
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
                      if (Positions.Count > 0 && _selectedPosition != null)
                      {
                          BLL.Models.PositionModel result = _selectedPosition;
                          if (result != null)
                          {
                              _selectedPosition = Positions.First();
                              Positions.Remove(result);
                              _model.DeletePosition(result);
                          }
                      }
                  }));
            }
        }



        public PositionCRUDViewModel(IDBCRUD dBCRUD, IAuthorizationService authorizationService, BLL.Models.PersonModel currentUser)
        {
            _model = new PositionCRUDModel(dBCRUD, authorizationService);
            _currentUser = currentUser;
            Positions = new ObservableCollection<BLL.Models.PositionModel>(_model.GetPositions());
            foreach(BLL.Models.PositionModel pos in Positions) { pos.Name = pos.Name.TrimEnd(); }
            SelectedPosition = Positions[0];
        }
    }
}
