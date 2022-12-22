using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem.Model
{
    class TestResultCRUDModel
    {
        IDBCRUD dbOperations;
        IAuthorizationService authorizationService;


        public TestResultCRUDModel(IDBCRUD crud, IAuthorizationService auth)
        {
            dbOperations = crud;
            authorizationService = auth;
        }


        public List<BLL.Models.TestResultModel> GetResults()
        {
            return dbOperations.GetAllResults().ToList();
        }
    }
}
