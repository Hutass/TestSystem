using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BLL;
using BLL.Interfaces;
using BLL.Sevices;


namespace Lab2.Util
{
    public class NinjectRegistration : NinjectModule
    {
        public override void Load()
        {
            Bind<IDBCRUD>().To<DataOperator>();
            Bind<IAuthorizationService>().To<UserAuthorization>();
        }
    }
}
