using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAuthorizationService
    {
        int Authorize(PersonModel person);
        void SubmitReport(List<AnswerModel> answers, TestResultModel result, PersonModel person);
    }
}
