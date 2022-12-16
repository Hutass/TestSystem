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
        TestPassModel model;
        public TestPassViewModel(IDBCRUD dBCRUD, IAuthorizationService authorizationService)
        {        
            model = new TestPassModel(dBCRUD, authorizationService);
            Positions = new ObservableCollection<BLL.Models.PositionModel>(model.GetPositions());
            foreach(BLL.Models.PositionModel p in Positions) { p.Name.TrimEnd(); }
        }
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
                OnPropertyChanged(nameof(SelectedPosition));
            }
        }
    }
}
