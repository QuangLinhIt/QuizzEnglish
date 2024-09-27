using QE.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QE.Business.Logic.Quizz
{
    public interface IQuizzBo
    {
        Task<IEnumerable<QuizzModel>> GetAll(int pageIndex,int pageSize);
        Task<QuizzModel> GetById(int id);
        Task<int> Create(QuizzModel model);
        Task<int> Update(QuizzModel model);
        Task<int> Delete(int id);
    }
}
