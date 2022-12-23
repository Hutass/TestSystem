using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem.Model
{
    class AnswerCRUDModel
    {
        IDBCRUD dbOperations;
        IAuthorizationService authorizationService;


        public AnswerCRUDModel(IDBCRUD crud, IAuthorizationService auth)
        {
            dbOperations = crud;
            authorizationService = auth;
        }


        public List<BLL.Models.AnswerModel> GetAnswers()
        {
            return dbOperations.GetAllAnswers().ToList();
        }
        public List<BLL.Models.QuestionModel> GetQuestions()
        {
            return dbOperations.GetAllQuestions().ToList();
        }
        public int CreateAnswer(BLL.Models.AnswerModel result)
        {
            return dbOperations.CreateAnswer(result);
        }
        public void DeleteAnswer(BLL.Models.AnswerModel result)
        {
            dbOperations.DeleteAnswer(result);
        }
        public void UpdateAnswer(BLL.Models.AnswerModel result)
        {
            if (result.QuestionID == null)
                result.QuestionID = 0;
            if (result.Cost == null)
                result.Cost = 0;
            if (result.Text == null)
                result.Text = "";

            dbOperations.UpdateAnswer(result);
        }
    }
}
