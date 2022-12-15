using BLL.Interfaces;
using BLL.Util;
using Lab2.Util;
using Ninject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TestSystem.View;
using TestSystem.ViewModel;

namespace TestSystem
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IKernel kernel;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigureContainer();
            ComposeObjects();
            IDBCRUD crudServ = kernel.Get<IDBCRUD>();
            IAuthorizationService authServ = kernel.Get<IAuthorizationService>();
            Current.MainWindow.Show();
        }
        private void ConfigureContainer()
        {
            kernel = new StandardKernel(new NinjectRegistration(), new ServiceModule("TestBaseDBEntities"));
        }
        private void ComposeObjects()
        {
            Current.MainWindow = kernel.Get<MainWindow>();
            Current.MainWindow.Title = "TestProgram";
        }
    }
}
