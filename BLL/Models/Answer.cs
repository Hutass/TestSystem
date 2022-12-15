using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class AnswerModel
    {
        public int ID { get; set; }
        public Nullable<int> QuestionID { get; set; }
        public string Text { get; set; }
        public Nullable<double> Cost { get; set; }
        public AnswerModel() { }
        public AnswerModel(Answer a)
        {
            ID = a.ID;
            QuestionID = a.QuestionID;
            Text = a.Text;
            Cost = a.Cost;
        }
    }
}
