using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem.Model
{
    class UserResultsModel
    {
        IDBCRUD dbOperations;
        IAuthorizationService authorizationService;


        public UserResultsModel(IDBCRUD crud, IAuthorizationService auth)
        {
            dbOperations = crud;
            authorizationService = auth;
        }

        public List<BLL.Models.TestResultModel> GetResults(BLL.Models.PersonModel user)
        {
            return dbOperations.GetAllResults().Where(i => i.PersonID == user.ID).ToList();
        }
        public List<BLL.Models.PersonModel> GetPersones()
        {
            return dbOperations.GetAllPersones().ToList();
        }
        public List<BLL.Models.PersonModel> GetPerson(BLL.Models.PersonModel person)
        {
            return dbOperations.GetAllPersones().ToList().Where(i=> i.ID == person.ID).ToList();
        }
        public List<BLL.Models.AnswerModel> GetAnswers(int questionID)
        {
            return dbOperations.GetAllAnswers().Where(i => i.QuestionID == questionID).ToList();
        }
        public string GetPosition(BLL.Models.TestResultModel result)
        {
            return dbOperations.GetAllPositions().ToList().Where(i => i.ID == result.PositionID).First().Name;
        }
    }
}
