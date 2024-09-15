using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QE.Entity.Entity
{
    public class VocabularyTopic
    {
        public int TopicId { get; set; }
        public Topic Topic { get; set; } = null!;

        public int VocabularyId { get; set; }
        public Vocabulary Vocabulary { get; set; } = null!;
    }
}
