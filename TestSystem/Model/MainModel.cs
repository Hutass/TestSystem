using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem.Model
{
    class MainModel
    {
        IDBCRUD dbOperations;
        IAuthorizationService authorizationService;


        public MainModel(IDBCRUD crud, IAuthorizationService auth)
        {
            dbOperations = crud;
            authorizationService = auth;
        }

        public int AuthorizationCheck(BLL.Models.PersonModel person)
        {
            return authorizationService.Authorize(person);
        }
        public void CreatePerson(BLL.Models.PersonModel person)
        {
            person.RightsID = 1;
            dbOperations.CreatePerson(person);
        }
        public BLL.Models.PersonModel GetPerson(string mail)
        {
            return dbOperations.GetAllPersones().Where(i => i.Mail.TrimEnd() == mail).ToList().First();
        }
    }
}
