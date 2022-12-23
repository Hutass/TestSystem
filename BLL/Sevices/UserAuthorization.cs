using BLL.Interfaces;
using BLL.Models;
using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.Pdf;
using System.IO;

namespace BLL.Sevices
{
    public class UserAuthorization : IAuthorizationService
    {
        private DBRepos db;
        public UserAuthorization(DBRepos repos)
        {
            db = repos;
        }
        public int Authorize(PersonModel personAuthData)
        {
            if (personAuthData.Mail == null)
                return 3;
            List<PersonModel> person = db.Person.GetList().Select(i => new PersonModel(i)).Where(i => i.Mail.TrimEnd().Equals(personAuthData.Mail)).ToList();// db.Person.GetList().Select(i => new PersonModel(i)).Where(i => i.Mail == personAuthData.Mail).ToList();
            if (person.Count == 1)
            {
                if (person[0].Password.TrimEnd() == personAuthData.Password)
                    return 0;
                else
                    return 1;
            }
            else
                return 2;

        }
        public void SubmitReport(List<AnswerModel> answers, TestResultModel result, PersonModel person)
        {
            double score = 0;
            foreach(AnswerModel answer in answers) { score += (double)answer.Cost; }
            TestResultModel bufResult = new TestResultModel();
            bufResult.Date = DateTime.Today;
            bufResult.PersonID = person.ID;
            bufResult.PositionID = result.PositionID;
            bufResult.ID = result.ID;
            bufResult.Score = score;
            IDBCRUD crud = new DataOperator(db);
            crud.UpdateTestResult(bufResult);
            SaveReport(answers, bufResult, person);
        }
        public void SaveReport(List<AnswerModel> answers, TestResultModel result, PersonModel person)
        {
            Document document = new Document();
            Page page = document.Pages.Add();
            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment($"{db.Position.GetItem((int)result.PositionID).Name.TrimEnd()}"));
            page.Paragraphs.First().HorizontalAlignment = HorizontalAlignment.Center;
            TextStamp header = new TextStamp($"{DateTime.Today}");
            header.HorizontalAlignment = HorizontalAlignment.Right;
            header.TopMargin = 20;
            header.VerticalAlignment = VerticalAlignment.Top;
            page.AddStamp(header);
            IDBCRUD crud = new DataOperator(db);
            List<QuestionModel> questions = crud.GetAllQuestions().Where(i => i.PositionID == result.PositionID).ToList();
            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment($"Ответы пользователя {person.Surname.TrimEnd()} {person.Name.TrimEnd()} {person.MiddleName.TrimEnd()} ( {person.Mail.TrimEnd()} ):"));
            for (int i=0;i<questions.Count;i++)
            {
                page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment($""));
                page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment($"{questions[i].Text}:"));
                foreach(BLL.Models.AnswerModel answer in answers) { if(answer.QuestionID == questions[i].ID) page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment($"{answer.Text.TrimEnd()} / {answer.Cost}")); }
            }

            CheckFolder();
            document.Save($"{Environment.GetFolderPath(Environment.SpecialFolder.Personal)}" + @"\TestProgram\"+ $"{person.Surname.TrimEnd()}{person.Name.TrimEnd()}{person.MiddleName.TrimEnd()}{db.Position.GetItem((int)result.PositionID).Name.TrimEnd()}.pdf");
        }
        public void CheckFolder()
        {
            if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\TestProgram"))
                return;
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\TestProgram");
        }
    }
}
