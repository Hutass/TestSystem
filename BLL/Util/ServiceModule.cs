using DAL.Interface;
using DAL.Repository;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Util
{
    public class ServiceModule : NinjectModule
    {
        private string connectionString;
        public ServiceModule(string connection)
        {
            this.connectionString = connection;
        }
        public override void Load()
        {
            Bind<DBRepos>().To<DBReposSQL>().InSingletonScope().WithConstructorArgument(connectionString);
        }
    }
}
