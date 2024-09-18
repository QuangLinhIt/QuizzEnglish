using QE.DataAccess.Repository.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QE.DataAccess.Repository.Detail.Interface
{
    public interface IVocabularyTopicUnitOfWork:IUnitOfWork
    {
        ITopicRepository Topics { get; }
        IVocabularyRepository Vocabularies { get; }
    }
}
