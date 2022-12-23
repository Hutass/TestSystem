using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem.Model
{
    class QuestionCRUDModel
    {
        IDBCRUD dbOperations;
        IAuthorizationService authorizationService;


        public QuestionCRUDModel(IDBCRUD crud, IAuthorizationService auth)
        {
            dbOperations = crud;
            authorizationService = auth;
        }


        public List<BLL.Models.QuestionModel> GetQuestions()
        {
            return dbOperations.GetAllQuestions().ToList();
        }
        public List<BLL.Models.PositionModel> GetPositions()
        {
            return dbOperations.GetAllPositions().ToList();
        }
        public List<BLL.Models.QuestionTypeModel> GetTypes()
        {
            return dbOperations.GetAllTypes().ToList();
        }
        public int CreateQuestion(BLL.Models.QuestionModel result)
        {
            return dbOperations.CreateQuestion(result);
        }
        public void DeleteQuestion(BLL.Models.QuestionModel result)
        {
            dbOperations.DeleteQuestion(result);
        }
        public void UpdateQuestion(BLL.Models.QuestionModel result)
        {
            if (result.PositionID == null)
                result.PositionID = 0;
            if (result.TypeID == null)
                result.TypeID = 0;
            if (result.Text == null)
                result.Text = "";

            dbOperations.UpdateQuestion(result);
        }
    }
}
