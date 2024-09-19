using QE.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QE.Business.Model
{
    public partial class VocabularyModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Meaning { get; set; } = null!;
        public string Pronoun { get; set; } = null!;
        public string? Image { get; set; }
        public string? Audio { get; set; }
        public ICollection<VocabularyTopic> VocabularyTopics { get; set; } = null!;
    }
}
