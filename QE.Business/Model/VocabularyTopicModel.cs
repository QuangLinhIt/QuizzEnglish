using QE.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QE.Business.Model
{
    public class VocabularyTopicModel
    {
        public VocabularyTopicModel() { }
        public VocabularyTopicModel(VocabularyTopic vt)
        {
            this.TopicId = vt.TopicId;
            this.VocabularyId = vt.VocabularyId;
        }
        public int TopicId { get; set; }
        public int VocabularyId { get; set; }
    }
}
