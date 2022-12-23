using DAL;
using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.SqlClient;
using DAL.Interface;
using DAL.Entities;
using BLL.Interfaces;

namespace BLL.Sevices
{
    public class QuestionFinder : IReportService
    {
        private DBRepos db;
        public QuestionFinder(DBRepos repos)
        {
            db = repos;
        }
        public List<ReportModels> TestByPerson(int id)
        {
            return db.Reports.TestByPerson(id).Select(i => new ReportModels{ Date = i.Date, Score = i.Score, PositionID = i.PositionID }).Where(i=>i.Score > 45).ToList();

        }

    }

}
