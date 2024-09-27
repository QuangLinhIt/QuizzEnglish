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
    public class QuestionQuizzUnitOfWork : UnitOfWork, IQuestionQuizzUnitOfWork
    {
        public IQuestionRepository Question { get; private set; }
        public IQuizzRepository Quizz { get; private set; }
        public IQuestionQuizzRepository QuestionQuizz { get; private set; }
        public IQuizzScoreRepository QuizzScore { get; private set; }
        public IUserRepository AppUser { get; private set; }
        public QuestionQuizzUnitOfWork(ApplicationDbContext applicationDbContext,
                                    IQuestionRepository questionRepository,
                                    IQuizzRepository quizzRepository,
                                    IQuestionQuizzRepository questionQuizzRepository,
                                    IQuizzScoreRepository quizzScoreRepository,
                                    IUserRepository appUserRepository)
            : base(applicationDbContext)
        {
            Question = questionRepository;
            Quizz = quizzRepository;
            QuestionQuizz = questionQuizzRepository;
            QuizzScore = quizzScoreRepository;
            AppUser = appUserRepository;
        }
    }
}
