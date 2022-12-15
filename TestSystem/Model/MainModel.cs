using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem.Model
{
    class MainModel
    {
        IDBCRUD dbOperations;
        IAuthorizationService authorizationService;

        public MainModel(IDBCRUD crud, IAuthorizationService auth)
        {
            dbOperations = crud;
            authorizationService = auth;
        }
    }
}
