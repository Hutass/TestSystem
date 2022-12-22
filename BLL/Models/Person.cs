
using DAL;
using System;
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
        public string Surname { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public ObservableCollection<TestResult> TestResults { get; set; }
        public PersonModel() { }
        public PersonModel(Person p)
        {
            ID = p.ID; 
            RightsID = p.RightsID;
            Surname = p.Surname;
            Name = p.Name;
            MiddleName = p.MiddleName;
            Mail = p.Mail;
            Password = p.Password;
        }
    }
}
