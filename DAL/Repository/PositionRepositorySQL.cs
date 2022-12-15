using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    class PositionRepositorySQL : IRepository<Position>
    {
        private TestBaseDBEntities db;
        public PositionRepositorySQL(TestBaseDBEntities dbcontext)
        {
            this.db = dbcontext;
        }
        public void Create(Position item)
        {
            db.Position.Add(item);
        }

        public void Delete(int id)
        {
            Position item = db.Position.Find(id);
            if (item != null)
                db.Position.Remove(item);
        }

        public Position GetItem(int id)
        {
            return db.Position.Find(id);
        }

        public List<Position> GetList()
        {
            return db.Position.ToList();
        }

        public void Update(Position item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
