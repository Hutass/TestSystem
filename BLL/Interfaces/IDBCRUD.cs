using BLL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IDBCRUD
    {
        List<AnswerModel> GetAllAnswers();
        List<PersonModel> GetAllPersones();
        List<PositionModel> GetAllPositions();
        List<QuestionModel> GetAllQuestions();
        List<QuestionTypeModel> GetAllTypes();
        List<RightsModel> GetAllRights();
        List<TestResultModel> GetAllResults();

        int CreateAnswer(AnswerModel q);
        int CreatePerson(PersonModel q);
        int CreatePosition(PositionModel q);
        int CreateQuestion(QuestionModel q);
        int CreateQuestionType(QuestionTypeModel q);
        int CreateTestResult(TestResultModel q);

        void UpdateAnswer(AnswerModel q);
        void UpdatePerson(PersonModel q);
        void UpdatePosition(PositionModel q);
        void UpdateQuestion(QuestionModel q);
        void UpdateQuestionType(QuestionTypeModel q);
        void UpdateTestResult(TestResultModel q);
    }
}
