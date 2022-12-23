using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestSystem.ViewModel;

namespace TestSystem.View
{
    /// <summary>
    /// Логика взаимодействия для PositionCRUDUserControl.xaml
    /// </summary>
    public partial class PositionCRUDUserControl : UserControl
    {
        public PositionCRUDUserControl(IDBCRUD dBCRUD, IAuthorizationService authorizationService, BLL.Models.PersonModel currentUser)
        {
            InitializeComponent();
            DataContext = new PositionCRUDViewModel(dBCRUD, authorizationService, currentUser);
        }
    }
}
