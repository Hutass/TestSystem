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
        public List<BLL.Models.PositionModel> GetPositions()
        {
            return dbOperations.GetAllPositions().ToList();
        }
        public List<BLL.Models.PersonModel> GetPersons()
        {
            return dbOperations.GetAllPersones().ToList();
        }
        public int CreateResult(BLL.Models.TestResultModel result)
        {
           return dbOperations.CreateTestResult(result);
        }
        public void DeleteResult(BLL.Models.TestResultModel result)
        {
            dbOperations.DeleteTestResult(result);
        }
        public void UpdateResult(BLL.Models.TestResultModel result)
        {
            if (result.PersonID == null)
                result.PersonID = 0;
            if (result.PositionID == null)
                result.PositionID = 0;
            if (result.Score == null)
                result.Score = 0;
            if (result.Date == null)
                result.Date = DateTime.Now;

            dbOperations.UpdateTestResult(result);
        }
    }
}
