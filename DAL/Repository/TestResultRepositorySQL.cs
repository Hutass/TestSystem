using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    class TestResultRepositorySQL : IRepository<TestResult>
    {
        private TestBaseDBEntities db;
        public TestResultRepositorySQL(TestBaseDBEntities dbcontext)
        {
            this.db = dbcontext;
        }
        public void Create(TestResult item)
        {
            db.TestResult.Add(item);
        }

        public void Delete(int id)
        {
            TestResult item = db.TestResult.Find(id);
            if (item != null)
                db.TestResult.Remove(item);
        }

        public TestResult GetItem(int id)
        {
            return db.TestResult.Find(id);
        }

        public List<TestResult> GetList()
        {
            return db.TestResult.ToList();
        }

        public void Update(TestResult item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }

}
