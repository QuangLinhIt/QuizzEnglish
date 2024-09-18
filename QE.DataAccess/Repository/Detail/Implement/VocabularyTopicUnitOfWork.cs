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
        public ITopicRepository Topic { get; private set; }
        public IVocabularyRepository Vocabulary { get; private set; }
        public IVocabularyTopicRepository VocabularyTopic { get; private set; }

        public VocabularyTopicUnitOfWork(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            Topic = new TopicRepository(applicationDbContext);
            Vocabulary = new VocabularyRepository(applicationDbContext);
            VocabularyTopic = new VocabularyTopicRepository(applicationDbContext);
        }
    }
}
