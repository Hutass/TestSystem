using BLL.Interfaces;
using BLL.Models;
using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Sevices
{
    class UserAuthorization : IAuthorizationService
    {
        private DBRepos db;
        public UserAuthorization(DBRepos repos)
        {
            db = repos;
        }
        public int Authorize(PersonModel personAuthData)
        {
            List<PersonModel> person = db.Person.GetList().Select(i => new PersonModel(i)).Where(i => i.Mail == personAuthData.Mail).ToList();
            if (person.Count == 1)
            {
                if (person[0].Password == personAuthData.Password)
                    return 0;
                else
                    return 1;
            }
            else
                return 2;

        }
    }
}
