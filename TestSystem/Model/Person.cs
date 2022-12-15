using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem.Model
{
    class PersonModel
    {
        public int ID { get; set; }
        public Nullable<int> RightsID { get; set; }
        public string FullName { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public List<int> TestResults { get; set; }
        public PersonModel() { }
        public PersonModel(BLL.Models.PersonModel p)
        {
            ID = p.ID;
            RightsID = p.RightsID;
            FullName = p.FullName;
            Mail = p.Mail;
            Password = p.Password;
        }
    }
}
