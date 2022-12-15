using DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class PersonModel
    {
        public int ID { get; set; }
        public Nullable<int> RightsID { get; set; }
        public string FullName { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public ObservableCollection<TestResult> TestResults { get; set; }
        public PersonModel() { }
        public PersonModel(Person p)
        {
            ID = p.ID; 
            RightsID = p.RightsID;
            FullName = p.FullName;
            Mail = p.Mail;
            Password = p.Password;
        }
    }
}
