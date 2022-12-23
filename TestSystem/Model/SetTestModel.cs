using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem.Model
{
    class SetTestModel
    {
        IDBCRUD dbOperations;
        IAuthorizationService authorizationService;


        public SetTestModel(IDBCRUD crud, IAuthorizationService auth)
        {
            dbOperations = crud;
            authorizationService = auth;
        }

        public List<BLL.Models.PositionModel> GetPositions()
        {
            return dbOperations.GetAllPositions().ToList();
        }
        public List<BLL.Models.PersonModel> GetPersons()
        {
            return dbOperations.GetAllPersones().ToList();
        }
        public int CreateResult(BLL.Models.PositionModel position, BLL.Models.PersonModel person)
        {
            BLL.Models.TestResultModel result = new BLL.Models.TestResultModel();
            result.PersonID = person.ID;
            result.PositionID = position.ID;
            result.Score = 0;
            return dbOperations.CreateTestResult(result);
        }

    }
}
