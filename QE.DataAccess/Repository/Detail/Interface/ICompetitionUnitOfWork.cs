using QE.DataAccess.Repository.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QE.DataAccess.Repository.Detail.Interface
{
    public interface ICompetitionUnitOfWork:IUnitOfWork
    {
        ICompetitionRepository Competition { get; }
        IQuizzRepository Quizz { get; }
        ICompetitionQuizzRepository CompetitionQuizz { get; }
        IUserRepository AppUser { get; }
    }
}
