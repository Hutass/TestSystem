using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem.Model
{
    class PersonCRUDModel
    {
        IDBCRUD dbOperations;
        IAuthorizationService authorizationService;


        public PersonCRUDModel(IDBCRUD crud, IAuthorizationService auth)
        {
            dbOperations = crud;
            authorizationService = auth;
        }

        public List<BLL.Models.PersonModel> GetPersones()
        {
            return dbOperations.GetAllPersones().ToList();
        }
        public List<BLL.Models.RightsModel> GetRights()
        {
            return dbOperations.GetAllRights().ToList();
        }

        public int CreatePerson(BLL.Models.PersonModel result)
        {
            return dbOperations.CreatePerson(result);
        }
        public void DeletePerson(BLL.Models.PersonModel result)
        {
            dbOperations.DeletePerson(result);
        }
        public void UpdatePerson(BLL.Models.PersonModel result)
        {
            if (result.RightsID == null)
                result.RightsID = 0;
            if (result.Mail == null)
                result.Mail = "";
            if (result.MiddleName == null)
                result.MiddleName = "";
            if (result.Surname == null)
                result.Surname = "";
            if (result.Name == null)
                result.Name = "";
            if (result.Password == null)
                result.Password = "";

            dbOperations.UpdatePerson(result);
        }
    }
}
