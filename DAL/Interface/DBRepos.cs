using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface DBRepos
    {
        IRepository<Question> Question { get; }
        IRepository<Answer> Answer { get; }
        IRepository<QuestionType> QuestionType { get; }
        IRepository<Position> Position { get; }
        IRepository<Person> Person { get; }
        IRepository<TestResult> TestResult { get; }
        IRepository<Rights> Rights { get; }
        IReportRepository Reports { get; }
        int Save();
    }
}
