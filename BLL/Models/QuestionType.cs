using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class QuestionTypeModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public QuestionTypeModel() { }
        public QuestionTypeModel(QuestionType q)
        {
            ID = q.ID;
            Name = q.Name;
        }
    }
}
