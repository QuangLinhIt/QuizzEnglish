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
    public class CompetitionUnitOfWork : UnitOfWork, ICompetitionUnitOfWork
    {
        public ICompetitionRepository Competition { get; private set; }

        public IQuizzRepository Quizz { get; private set; }

        public ICompetitionQuizzRepository CompetitionQuizz { get; private set; }

        public IUserRepository AppUser { get; private set; }
        public CompetitionUnitOfWork(ApplicationDbContext applicationDbContext,
            ICompetitionRepository competitionRepository,
            IQuizzRepository quizzRepository,
            ICompetitionQuizzRepository competitionQuizzRepository,
            IUserRepository appUserRepository) : base(applicationDbContext)
        {
            Competition = competitionRepository;
            Quizz = quizzRepository;
            AppUser = appUserRepository;
            CompetitionQuizz = competitionQuizzRepository;
        }
    }
}
