using QE.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QE.Business.Model
{
    public class TopicModel
    {
        public TopicModel() { }
        public TopicModel(Topic topic)
        {
            this.Id = topic.Id;
            this.Name = topic.Name;
            this.VocabularyTopics = topic.VocabularyTopics?.Select(x => new VocabularyTopicModel(x)).ToList() ?? new List<VocabularyTopicModel>();
        }
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<VocabularyTopicModel>? VocabularyTopics { get; set; }

    }
}
