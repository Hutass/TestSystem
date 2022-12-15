using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    class QuestionRepositorySQL : IRepository<Question>
    {
        private TestBaseDBEntities db;
        public QuestionRepositorySQL(TestBaseDBEntities dbcontext)
        {
            this.db = dbcontext;
        }
        public void Create(Question item)
        {
            db.Question.Add(item);
        }

        public void Delete(int id)
        {
            Question item = db.Question.Find(id);
            if (item != null)
                db.Question.Remove(item);
        }

        public Question GetItem(int id)
        {
            return db.Question.Find(id);
        }

        public List<Question> GetList()
        {
            return db.Question.ToList();
        }

        public void Update(Question item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
