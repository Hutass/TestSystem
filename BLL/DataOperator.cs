using DAL;
using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using DAL.Interface;
using BLL.Interfaces;

namespace BLL
{
    public class DataOperator : IDBCRUD
    {
        private DBRepos db;

        public DataOperator(DBRepos repos)
        {
            db = repos;
        }

        public List<QuestionModel> GetAllQuestions()
        {
            List<QuestionModel> list = db.Question.GetList().Select(i => new QuestionModel(i)).ToList();
            return list;
        }
        public List<QuestionTypeModel> GetAllTypes()
        {
            return db.QuestionType.GetList().Select(i => new QuestionTypeModel(i)).ToList();
        }
        public List<PositionModel> GetAllPositions()
        {
            return db.Position.GetList().Select(i => new PositionModel(i)).ToList();
        }
        public List<PersonModel> GetAllPersones()
        {
            return db.Person.GetList().Select(i => new PersonModel(i)).ToList();
        }
        public List<AnswerModel> GetAllAnswers()
        {
            return db.Answer.GetList().Select(i => new AnswerModel(i)).ToList();
        }
        public List<RightsModel> GetAllRights()
        {
            return db.Rights.GetList().Select(i => new RightsModel(i)).ToList();
        }
        public List<TestResultModel> GetAllResults()
        {
            return db.TestResult.GetList().Select(i => new TestResultModel(i)).ToList();
        }


        public int CreateQuestion(QuestionModel q)
        {
            Question qu = new Question() { Text = q.Text, TypeID = q.TypeID, PositionID = q.PositionID };
             db.Question.Create(qu);
            Save();
            return qu.ID;
        }
        public int CreatePosition(PositionModel q)
        {
            Position po = new Position() { Name = q.Name };
            db.Position.Create(po);
            Save();
            return po.ID;
        }
        public int CreateAnswer(AnswerModel q)
        {
            Answer an = new Answer() { Text = q.Text, Cost = q.Cost, QuestionID = q.QuestionID };
            db.Answer.Create(an);
            Save();
            return an.ID;
        }
        public int CreatePerson(PersonModel q)
        {
            Person pr = new Person() { FullName= q.FullName, RightsID=q.RightsID, Password=q.Password, Mail=q.Mail };
            db.Person.Create(pr);
            Save();
            return pr.ID;
        }
        public int CreateQuestionType(QuestionTypeModel q)
        {
            QuestionType qt = new QuestionType() { Name = q.Name };
            db.QuestionType.Create(qt);
            Save();
            return qt.ID;
        }
        public int CreateTestResult(TestResultModel q)
        {
            TestResult tr = new TestResult() { PersonID = q.PersonID, PositionID = q.PositionID, Date = q.Date, Score = q.Score };
            db.TestResult.Create(tr);
            Save();
            return tr.ID;
        }


        public void UpdateQuestion(QuestionModel q)
        {
            Question quest = db.Question.GetItem(q.ID);
            quest.PositionID = q.PositionID;
            quest.TypeID = q.TypeID;
            quest.Text = q.Text;
            Save();
        }            
        public void UpdateAnswer(AnswerModel q)
        {
            throw new NotImplementedException();
        }

        public void UpdatePerson(PersonModel q)
        {
            throw new NotImplementedException();
        }

        public void UpdatePosition(PositionModel q)
        {
            throw new NotImplementedException();
        }

        public void UpdateQuestionType(QuestionTypeModel q)
        {
            throw new NotImplementedException();
        }

        public void UpdateTestResult(TestResultModel q)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            db.Save();

        }
    }
}
