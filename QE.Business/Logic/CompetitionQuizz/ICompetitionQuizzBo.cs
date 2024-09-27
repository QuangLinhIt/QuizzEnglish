using QE.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QE.Business.Logic.CompetitionQuizz
{
    public interface ICompetitionQuizzBo
    {
        Task<IEnumerable<CompetitionQuizzModel>> GetByCompetitionId(int competitionId);
        Task<int> Create(CompetitionQuizzModel model);
    }
}
