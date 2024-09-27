using QE.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QE.Business.Logic.QuizzScore
{
    public interface IQuizzScoreBo
    {
        Task<IEnumerable<QuizzScoreModel>> GetQuizzScoreByQuizzId(int quizzId, string userId);
        Task<int> Create(QuizzScoreModel model);
    }
}
