using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using DAL.Interface;

namespace DAL.Repository
{
    class QuestionTypeRepositorySQL : IRepository<QuestionType>
    {
        private TestBaseDBEntities db;
        public QuestionTypeRepositorySQL(TestBaseDBEntities dbcontext)
        {
            this.db = dbcontext;
        }
        public void Create(QuestionType item)
        {
            db.QuestionType.Add(item);
        }

        public void Delete(int id)
        {
            QuestionType item = db.QuestionType.Find(id);
            if (item != null)
                db.QuestionType.Remove(item);
        }

        public QuestionType GetItem(int id)
        {
            return db.QuestionType.Find(id);
        }

        public List<QuestionType> GetList()
        {
            return db.QuestionType.ToList();
        }

        public void Update(QuestionType item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
