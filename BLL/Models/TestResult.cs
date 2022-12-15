using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{

    public class TestResultModel
    {
        public int ID { get; set; }
        public Nullable<int> PositionID { get; set; }
        public Nullable<int> PersonID { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<double> Score { get; set; }
        public TestResultModel() { }
        public TestResultModel(TestResult t)
        {
            ID = t.ID;
            PositionID = t.PositionID;
            Date = t.Date;
            Score = t.Score;
        }

    }
}
