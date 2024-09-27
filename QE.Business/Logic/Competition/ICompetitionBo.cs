using QE.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QE.Business.Logic.Competition
{
    public interface ICompetitionBo
    {
        Task<CompetitionModel> GetById(int id);
        Task<int> Create(CompetitionModel model);
        Task<int> Update(CompetitionModel model);
        Task<int> Delete(int id);
    }
}
