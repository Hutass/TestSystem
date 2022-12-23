using BLL.Interfaces;
using BLL.Models;
using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Sevices
{
    public class UserAuthorization : IAuthorizationService
    {
        private DBRepos db;
        public UserAuthorization(DBRepos repos)
        {
            db = repos;
        }
        public int Authorize(PersonModel personAuthData)
        {
            if (personAuthData.Mail == null)
                return 3;
            List<PersonModel> person = db.Person.GetList().Select(i => new PersonModel(i)).Where(i => i.Mail.TrimEnd().Equals(personAuthData.Mail)).ToList();// db.Person.GetList().Select(i => new PersonModel(i)).Where(i => i.Mail == personAuthData.Mail).ToList();
            if (person.Count == 1)
            {
                if (person[0].Password.TrimEnd() == personAuthData.Password)
                    return 0;
                else
                    return 1;
            }
            else
                return 2;

        }
        public void SubmitReport(List<AnswerModel> answers, TestResultModel result, PersonModel person)
        {
            double score = 0;
            foreach(AnswerModel answer in answers) { score += (double)answer.Cost; }
            TestResultModel bufResult = new TestResultModel();
            bufResult.Date = DateTime.Now;
            bufResult.PersonID = person.ID;
            bufResult.PositionID = result.PositionID;
            bufResult.ID = result.ID;
            bufResult.Score = score;
            IDBCRUD crud = new DataOperator(db);
            crud.UpdateTestResult(bufResult);
        }
        public void SaveReport(List<AnswerModel> answers, TestResultModel result, PersonModel person)
        {

        }
    }
}
