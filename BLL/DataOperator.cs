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
            return db.Question.GetList().Select(i => new QuestionModel(i)).ToList();

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
            Person pr = new Person() { Surname=q.Surname, Name=q.Name, MiddleName=q.MiddleName, RightsID=q.RightsID, Password=q.Password, Mail=q.Mail };
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
            Answer answ = db.Answer.GetItem(q.ID);
            answ.Cost = q.Cost;
            answ.QuestionID = q.QuestionID;
            answ.Text = q.Text;
            Save();
        }

        public void UpdatePerson(PersonModel q)
        {
            Person pers = db.Person.GetItem(q.ID);
            pers.Mail = q.Mail;
            pers.MiddleName = q.MiddleName;
            pers.Name = q.Name;
            pers.Password = q.Password;
            pers.RightsID = q.RightsID;
            pers.Surname = q.Surname;
            Save();
        }

        public void UpdatePosition(PositionModel q)
        {
            Position posm = db.Position.GetItem(q.ID);
            posm.Name = q.Name;
            Save();
        }

        public void UpdateQuestionType(QuestionTypeModel q)
        {
            QuestionType quest = db.QuestionType.GetItem(q.ID);
            quest.Name = q.Name;
            Save();
        }

        public void UpdateTestResult(TestResultModel q)
        {
            TestResult testr = db.TestResult.GetItem(q.ID);
            testr.PersonID = q.PersonID;
            testr.PositionID = q.PositionID;
            testr.Score = q.Score;
            testr.Date = q.Date;
            Save();
        }

        public void DeleteAnswer(AnswerModel q)
        {
            db.Answer.Delete(q.ID);
            Save();
        }

        public void DeletePerson(PersonModel q)
        {
            db.Person.Delete(q.ID);
            Save();
        }

        public void DeletePosition(PositionModel q)
        {
            db.Position.Delete(q.ID);
            Save();
        }

        public void DeleteQuestion(QuestionModel q)
        {
            db.Question.Delete(q.ID);
            Save();
        }

        public void DeleteQuestionType(QuestionTypeModel q)
        {
            db.QuestionType.Delete(q.ID);
            Save();
        }

        public void DeleteTestResult(TestResultModel q)
        {
            db.TestResult.Delete(q.ID);
            Save();
        }

        public void Save()
        {
            db.Save();

        }
    }
}
