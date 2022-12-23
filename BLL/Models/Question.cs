using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class QuestionModel
    {
        public int ID { get; set; }
        public Nullable<int> PositionID { get; set; }
        public Nullable<int> TypeID { get; set; }
        public string Text { get; set; }
        public QuestionModel() { }
        public QuestionModel(Question q)
        {
            ID = q.ID;
            PositionID = q.PositionID;
            TypeID = q.TypeID;
            Text = q.Text;
        }
    }
}
