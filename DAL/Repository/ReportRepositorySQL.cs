using DAL.Entities;
using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    class ReportRepositorySQL : IReportRepository
    {
        private TestBaseDBEntities db;
        public ReportRepositorySQL(TestBaseDBEntities dbcontext)
        {
            db = dbcontext;
        }
        public List<Reports> TestByPerson(int personID)
        {
            SqlParameter param1 = new SqlParameter("@FullName", personID);
            var result = db.Database.SqlQuery<Reports>("Question_List @FullName", param1).ToList();
            var data = result.Where(i => new { i.Date, i.Score, i.PositionID }.Score > 45)
            .OrderBy(i => i).Select(i => new Reports
            {
                Date = i.Date,
                PositionID = i.PositionID,
                Score = i.Score
            }).ToList();
            return data;
        }
    }
}
