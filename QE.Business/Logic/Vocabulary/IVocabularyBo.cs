using QE.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QE.Business.Logic.Vocabulary
{
    public interface IVocabularyBo
    {
        Task<VocabularyModel> GetById(int id);
        Task<int> Create(VocabularyModel model);
        Task<int> Update(VocabularyModel model);
        Task<int> Delete(int id);
    }
}
