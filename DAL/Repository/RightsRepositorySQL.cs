using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    class RightsRepositorySQL : IRepository<Rights>
    {
        private TestBaseDBEntities db;
        public RightsRepositorySQL(TestBaseDBEntities dbcontext)
        {
            this.db = dbcontext;
        }
        public void Create(Rights item)
        {
            db.Rights.Add(item);
        }

        public void Delete(int id)
        {
            Rights item = db.Rights.Find(id);
            if (item != null)
                db.Rights.Remove(item);
        }

        public Rights GetItem(int id)
        {
            return db.Rights.Find(id);
        }

        public List<Rights> GetList()
        {
            return db.Rights.ToList();
        }

        public void Update(Rights item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }

}
