using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    class AnswerRepositorySQL : IRepository<Answer>
    {
        private TestBaseDBEntities db;
        public AnswerRepositorySQL(TestBaseDBEntities dbcontext)
        {
            this.db = dbcontext;
        }
        public void Create(Answer item)
        {
            db.Answer.Add(item);
        }

        public void Delete(int id)
        {
            Answer item = db.Answer.Find(id);
            if (item != null)
                db.Answer.Remove(item);
        }

        public Answer GetItem(int id)
        {
            return db.Answer.Find(id);
        }

        public List<Answer> GetList()
        {
            return db.Answer.ToList();
        }

        public void Update(Answer item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }

}
