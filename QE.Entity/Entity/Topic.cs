using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QE.Entity.Entity
{
    public class Topic
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public ICollection<VocabularyTopic> VocabularyTopics { get; set; } = null!;
    }
}
