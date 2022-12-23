using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    class PersonRepositorySQL : IRepository<Person>
    {
        private TestBaseDBEntities db;
        public PersonRepositorySQL(TestBaseDBEntities dbcontext)
        {
            this.db = dbcontext;
        }
        public void Create(Person item)
        {
            db.Person.Add(item);
        }

        public void Delete(int id)
        {
            Person item = db.Person.Find(id);
            if (item != null)
                db.Person.Remove(item);
        }

        public Person GetItem(int id)
        {
            return db.Person.Find(id);
        }

        public List<Person> GetList()
        {
            return db.Person.ToList();
        }

        public void Update(Person item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
