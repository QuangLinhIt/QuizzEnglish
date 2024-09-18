using QE.DataAccess.Context;
using QE.DataAccess.Repository.Common.Implement;
using QE.DataAccess.Repository.Detail.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QE.DataAccess.Repository.Detail.Implement
{
    public class QuestionQuizzUnitOfWork:UnitOfWork,IQuestionQuizzUnitOfWork
    {
        public IQuestionRepository Question { get; private set; }
        public IQuizzRepository Quizz { get; private set; }
        public IQuestionQuizzRepository QuestionQuizz { get; private set; }
        public QuestionQuizzUnitOfWork(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            Question = new QuestionRepository(applicationDbContext);
            Quizz = new QuizzRepository(applicationDbContext);
            QuestionQuizz = new QuestionQuizzRepository(applicationDbContext);
        }
    }
}
