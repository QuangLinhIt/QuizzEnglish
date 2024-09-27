using QE.DataAccess.Repository.Common.Interface;
using QE.Entity.Entity.Abstract.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QE.DataAccess.Repository.Detail.Interface
{
    public interface IQuestionRepository:IRepository<Question>
    {
        Task<bool> DeleteQuestionsNotRelationship();
    }
}
