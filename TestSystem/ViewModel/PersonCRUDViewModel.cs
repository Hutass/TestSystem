
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
    class PersonCRUDViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        BLL.Models.PersonModel _currentUser;
        PersonCRUDModel _model;
        View.PersonCRUDUserControl _control;
        public ObservableCollection<BLL.Models.PersonModel> Persones { get; set; }
        public ObservableCollection<BLL.Models.RightsModel> Rights { get; set; }

        private BLL.Models.RightsModel _selectedRights;
        public BLL.Models.RightsModel SelectedRights
        {
            get
            {
                return _selectedRights;
            }
            set
            {
                _selectedRights = value;
                SelectedPerson.RightsID = _selectedRights.ID;
                OnPropertyChanged("SelectedRights");

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
                if (_selectedPerson != null && _selectedPerson.RightsID > 0)
                    _model.UpdatePerson(_selectedPerson);
                _selectedPerson = value;
                if (_selectedPerson != null)
                {
                    if (_selectedPerson.RightsID != null)
                        _control.RightsComboBox.SelectedItem = Rights.Where(i => i.ID == _selectedPerson.RightsID).First();

                }
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
                      BLL.Models.PersonModel result = new BLL.Models.PersonModel();
                      Persones.Insert(0, result);
                      result.ID = _model.CreatePerson(result);
                      SelectedPerson = result;
                      SelectedPerson.RightsID = _selectedRights.ID;

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
                      if (Persones.Count > 0 && _selectedPerson != null)
                      {
                          BLL.Models.PersonModel result = _selectedPerson;
                          if (result != null)
                          {
                              _selectedPerson = Persones.First();
                              Persones.Remove(result);
                              _model.DeletePerson(result);
                          }
                      }
                  }));
            }
        }



        public PersonCRUDViewModel(IDBCRUD dBCRUD, IAuthorizationService authorizationService, View.PersonCRUDUserControl control, BLL.Models.PersonModel currentUser)
        {
            _model = new PersonCRUDModel(dBCRUD, authorizationService);
            _currentUser = currentUser;
            _control = control;
            Persones = new ObservableCollection<BLL.Models.PersonModel>(_model.GetPersones());
            foreach(BLL.Models.PersonModel pers in Persones) { pers.Name = pers.Name.TrimEnd(); }
            foreach (BLL.Models.PersonModel pers in Persones) { pers.Surname = pers.Surname.TrimEnd(); }
            foreach (BLL.Models.PersonModel pers in Persones) { pers.MiddleName = pers.MiddleName.TrimEnd(); }
            foreach (BLL.Models.PersonModel pers in Persones) { pers.Mail = pers.Mail.TrimEnd(); }
            foreach (BLL.Models.PersonModel pers in Persones) { pers.Password = pers.Password.TrimEnd(); }
            Rights = new ObservableCollection<BLL.Models.RightsModel>(_model.GetRights());
            _selectedPerson = new BLL.Models.PersonModel();
            _selectedRights = new BLL.Models.RightsModel();
            SelectedPerson = Persones[0];
            SelectedRights = Rights.Where(i => i.ID == SelectedPerson.RightsID).First();

        }
    }
}
