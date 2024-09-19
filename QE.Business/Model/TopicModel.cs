using QE.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QE.Business.Model
{
    public partial class TopicModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<VocabularyTopicModel> VocabularyTopics { get; set; } = null!;
    }
}
