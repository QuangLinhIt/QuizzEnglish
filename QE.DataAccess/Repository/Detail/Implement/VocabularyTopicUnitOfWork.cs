using QE.DataAccess.Context;
using QE.DataAccess.Repository.Common.Implement;
using QE.DataAccess.Repository.Detail.Interface;
using QE.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QE.DataAccess.Repository.Detail.Implement
{
    public class VocabularyTopicUnitOfWork:UnitOfWork,IVocabularyTopicUnitOfWork
    {
        public ITopicRepository Topics { get; private set; }
        public IVocabularyRepository Vocabularies { get; private set; }

        public VocabularyTopicUnitOfWork(ApplicationDbContext applicationDbContext,
                                         ITopicRepository topicRepository,
                                         IVocabularyRepository vocabularyRepository)
            : base(applicationDbContext)
        {
            Topics = topicRepository;
            Vocabularies = vocabularyRepository;
        }
    }
}
