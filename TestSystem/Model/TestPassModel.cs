using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem.Model
{
    class TestPassModel
    {
        IDBCRUD dbOperations;
        IAuthorizationService authorizationService;


        public TestPassModel(IDBCRUD crud, IAuthorizationService auth)
        {
            dbOperations = crud;
            authorizationService = auth;
        }

        public List<BLL.Models.PositionModel> GetPositions()
        {
            return dbOperations.GetAllPositions();
        }
    }
}
