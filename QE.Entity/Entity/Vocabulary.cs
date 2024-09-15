using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QE.Entity.Entity
{
    public class Vocabulary
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Meaning { get; set; }
        public required string Pronoun { get; set; }
        public required string Image { get; set; }
        public required string Audio { get; set; }

        public ICollection<VocabularyTopic> VocabularyTopics { get; set; } = null!;

    }
}
