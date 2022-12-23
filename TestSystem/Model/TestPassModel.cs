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

        public List<BLL.Models.PositionModel> GetPositions(BLL.Models.PersonModel user)
        {
            List<BLL.Models.TestResultModel> result = dbOperations.GetAllResults().Where(i => i.PersonID == user.ID).ToList().Where(i => i.Score == 0).ToList();
            return dbOperations.GetAllPositions().Join(dbOperations.GetAllResults().Where(i => i.PersonID == user.ID).ToList().Where(i => i.Score == 0).ToList(), p => p.ID, r => r.PositionID, (p, r) => new BLL.Models.PositionModel { ID = p.ID, Name = p.Name }).ToList();
        }
        public List<BLL.Models.QuestionModel> GetQuestions(int positionID)
        {
            return dbOperations.GetAllQuestions().Where(i => i.PositionID == positionID).ToList();
        }
        public List<BLL.Models.AnswerModel> GetAnswers(int questionID)
        {
            return dbOperations.GetAllAnswers().Where(i => i.QuestionID == questionID).ToList();
        }

        public void SaveResults(List<BLL.Models.AnswerModel> answers, BLL.Models.PositionModel position, BLL.Models.PersonModel person)
        {
            BLL.Models.TestResultModel result = dbOperations.GetAllResults().Where(i => i.PositionID == position.ID).Where(i => i.PersonID == person.ID).Where(i => i.Score == 0).First();
            authorizationService.SubmitReport(answers, result, person);
        }
    }
}
