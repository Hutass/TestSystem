using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository 
{
    public class DBReposSQL : DBRepos
    {
        private TestBaseDBEntities db;
        private AnswerRepositorySQL answerRepository;
        private PersonRepositorySQL personRepository;
        private PositionRepositorySQL positionRepository;
        private QuestionRepositorySQL questionRepository;
        private QuestionTypeRepositorySQL questionTypeRepository;
        private RightsRepositorySQL rightsRepository;
        private TestResultRepositorySQL testResultRepository;
        private ReportRepositorySQL reportRepository;
        public DBReposSQL()
        {
            this.db = new TestBaseDBEntities();
        }
        public IRepository<Question> Question 
        {
            get
            {
                if (questionRepository == null)
                    questionRepository = new QuestionRepositorySQL(db);
                return questionRepository;
            }
        }
        public IRepository<QuestionType> QuestionType
        {
            get
            {
                if (questionTypeRepository == null)
                    questionTypeRepository = new QuestionTypeRepositorySQL(db);
                return questionTypeRepository;
            }
        }

        public IRepository<Answer> Answer
        {
            get
            {
                if (answerRepository == null)
                    answerRepository = new AnswerRepositorySQL(db);
                return answerRepository;
            }
        }

        public IRepository<Person> Person
        {
            get
            {
                if (personRepository == null)
                    personRepository = new PersonRepositorySQL(db);
                return personRepository;
            }
        }

        public IRepository<TestResult> TestResult
        {
            get
            {
                if (testResultRepository == null)
                    testResultRepository = new TestResultRepositorySQL(db);
                return testResultRepository;
            }
        }

        public IRepository<Rights> Rights
        {
            get
            {
                if (rightsRepository == null)
                    rightsRepository = new RightsRepositorySQL(db);
                return rightsRepository;
            }
        }

        public IReportRepository Reports
        {
            get
            {
                if (reportRepository == null)
                    reportRepository = new ReportRepositorySQL(db);
                return reportRepository;
            }
        }

        public IRepository<Position> Position
        {
            get
            {
                if (positionRepository == null)
                    positionRepository = new PositionRepositorySQL(db);
                return positionRepository;
            }
        }

        public int Save()
        {
            return db.SaveChanges();
        }
    }
}
