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
        Task<int> Create(VocabularyModel vocabularyModel);
        Task<int> Update(VocabularyModel vocabularyModel);
        Task<int> Delete(int id);
    }
}
